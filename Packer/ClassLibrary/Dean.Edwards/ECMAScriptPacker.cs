using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Dean.Edwards;

namespace SmallSharpTools.Packer.Utilities.Dean.Edwards
{
    /// <summary>
    /// Packs a javascript file into a smaller area, removing unnecessary characters from the output.
    /// </summary>
    /// <remarks>
    /// packer, version 2.0 (beta) (2005/02/01)
    /// Copyright 2004-2005, Dean Edwards
    /// Web: http://dean.edwards.name/
    /// 
    /// This software is licensed under the CC-GNU LGPL
    /// Web: http://creativecommons.org/licenses/LGPL/2.1/
    /// 
    /// Ported to C# by Jesse Hansen, twindagger2k@msn.com
    /// </remarks>
    public class ECMAScriptPacker : IHttpHandler
    {
        /// <summary>
        /// The encoding level to use. See http://dean.edwards.name/packer/usage/ for more info.
        /// </summary>
        public enum PackerEncoding
        {
            None = 0, 
            Numeric = 10, 
            Mid = 36, 
            Normal = 62, 
            HighAscii = 95
        };

        #region Private fields

        private PackerEncoding _encoding = PackerEncoding.Normal;
        private bool _fastDecode = true;
        private bool _specialChars;
        private bool _enabled = true;
        private WordList _encodingLookup;

        #endregion Private fields

        #region Constants

        private const string IGNORE = "$1";
        private const string LOOKUP36 = "0123456789abcdefghijklmnopqrstuvwxyz";
        //lookups seemed like the easiest way to do this since 
        // I don't know of an equivalent to .toString(36)
        private const string LOOKUP62 = LOOKUP36 + "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string LOOKUP95 = "¡¢£¤¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿ";

        #endregion Constants

        #region Public properties

        /// <summary>
        /// The encoding level for this instance
        /// </summary>
        public PackerEncoding Encoding
        {
            get { return _encoding; }
            set { _encoding = value; }
        }

        /// <summary>
        /// Adds a subroutine to the output to speed up decoding
        /// </summary>
        public bool FastDecode
        {
            get { return _fastDecode; }
            set { _fastDecode = value; }
        }

        /// <summary>
        /// Replaces special characters
        /// </summary>
        public bool SpecialChars
        {
            get { return _specialChars; }
            set { _specialChars = value; }
        }

        /// <summary>
        /// Packer enabled
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        #endregion Public properties

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ECMAScriptPacker()
        {
            Encoding = PackerEncoding.Normal;
            FastDecode = true;
            SpecialChars = false;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="encoding">The encoding level for this instance</param>
        /// <param name="fastDecode">Adds a subroutine to the output to speed up decoding</param>
        /// <param name="specialChars">Replaces special characters</param>
        public ECMAScriptPacker(PackerEncoding encoding, bool fastDecode, bool specialChars)
        {
            Encoding = encoding;
            FastDecode = fastDecode;
            SpecialChars = specialChars;
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Packs the script
        /// </summary>
        /// <param name="script">Script to pack</param>
        /// <returns>Packed script</returns>
        public string Pack(string script)
        {
            if (_enabled)
            {
                script += "\n";
                script = BasicCompression(script);
                if (SpecialChars)
                    script = EncodeSpecialChars(script);
                if (Encoding != PackerEncoding.None)
                    script = EncodeKeywords(script);
            }
            return script;
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Zero encoding - just removal of whitespace and comments
        /// </summary>
        /// <param name="script">Script to compress</param>
        /// <returns>Compressed script</returns>
        private string BasicCompression(string script)
        {
            ParseMaster parser = new ParseMaster();
            // make safe
            parser.EscapeChar = '\\';
            // protect strings
            parser.Add("'[^'\\n\\r]*'", IGNORE);
            parser.Add("\"[^\"\\n\\r]*\"", IGNORE);
            // remove comments
            parser.Add("\\/\\/[^\\n\\r]*[\\n\\r]");
            parser.Add("\\/\\*[^*]*\\*+([^\\/][^*]*\\*+)*\\/");
            // protect regular expressions
            parser.Add("\\s+(\\/[^\\/\\n\\r\\*][^\\/\\n\\r]*\\/g?i?)", "$2");
            parser.Add("[^\\w\\$\\/'\"*)\\?:]\\/[^\\/\\n\\r\\*][^\\/\\n\\r]*\\/g?i?", IGNORE);
            // remove: ;;; doSomething();
            if (_specialChars)
            {
                parser.Add(";;[^\\n\\r]+[\\n\\r]");
            }
            // remove redundant semi-colons
            parser.Add(";+\\s*([};])", "$2");
            // remove white-space
            parser.Add("(\\b|\\$)\\s+(\\b|\\$)", "$2 $3");
            parser.Add("([+\\-])\\s+([+\\-])", "$2 $3");
            parser.Add("\\s+");
            // done
            return parser.Exec(script);
        }

        /// <summary>
        /// Encodes special characters
        /// </summary>
        /// <param name="script">Script to encode</param>
        /// <returns>Encoded script</returns>
        private string EncodeSpecialChars(string script)
        {
            ParseMaster parser = new ParseMaster();
            // replace: $name -> n, $$name -> na
            parser.Add("((\\$+)([a-zA-Z\\$_]+))(\\d*)", new ParseMaster.MatchGroupEvaluator(EncodeLocalVars));

            // replace: _name -> _0, double-underscore (__name) is ignored
            Regex regex = new Regex("\\b_[A-Za-z\\d]\\w*");
            
            // build the word list
            _encodingLookup = Analyze(script, regex, EncodePrivate);

            parser.Add("\\b_[A-Za-z\\d]\\w*", new ParseMaster.MatchGroupEvaluator(EncodeWithLookup));
            
            script = parser.Exec(script);
            return script;
        }

        /// <summary>
        /// Encodes JS keywords
        /// </summary>
        /// <param name="script">Script to encode</param>
        /// <returns>Encoded script</returns>
        private string EncodeKeywords(string script)
        {
            // escape high-ascii values already in the script (i.e. in strings)
            if (Encoding.Equals(PackerEncoding.HighAscii))
            {
                script = Escape95(script);
            }
            // create the parser
            ParseMaster parser = new ParseMaster();
            EncodeMethod encode = GetEncoder(Encoding);

            // for high-ascii, don't encode single character low-ascii
            Regex regex = new Regex((Encoding.Equals(PackerEncoding.HighAscii)) ? "\\w\\w+" : "\\w+");
            // build the word list
            _encodingLookup = Analyze(script, regex, encode);

            // encode
            parser.Add((Encoding.Equals(PackerEncoding.HighAscii)) ? "\\w\\w+" : "\\w+",
                       new ParseMaster.MatchGroupEvaluator(EncodeWithLookup));

            // if encoded, wrap the script in a decoding function
            return (String.IsNullOrEmpty(script)) ? String.Empty : BootStrap(parser.Exec(script), _encodingLookup);
        }

        /// <summary>
        /// Wraps a packed script into a decoding function.
        /// </summary>
        /// <param name="packed">Packed script</param>
        /// <param name="keywords">Keywords used</param>
        /// <returns>Modified script</returns>
        private string BootStrap(string packed, WordList keywords)
        {
            // packed: the packed script
            packed = "'" + Escape(packed) + "'";

            // ascii: base for encoding
            int ascii = Math.Min(keywords.Sorted.Count, (int) Encoding);
            if (ascii.Equals(0))
            {
                ascii = 1;
            }

            // count: number of words contained in the script
            int count = keywords.Sorted.Count;

            // keywords: list of words contained in the script
            foreach (object key in keywords.Protected.Keys)
            {
                keywords.Sorted[(int) key] = String.Empty;
            }
            // convert from a string to an array
            StringBuilder sbKeywords = new StringBuilder("'");
            foreach (string word in keywords.Sorted)
            {
                sbKeywords.Append(word + "|");
            }
            sbKeywords.Remove(sbKeywords.Length-1, 1);
            string keywordsout = sbKeywords + "'.split('|')";

            string encode;
            string inline = "c";

            switch (Encoding)
            {
                case PackerEncoding.Mid:
                    encode = "function(c){return c.toString(36)}";
                    inline += ".toString(a)";
                    break;
                case PackerEncoding.Normal:
                    encode = "function(c){return(c<a?\"\":e(parseInt(c/a)))+" +
                             "((c=c%a)>35?String.fromCharCode(c+29):c.toString(36))}";
                    inline += ".toString(a)";
                    break;
                case PackerEncoding.HighAscii:
                    encode = "function(c){return(c<a?\"\":e(c/a))+" +
                             "String.fromCharCode(c%a+161)}";
                    inline += ".toString(a)";
                    break;
                default:
                    encode = "function(c){return c}";
                    break;
            }
            
            // decode: code snippet to speed up decoding
            string decode = String.Empty;
            if (_fastDecode)
            {
                decode = "if(!''.replace(/^/,String)){while(c--)d[e(c)]=k[c]||e(c);k=[function(e){return d[e]}];e=function(){return'\\\\w+'};c=1;}";
                if (Encoding.Equals(PackerEncoding.HighAscii))
                {
                    decode = decode.Replace("\\\\w", "[\\xa1-\\xff]");
                }
                else if (Encoding.Equals(PackerEncoding.Numeric))
                {
                    decode = decode.Replace("e(c)", inline);
                }
                if (count.Equals(0))
                {
                    decode = decode.Replace("c=1", "c=0");
                }
            }

            // boot function
            string unpack = "function(p,a,c,k,e,d){while(c--)if(k[c])p=p.replace(new RegExp('\\\\b'+e(c)+'\\\\b','g'),k[c]);return p;}";
            Regex r;
            if (_fastDecode)
            {
                //insert the decoder
                r = new Regex("\\{");
                unpack = r.Replace(unpack, "{" + decode + ";", 1);
            }

            if (Encoding.Equals(PackerEncoding.HighAscii))
            {
                // get rid of the word-boundries for regexp matches
                r = new Regex("'\\\\\\\\b'\\s*\\+|\\+\\s*'\\\\\\\\b'");
                unpack = r.Replace(unpack, "");
            }
            if (Encoding.Equals(PackerEncoding.HighAscii) || ascii > (int) PackerEncoding.Normal || _fastDecode)
            {
                // insert the encode function
                r = new Regex("\\{");
                unpack = r.Replace(unpack, "{e=" + encode + ";", 1);
            }
            else
            {
                r = new Regex("e\\(c\\)");
                unpack = r.Replace(unpack, inline);
            }
            // no need to pack the boot function since I've already done it
            string _params = packed + "," + ascii + "," + count + "," + keywordsout;
            if (_fastDecode)
            {
                //insert placeholders for the decoder
                _params += ",0,{}";
            }
            // the whole thing
            return "eval(" + unpack + "(" + _params + "))\n";
        }

        /// <summary>
        /// Escapes comment strings
        /// </summary>
        /// <param name="input">String to escape</param>
        /// <returns>Escaped string</returns>
        private static string Escape(string input)
        {
            Regex r = new Regex("([\\\\'])");
            return r.Replace(input, "\\$1");
        }

        /// <summary>
        /// Retrieves the an encoder for an encoding method
        /// </summary>
        /// <param name="encoding">Encoding method</param>
        /// <returns>Encoder</returns>
        private static EncodeMethod GetEncoder(PackerEncoding encoding)
        {
            switch (encoding)
            {
                case PackerEncoding.Mid:
                    return Encode36;
                case PackerEncoding.Normal:
                    return Encode62;
                case PackerEncoding.HighAscii:
                    return Encode95;
                default:
                    return Encode10;
            }
        }

        private static string Encode10(int code)
        {
            return code.ToString();
        }

        private static string Encode36(int code)
        {
            string encoded = "";
            int i = 0;
            do
            {
                int digit = (code / (int) Math.Pow(36, i)) % 36;
                encoded = LOOKUP36[digit] + encoded;
                code -= digit * (int) Math.Pow(36, i++);
            } while (code > 0);
            return encoded;
        }

        private static string Encode62(int code)
        {
            string encoded = "";
            int i = 0;
            do
            {
                int digit = (code / (int) Math.Pow(62, i)) % 62;
                encoded = LOOKUP62[digit] + encoded;
                code -= digit * (int) Math.Pow(62, i++);
            } while (code > 0);
            return encoded;
        }

        private static string Encode95(int code)
        {
            string encoded = "";
            int i = 0;
            do
            {
                int digit = (code / (int) Math.Pow(95, i)) % 95;
                encoded = LOOKUP95[digit] + encoded;
                code -= digit * (int) Math.Pow(95, i++);
            } while (code > 0);
            return encoded;
        }

        private static string Escape95(string input)
        {
            Regex r = new Regex("[\xa1-\xff]");
            return r.Replace(input, new MatchEvaluator(Escape95Eval));
        }

        private static string Escape95Eval(Match match)
        {
            return "\\x" + ((int) match.Value[0]).ToString("x"); //return hexadecimal value
        }

        private static string EncodeLocalVars(Match match, int offset)
        {
            int length = match.Groups[offset + 2].Length;
            int start = length - Math.Max(length - match.Groups[offset + 3].Length, 0);
            return match.Groups[offset + 1].Value.Substring(start, length) + 
                   match.Groups[offset + 4].Value;
        }

        private string EncodeWithLookup(Match match, int offset)
        {
            return (string) _encodingLookup.Encoded[match.Groups[offset].Value];
        }

        private delegate string EncodeMethod(int code);

        private static string EncodePrivate(int code)
        {
            return "_" + code;
        }

        private static WordList Analyze(string input, Regex regex, EncodeMethod encodeMethod)
        {
            // analyse
            // retreive all words in the script
            MatchCollection all = regex.Matches(input);
            WordList rtrn;
            rtrn.Sorted = new StringCollection(); // list of words sorted by frequency
            rtrn.Protected = new HybridDictionary(); // dictionary of word->encoding
            rtrn.Encoded = new HybridDictionary(); // instances of "protected" words
            if (all.Count > 0)
            {
                StringCollection unsorted = new StringCollection(); // same list, not sorted
                HybridDictionary Protected = new HybridDictionary(); // "protected" words (dictionary of word->"word")
                HybridDictionary values = new HybridDictionary(); // dictionary of charCode->encoding (eg. 256->ff)
                HybridDictionary count = new HybridDictionary(); // word->count
                int i = all.Count, j = 0;
                string word;
                // count the occurrences - used for sorting later
                do
                {
                    word = "$" + all[--i].Value;
                    if (count[word] == null)
                    {
                        count[word] = 0;
                        unsorted.Add(word);
                        // make a dictionary of all of the protected words in this script
                        //  these are words that might be mistaken for encoding
                        Protected["$" + (values[j] = encodeMethod(j))] = j++;
                    }
                    // increment the word counter
                    count[word] = (int) count[word] + 1;
                } while (i > 0);
                /* prepare to sort the word list, first we must protect
                    words that are also used as codes. we assign them a code
                    equivalent to the word itself.
                   e.g. if "do" falls within our encoding range
                        then we store keywords["do"] = "do";
                   this avoids problems when decoding */
                i = unsorted.Count;
                string[] sortedarr = new string[unsorted.Count];
                do
                {
                    word = unsorted[--i];
                    if (Protected[word] != null)
                    {
                        sortedarr[(int) Protected[word]] = word.Substring(1);
                        rtrn.Protected[(int) Protected[word]] = true;
                        count[word] = 0;
                    }
                } while (i > 0);
                string[] unsortedarr = new string[unsorted.Count];
                unsorted.CopyTo(unsortedarr, 0);
                // sort the words by frequency
                Array.Sort(unsortedarr, new CountComparer(count));
                j = 0;
                /*because there are "protected" words in the list
                  we must add the sorted words around them */
                do 
                {
                    if (sortedarr[i] == null) 
                        sortedarr[i] = unsortedarr[j++].Substring(1);
                    rtrn.Encoded[sortedarr[i]] = values[i];
                } while (++i < unsortedarr.Length);
                rtrn.Sorted.AddRange(sortedarr);
            }
            return rtrn;
        }

        #endregion Private methods

        private struct WordList
        {
            public StringCollection Sorted;
            public HybridDictionary Encoded;
            public HybridDictionary Protected;
        }

        private class CountComparer : IComparer
        {
            readonly HybridDictionary count;

            public CountComparer(HybridDictionary count)
            {
                this.count = count;
            }

            #region IComparer Members

            public int Compare(object x, object y)
            {
                return (int) count[y] - (int) count[x];
            }

            #endregion
        }
        #region IHttpHandler Members

        public void ProcessRequest(HttpContext context)
        {
            // try and read settings from config file
            //ConfigurationManager.AppSettings["ecmascriptpacker"] 
            if (ConfigurationManager.GetSection("ecmascriptpacker") != null)
            {
                NameValueCollection cfg = 
                    (NameValueCollection) ConfigurationManager.GetSection("ecmascriptpacker");
                if (cfg["Encoding"] != null)
                {
                    switch(cfg["Encoding"].ToLower())
                    {
                        case "none":
                            Encoding = PackerEncoding.None;
                            break;
                        case "numeric":
                            Encoding = PackerEncoding.Numeric;
                            break;
                        case "mid":
                            Encoding = PackerEncoding.Mid;
                            break;
                        case "normal":
                            Encoding = PackerEncoding.Normal;
                            break;
                        case "highascii":
                        case "high":
                            Encoding = PackerEncoding.HighAscii;
                            break;
                    }
                }
                if (cfg["FastDecode"] != null)
                {
                    FastDecode = cfg["FastDecode"].ToLower().Equals("true");
                }
                if (cfg["SpecialChars"] != null)
                {
                    SpecialChars = cfg["SpecialChars"].ToLower().Equals("true");
                }
                if (cfg["Enabled"] != null)
                {
                    Enabled = cfg["Enabled"].ToLower().Equals("true");
                }
            }
            // try and read settings from URL
            if (context.Request.QueryString["Encoding"] != null)
            {
                switch(context.Request.QueryString["Encoding"].ToLower())
                {
                    case "none":
                        Encoding = PackerEncoding.None;
                        break;
                    case "numeric":
                        Encoding = PackerEncoding.Numeric;
                        break;
                    case "mid":
                        Encoding = PackerEncoding.Mid;
                        break;
                    case "normal":
                        Encoding = PackerEncoding.Normal;
                        break;
                    case "highascii":
                    case "high":
                        Encoding = PackerEncoding.HighAscii;
                        break;
                }
            }
            if (context.Request.QueryString["FastDecode"] != null)
            {
                FastDecode = context.Request.QueryString["FastDecode"].ToLower().Equals("true");
            }
            if (context.Request.QueryString["SpecialChars"] != null)
            {
                SpecialChars = context.Request.QueryString["SpecialChars"].ToLower().Equals("true");
            }
            if (context.Request.QueryString["Enabled"] != null)
            {
                Enabled = context.Request.QueryString["Enabled"].ToLower().Equals("true");
            }
            //handle the request
            TextReader r = new StreamReader(context.Request.PhysicalPath);
            string jscontent = r.ReadToEnd();
            r.Close();
            context.Response.ContentType = "text/javascript";
            context.Response.Output.Write(Pack(jscontent));
        }

        public bool IsReusable
        {
            get 
            { 
                if (ConfigurationManager.GetSection("ecmascriptpacker") != null)
                {
                    NameValueCollection cfg = (NameValueCollection) ConfigurationManager.GetSection("ecmascriptpacker");
                    if (cfg["IsReusable"] != null)
                    {
                        if (cfg["IsReusable"].ToLower().Equals("true"))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        #endregion
    }
}