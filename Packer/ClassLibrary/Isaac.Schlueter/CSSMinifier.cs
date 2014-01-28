using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace SmallSharpTools.Packer.Utilities.Isaac.Schlueter
{
    /// <summary>
    /// Minifies a CSS file.
    /// </summary>
    /// <remarks>
    /// BSD License http://developer.yahoo.net/yui/license.txt
    /// Written in Java by Isaac Schlueter
    /// Ported to C# by Daniel Crenna http://www.dimebrain.com/2008/03/a-better-css-mi.html
    /// New css tests and regexes by Michael Ash http://regexadvice.com/blogs/mash/archive/2008/04/27/Update-to-CSS-Minification.aspx
    /// Cleaned up, commented and refactored by Chris Lienert
    /// </remarks>
    public static class CSSMinifier
    {

        #region Private fields

        private static readonly Hashtable _shortColorNames = CreateColorTable();
        private static readonly Hashtable _shortHexColors = CreateHexTable();

        #endregion Private fields

        #region Public methods

        /// <summary>
        /// Minify CSS code
        /// </summary>
        /// <param name="css">Code to minify</param>
        /// <returns>Minified code</returns>
        public static string Minify(string css)
        {
            return Minify(css, 0);
        }

        /// <summary>
        /// Minify CSS code
        /// </summary>
        /// <param name="css">Code to minify</param>
        /// <param name="columnWidth">Column width at which to break output</param>
        /// <returns>Minified code</returns>
        public static string Minify(string css, int columnWidth)
        {
            css = RemoveCommentBlocks(css);
            // bypass RegEx optimizations for large files
            if (css.Length < 10000)
            {
                css = RunRegexOptimizations(css, columnWidth);
            }
            if (columnWidth > 0)
            {
                css = BreakLines(css, columnWidth);
            }
            return css;
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Strips any comment blocks from the provided code
        /// </summary>
        /// <param name="input">CSS code</param>
        /// <returns>Lovely uncommented CSS</returns>
        private static string RemoveCommentBlocks(string input)
        {
            int startIndex = 0;
            bool iemac = false;
            startIndex = input.IndexOf(@"/*", startIndex);
            while (startIndex >= 0)
            {
                int endIndex = input.IndexOf(@"*/", startIndex + 2);
                if (endIndex >= startIndex + 2)
                {
                    if (input[endIndex - 1] == '\\')
                    {
                        startIndex = endIndex + 2;
                        iemac = true;
                    }
                    else if (iemac)
                    {
                        startIndex = endIndex + 2;
                        iemac = false;
                    }
                    else
                    {
                        input = input.Remove(startIndex, endIndex + 2 - startIndex);
                    }
                }
                startIndex = input.IndexOf(@"/*", startIndex);
            }
            return input;
        }

        public static string RunRegexOptimizations(string css, int columnWidth)
        {
            MatchEvaluator rgbDelegate = RGBMatchHandler;
            MatchEvaluator shortColorNameDelegate = ShortColorNameMatchHandler;
            MatchEvaluator shortColorHexDelegate = ShortColorHexMatchHandler;

            css = Regex.Replace(css, @"\s+", " "); //Normalize whitespace
            css = Regex.Replace(css, @"\x22\x5C\x22}\x5C\\x22\x22", "___PSEUDOCLASSBMH___"); //hide Box model hack
            /* Remove the spaces before the things that should not have spaces before them.
               But, be careful not to turn "p :link {...}" into "p:link{...}"
            */
            css = Regex.Replace(css, @"(?#no preceding space needed)\s+((?:[!{};>+()\],])|(?<={[^{}]*):(?=[^}]*}))", "$1");
            css = Regex.Replace(css, @"([!{}:;>+([,])\s+", "$1");  // Remove the spaces after the things that should not have spaces after them.
            css = Regex.Replace(css, @"([^;}])}", "$1;}");    // Add the semicolon where it's missing.
            css = Regex.Replace(css, @"(\d+)\.0+(p(?:[xct])|(?:[cem])m|%|in|ex)\b", "$1$2"); // Remove .0 from size units x.0em becomes xem
            css = Regex.Replace(css, @"([\s:])(0)(px|em|%|in|cm|mm|pc|pt|ex)\b", "$1$2"); // Remove unit from zero
            //New test
            //Font weights
            css = Regex.Replace(css, @"(?<=font-weight:)normal\b", "400");
            css = Regex.Replace(css, @"(?<=font-weight:)bold\b", "700");
            //Thought this was a good idea but properties of a set not defined get element defaults. This is reseting them. css = ShortHandProperty(css);
            css = ShortHandAllProperties(css);
            //css = Regex.Replace(css, @":(\s*0){2,4}\s*;", ":0;"); // if all parameters zero just use 1 parameter
            // if all 4 parameters the same unit make 1 parameter
            css = Regex.Replace(css, @"(?<!background-position\s*):\s*(inherit|auto|0|(?:(?:\d*\.?\d+(?:p(?:[xct])|(?:[cem])m|%|in|ex))))(\s+\1){1,3};", ":$1;", RegexOptions.IgnoreCase);
            // if has 4 parameters and top unit = bottom unit and right unit = left unit make 2 parameters
            css = Regex.Replace(css, @":\s*((inherit|auto|0|(?:(?:\d*\.?\d+(?:p(?:[xct])|(?:[cem])m|%|in|ex))))\s+(inherit|auto|0|(?:(?:\d?\.?\d(?:p(?:[xct])|(?:[cem])m|%|in|ex)))))\s+\2\s+\3;", ":$1;", RegexOptions.IgnoreCase);
            // if has 4 parameters and top unit != bottom unit and right unit = left unit make 3 parameters
            css = Regex.Replace(css, @":\s*((?:(?:inherit|auto|0|(?:(?:\d*\.?\d+(?:p(?:[xct])|(?:[cem])m|%|in|ex))))\s+)?(inherit|auto|0|(?:(?:\d?\.?\d(?:p(?:[xct])|(?:[cem])m|%|in|ex))))\s+(?:0|(?:(?:\d?\.?\d(?:p(?:[xct])|(?:[cem])m|%|in|ex)))))\s+\2;", ":$1;", RegexOptions.IgnoreCase);
            //// if has 3 parameters and top unit = bottom unit make 2 parameters
            //css = Regex.Replace(css, @":\s*((0|(?:(?:\d?\.?\d(?:p(?:[xct])|(?:[cem])m|%|in|ex))))\s+(?:0|(?:(?:\d?\.?\d(?:p(?:[xct])|(?:[cem])m|%|in|ex)))))\s+\2;", ":$1;", RegexOptions.IgnoreCase);
            css = Regex.Replace(css, "background-position:0;", "background-position:0 0;");
            css = Regex.Replace(css, @"(:|\s)0+\.(\d+)", "$1.$2");
            //  Outline-styles and Border-sytles parameter reduction
            css = Regex.Replace(css, @"(outline|border)-style\s*:\s*(none|hidden|d(?:otted|ashed|ouble)|solid|groove|ridge|inset|outset)(?:\s+\2){1,3};", "$1-style:$2;", RegexOptions.IgnoreCase);

            css = Regex.Replace(css, @"(outline|border)-style\s*:\s*((none|hidden|d(?:otted|ashed|ouble)|solid|groove|ridge|inset|outset)\s+(none|hidden|d(?:otted|ashed|ouble)|solid|groove|ridge|inset|outset ))(?:\s+\3)(?:\s+\4);", "$1-style:$2;", RegexOptions.IgnoreCase);

            css = Regex.Replace(css, @"(outline|border)-style\s*:\s*((?:(?:none|hidden|d(?:otted|ashed|ouble)|solid|groove|ridge|inset|outset)\s+)?(none|hidden|d(?:otted|ashed|ouble)|solid|groove|ridge|inset|outset )\s+(?:none|hidden|d(?:otted|ashed|ouble)|solid|groove|ridge|inset|outset ))(?:\s+\3);", "$1-style:$2;", RegexOptions.IgnoreCase);

            css = Regex.Replace(css, @"(outline|border)-style\s*:\s*((none|hidden|d(?:otted|ashed|ouble)|solid|groove|ridge|inset|outset)\s+(?:none|hidden|d(?:otted|ashed|ouble)|solid|groove|ridge|inset|outset ))(?:\s+\3);", "$1-style:$2;", RegexOptions.IgnoreCase);

            //  Outline-color and Border-color parameter reduction
            css = Regex.Replace(css, @"(outline|border)-color\s*:\s*((?:\#(?:[0-9A-F]{3}){1,2})|\S+)(?:\s+\2){1,3};", "$1-color:$2;", RegexOptions.IgnoreCase);

            css = Regex.Replace(css, @"(outline|border)-color\s*:\s*(((?:\#(?:[0-9A-F]{3}){1,2})|\S+)\s+((?:\#(?:[0-9A-F]{3}){1,2})|\S+))(?:\s+\3)(?:\s+\4);", "$1-color:$2;", RegexOptions.IgnoreCase);

            css = Regex.Replace(css, @"(outline|border)-color\s*:\s*((?:(?:(?:\#(?:[0-9A-F]{3}){1,2})|\S+)\s+)?((?:\#(?:[0-9A-F]{3}){1,2})|\S+)\s+(?:(?:\#(?:[0-9A-F]{3}){1,2})|\S+))(?:\s+\3);", "$1-color:$2;", RegexOptions.IgnoreCase);

            // Shorten colors from rgb(51,102,153) to #336699
            // This makes it more likely that it'll get further compressed in the next step.
            css = Regex.Replace(css, @"rgb\s*\x28((?:25[0-5])|(?:2[0-4]\d)|(?:[01]?\d?\d))\s*,\s*((?:25[0-5])|(?:2[0-4]\d)|(?:[01]?\d?\d))\s*,\s*((?:25[0-5])|(?:2[0-4]\d)|(?:[01]?\d?\d))\s*\x29", rgbDelegate);
            css = Regex.Replace(css, @"(?<![\x22\x27=]\s*)\#(?:([0-9A-F])\1)(?:([0-9A-F])\2)(?:([0-9A-F])\3)", "#$1$2$3", RegexOptions.IgnoreCase);
            // Replace hex color code with named value is shorter
            css = Regex.Replace(css, @"(?<=color\s*:\s*.*)\#(?<hex>f00)\b", "red", RegexOptions.IgnoreCase);
            css = Regex.Replace(css, @"(?<=color\s*:\s*.*)\#(?<hex>[0-9a-f]{6})", shortColorNameDelegate, RegexOptions.IgnoreCase);
            css = Regex.Replace(css, @"(?<=color\s*:\s*)\b(Black|Fuchsia|LightSlateGr[ae]y|Magenta|White|Yellow)\b", shortColorHexDelegate, RegexOptions.IgnoreCase);

            // Remove empty rules.
            css = Regex.Replace(css, @"[^}]+{;}", "");
            //Remove semicolon of last property
            css = Regex.Replace(css, ";(})", "$1");
            return css;
        }

        private static String RGBMatchHandler(Match m)
        {
            int val;
            StringBuilder hexcolor = new StringBuilder("#");
            for (int index = 1; index <= 3; index += 1)
            {
                val = Int32.Parse(m.Groups[index].Value);
                hexcolor.Append(val.ToString("x2"));
            }
            return hexcolor.ToString();
        }

        /// <summary>
        /// Splits the code into lines according to the provided column width
        /// </summary>
        /// <param name="css">Code to modify</param>
        /// <param name="columnWidth">Column width</param>
        /// <returns>Split code</returns>
        private static string BreakLines(string css, int columnWidth)
        {
            int i = 0;
            int start = 0;
            StringBuilder sb = new StringBuilder(css);
            while (i < sb.Length)
            {
                char c = sb[i++];
                if (c == '}' && i - start > columnWidth)
                {
                    sb.Insert(i, '\n');
                    start = i;
                }
            }
            return sb.ToString();

        }

        /// <summary>
        /// Replaces hex color values' named colors if the name is shorter than the hex code
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private static string ShortColorNameMatchHandler(Match m)
        {
            string returnValue = m.Value;
            if (_shortColorNames.ContainsKey(m.Groups["hex"].Value.ToLower()))
            {
                returnValue = _shortColorNames[m.Groups["hex"].Value.ToLower()].ToString();
            }
            return returnValue;
        }

        /// <summary>
        /// Replaces named values with there shorter hex equivalent
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private static string ShortColorHexMatchHandler(Match m)
        {
            return _shortHexColors[m.Value.ToString().ToLower()].ToString();
        }

        /// <summary>
        /// Initialises color hash table
        /// </summary>
        private static Hashtable CreateColorTable()
        {
            Hashtable ht = new Hashtable();

            //Color names shorter than hex notation. Except for red.
            ht.Add("f0ffff", "azure");
            ht.Add("f5f5dc", "beige");
            ht.Add("ffe4c4", "bisque");
            ht.Add("a52a2a", "brown");
            ht.Add("ff7f50", "coral");
            ht.Add("ffd700", "gold");
            ht.Add("808080", "grey");
            ht.Add("008000", "green");
            ht.Add("4b0082", "indigo");
            ht.Add("fffff0", "ivory");
            ht.Add("f0e68c", "khaki");
            ht.Add("faf0e6", "linen");
            ht.Add("800000", "maroon");
            ht.Add("000080", "navy");
            ht.Add("808000", "olive");
            ht.Add("ffa500", "orange");
            ht.Add("da70d6", "orchid");
            ht.Add("cd853f", "peru");
            ht.Add("ffc0cb", "pink");
            ht.Add("dda0dd", "plum");
            ht.Add("800080", "purple");
            ht.Add("fa8072", "salmon");
            ht.Add("a0522d", "sienna");
            ht.Add("c0c0c0", "silver");
            ht.Add("fffafa", "snow");
            ht.Add("d2b48c", "tan");
            ht.Add("008080", "teal");
            ht.Add("ff6347", "tomato");
            ht.Add("ee82ee", "violet");
            ht.Add("f5deb3", "wheat");

            return ht;
        }

        /// <summary>
        /// Initialises hex hash tables
        /// </summary>
        private static Hashtable CreateHexTable()
        {
            Hashtable ht = new Hashtable();

            // Hex notation shorter than named value
            ht.Add("black", "#000");
            ht.Add("fuchsia", "#f0f");
            ht.Add("lightSlategray", "#789");
            ht.Add("lightSlategrey", "#789");
            ht.Add("magenta", "#f0f");
            ht.Add("white", "#fff");
            ht.Add("yellow", "#ff0");

            return ht;
        }

        /// <summary>
        /// Searches for properties specifying all the individual properties of a property type 
        /// and reduces it to a single property use shorthand notation.
        /// </summary>
        /// <param name="css">CSS to modify</param>
        /// <returns>Short-handed code</returns>
        private static string ShortHandAllProperties(string css)
        {
            Regex reCSSBlock = new Regex("{[^{}]*}");
            Regex reTRBL1 = new Regex(@"(?<fullProperty>(?:(?<property>padding)-(?<position>top|right|bottom|left)))\s*:\s*(?<unit>[\w.]+);?", RegexOptions.IgnoreCase);
            Regex reTRBL2 = new Regex(@"(?<fullProperty>(?:(?<property>margin)-(?<position>top|right|bottom|left)))\s*:\s*(?<unit>[\w.]+);?", RegexOptions.IgnoreCase);
            Regex reTRBL3 = new Regex(@"(?<fullProperty>(?<property>border)-(?<position>top|right|bottom|left)(?<property2>-(?:color)))\s*:\s*(?<unit>[#\w.]+);?", RegexOptions.IgnoreCase);
            Regex reTRBL4 = new Regex(@"(?<fullProperty>(?<property>border)-(?<position>top|right|bottom|left)(?<property2>-(?:style)))\s*:\s*(?<unit>none|hidden|d(?:otted|ashed|ouble)|solid|groove|ridge|inset|outset);?", RegexOptions.IgnoreCase);
            Regex reTRBL5 = new Regex(@"(?<fullProperty>(?<property>border)-(?<position>top|right|bottom|left)(?<property2>-(?:width)))\s*:\s*(?<unit>[\w.]+);?", RegexOptions.IgnoreCase);
            Regex reListStyle = new Regex(@"list-style-(?<style>type|image|position)\s*:\s*(?<unit>[^};]+);?", RegexOptions.IgnoreCase);
            Regex reFont = new Regex(@"font-(?:(?:(?<fontProperty>family\b)\s*:\s*(?<fontPropertyValue>(?:\b[a-zA-Z]+(-[a-zA-Z]+)?\b|\x22[^\x22]+\x22)(?:\s*,\s*(?:\b[a-zA-Z]+(-[a-zA-Z]+)?\b|\x22[^\x22]+\x22))*)\b)|
(?:(?<fontProperty>style\b)\s*:\s*(?<fontPropertyValue>normal|italic|oblique|inherit))|
(?:(?<fontProperty>variant\b)\s*:\s*(?<fontPropertyValue>normal|small-caps|inherit))|
(?:(?<fontProperty>weight\b)\s*:\s*(?<fontPropertyValue>normal|bold|(?:bold|light)er|[1-9]00|inherit))|
(?:(?<fontProperty>size\b)\s*:\s*(?<fontPropertyValue>(?:(?:xx?-)?(?:small|large))|medium|(?:\d*\.?\d+(?:%|(p(?:[xct])|(?:[cem])m|in|ex))\b)|inherit|\b0\b)))\s*;?", (RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace));
            Regex reBackGround = new Regex(@"background-(?:
(?:(?<property>color)\s*:\s*(?<unit>transparent|inherit|(?:(?:\#(?:[0-9A-F]{3}){1,2})|\S+)))|
(?:(?<property>image)\s*:\s*(?<unit>none|inherit|(?:url\s*\([^()]+\))))|
(?:(?<property>repeat)\s*:\s*(?<unit>no-repeat|inherit|repeat(?:-[xy])))|
(?:(?<property>attachment)\s*:\s*(?<unit>scroll|inherit|fixed))|
(?:(?<property>position)\s*:\s*(?<unit>((?<horizontal>left | center | right|(?:0|(?:(?:\d*\.?\d+(?:p(?:[xct])|(?:[cem])m|%|in|ex)))))\s+(?<vertical>top | center | bottom |(?:0|(?:(?:\d*\.?\d+(?:p(?:[xct])|(?:[cem])m|%|in|ex))))))|
    ((?<vertical>top | center | bottom )\s+(?<horizontal>left | center | right ))|
    ((?<horizontal>left | center | right )|(?<vertical>top | center | bottom ))))
);?", (RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture));
            MatchCollection mcBlocks = reCSSBlock.Matches(css);
            foreach (Match mBlock in mcBlocks)
            {
                string strBlock = mBlock.Value;
                HasAllPositions(reTRBL1, ref strBlock);
                HasAllPositions(reTRBL2, ref strBlock);
                HasAllPositions(reTRBL3, ref strBlock);
                HasAllPositions(reTRBL4, ref strBlock);
                HasAllPositions(reTRBL5, ref strBlock);
                HasAllListStyle(reListStyle, ref strBlock);
                HasAllFontProperties(reFont, ref strBlock);
                HasAllBackGroundProperties(reBackGround, ref strBlock);
                css = css.Replace(mBlock.Value, strBlock);
            }
            return css;
        }

        private static void HasAllBackGroundProperties(Regex re, ref string CSSText)
        {
            {
                MatchCollection mcProperySet = re.Matches(CSSText);
                const int z = 5;
                if (mcProperySet.Count == z)
                {

                    int y = 0;
                    for (int x = 0; x < z; x = x + 1)
                    {
                        switch (mcProperySet[x].Groups["property"].Value)
                        {
                            case "color":
                                y = y + 1;
                                break;
                            case "image":
                                y = y + 2;
                                break;
                            case "repeat":
                                y = y + 4;
                                break;
                            case "attachment":
                                y = y + 8;
                                break;
                            case "position":
                                y = y + 16;
                                break;
                        }
                    }
                    if (y == 31)
                    {
                        CSSText = ShortHandBackGroundReplaceV2(mcProperySet, re, CSSText);
                    }
                }
            }
        }

        private static void HasAllFontProperties(Regex re, ref string CSSText)
        {
            {
                MatchCollection mcProperySet = re.Matches(CSSText);
                const int z = 5;
                if (mcProperySet.Count == z)
                {

                    int y = 0;
                    for (int x = 0; x < z; x = x + 1)
                    {
                        switch (mcProperySet[x].Groups["fontProperty"].Value)
                        {
                            case "style":
                                y = y + 1;
                                break;
                            case "variant":
                                y = y + 2;
                                break;
                            case "weight":
                                y = y + 4;
                                break;
                            case "size":
                                y = y + 8;
                                break;
                            case "family":
                                y = y + 16;
                                break;
                        }
                    }
                    if (y == 31)
                    {
                        CSSText = ShortHandFontReplaceV2(mcProperySet, re, CSSText);
                    }
                }
            }
        }

        private static void HasAllListStyle(Regex re, ref string CSSText)
        {
            {
                const int z = 3;
                MatchCollection mcProperySet = re.Matches(CSSText);
                if (mcProperySet.Count == z)
                {

                    int y = 0;
                    for (int x = 0; x < z; x = x + 1)
                    {
                        switch (mcProperySet[x].Groups["style"].Value)
                        {
                            case "type":
                                y = y + 1;
                                break;
                            case "image":
                                y = y + 2;
                                break;
                            case "position":
                                y = y + 4;
                                break;

                        }
                    }
                    if (y == 7)
                    {
                        CSSText = ShortHandListReplaceV2(mcProperySet, re, CSSText);
                    }
                }
            }
        }

        private static void HasAllPositions(Regex re, ref string CSSText)
        {
            {
                MatchCollection mcProperySet = re.Matches(CSSText);
                if (mcProperySet.Count == 4)
                {

                    int y = 0;
                    for (int x = 0; x < 4; x = x + 1)
                    {
                        switch (mcProperySet[x].Groups["position"].Value)
                        {
                            case "top":
                                y = y + 1;
                                break;
                            case "right":
                                y = y + 2;
                                break;
                            case "bottom":
                                y = y + 4;
                                break;
                            case "left":
                                y = y + 8;
                                break;
                        }
                    }
                    if (y == 15)
                    {
                        CSSText = ShortHandReplaceV2(mcProperySet, re, CSSText);
                    }
                }
            }
        }

        /// <summary>
        /// Replaces individual font properties with a single entry
        /// </summary>
        /// <param name="mcProperySet"></param>
        /// <param name="re"></param>
        /// <param name="InputText"></param>
        /// <returns></returns>
        private static string ShortHandFontReplaceV2(MatchCollection mcProperySet, Regex re, string InputText)
        {
            Regex reLineHeight = new Regex(@"line-height\s*:\s*((?:\d*\.?\d+(?:%|(p(?:[xct])|(?:[cem])m|in|ex)\b)?)|normal|inherit);?", RegexOptions.IgnoreCase);
            string strVariant = string.Empty;
            string strWeight = string.Empty;
            string strSize = string.Empty;
            string strStyle_Variant_Weight = string.Empty;
            foreach (Match mProperty in mcProperySet)
            {
                switch (mProperty.Groups[""].Value)
                {
                    case "family":
                        break;
                    case "size":
                        if (reLineHeight.IsMatch(InputText))
                        {
                            Match m = reLineHeight.Match(InputText);
                            if (m.Groups[1].Value != "normal")
                            {
                                strSize = String.Format("/{0}", m.Groups[1].Value);
                            }
                            InputText = reLineHeight.Replace(InputText, string.Empty);
                        }
                        strSize = string.Format(" {0}{1}", mProperty.Groups["fontPropertyValue"].Value, strSize);
                        if (strSize == "medium")
                        {
                            strSize = string.Empty;
                        }
                        break;
                    case "style":
                    case "variant":
                    case "weight":
                        if (mProperty.Groups["fontPropertyValue"].Value != "normal")
                        {
                            strStyle_Variant_Weight += string.Format(" {0}", mProperty.Groups["fontPropertyValue"].Value);
                        } break;

                }
            }

            string strProperties = string.Format("{0}{1}{2};", strStyle_Variant_Weight, strVariant, strWeight);
            string strShortcut = string.Format("font:{0}", strProperties.Trim());
            string strNewBlock = re.Replace(InputText, "");
            strNewBlock = strNewBlock.Insert(1, strShortcut);
            return strNewBlock;
        }

        /// <summary>
        /// Replaces the individual background properties with a single entry.
        /// </summary>
        /// <param name="mcProperySet"></param>
        /// <param name="re"></param>
        /// <param name="InputText"></param>
        /// <returns></returns>
        private static string ShortHandBackGroundReplaceV2(MatchCollection mcProperySet, Regex re, string InputText)
        {
            string strColor = string.Empty;
            string strImage = string.Empty;
            string strRepeat = string.Empty;
            string strAttachment = string.Empty;
            string strPosition = string.Empty;
            foreach (Match mProperty in mcProperySet)
            {
                switch (mProperty.Groups["property"].Value)
                {
                    case "color":
                        if (mProperty.Groups["unit"].Value != "transparent")
                        {
                            strColor = string.Format(" {0}", mProperty.Groups["unit"].Value);
                        }
                        break;
                    case "image":
                        if (mProperty.Groups["unit"].Value != "none")
                        {
                            strImage = string.Format(" {0}", mProperty.Groups["unit"].Value);
                        }
                        break;
                    case "repeat":
                        if (mProperty.Groups["unit"].Value != "repeat")
                        {
                            strRepeat = string.Format(" {0}", mProperty.Groups["unit"].Value);
                        } break;
                    case "attachment":
                        if (mProperty.Groups["unit"].Value != "scroll")
                        {
                            strAttachment = string.Format(" {0}", mProperty.Groups["unit"].Value);
                        }
                        break;
                    case "position":
                        if (mProperty.Groups["unit"].Value != "0% 0%")
                        {
                            strPosition = string.Format(" {0}", mProperty.Groups["unit"].Value);
                        }
                        break;
                }
            }

            string strProperties = string.Format("{0}{1}{2}{3}{4};", strColor, strImage, strRepeat, strAttachment, strPosition);
            string strShortcut = string.Format("background:{0}", strProperties.Trim());
            string strNewBlock = re.Replace(InputText, "");
            strNewBlock = strNewBlock.Insert(1, strShortcut);
            return strNewBlock;
        }

        /// <summary>
        /// Replace method for regexes used in ShortHand property method for properties with top, right, bottom and left sub properties.
        /// </summary>
        /// <param name="mcProperySet"></param>
        /// <param name="reTRBL1"></param>
        /// <param name="InputText"></param>
        /// <returns></returns>
        private static string ShortHandReplaceV2(MatchCollection mcProperySet, Regex reTRBL1, string InputText)
        {
            string strTop = string.Empty;
            string strRight = string.Empty;
            string strBottom = string.Empty;
            string strLeft = string.Empty;
            string strProperty = string.Format("{0}{1}", mcProperySet[0].Groups["property"].Value, mcProperySet[0].Groups["property2"].Value);
            foreach (Match mProperty in mcProperySet)
            {
                switch (mProperty.Groups["position"].Value)
                {
                    case "top":
                        strTop = mProperty.Groups["unit"].Value;
                        break;
                    case "right":
                        strRight = mProperty.Groups["unit"].Value;
                        break;
                    case "bottom":
                        strBottom = mProperty.Groups["unit"].Value;
                        break;
                    case "left":
                        strLeft = mProperty.Groups["unit"].Value;
                        break;
                }

            }

            string strShortcut = string.Format("{0}:{1} {2} {3} {4};", strProperty, strTop, strRight, strBottom, strLeft);
            string strNewBlock = reTRBL1.Replace(InputText, "");
            strNewBlock = strNewBlock.Insert(1, strShortcut);
            return strNewBlock;
        }

        /// <summary>
        /// Replaces the individual list properties with a single entry
        /// </summary>
        /// <param name="mcProperySet"></param>
        /// <param name="re"></param>
        /// <param name="InputText"></param>
        /// <returns></returns>
        private static string ShortHandListReplaceV2(MatchCollection mcProperySet, Regex re, string InputText)
        {
            string strType = string.Empty;
            string strPosition = string.Empty;
            string strImage = string.Empty;
            foreach (Match mProperty in mcProperySet)
            {
                switch (mProperty.Groups["style"].Value)
                {
                    case "type":
                        if (mProperty.Groups["unit"].Value != "disc")
                        {
                            strType = mProperty.Groups["unit"].Value;
                        }
                        break;
                    case "position":
                        if (mProperty.Groups["unit"].Value != "outside")
                        {
                            strPosition = string.Format(" {0}", mProperty.Groups["unit"].Value);
                        }
                        break;
                    case "style":
                        if (mProperty.Groups["unit"].Value != "none")
                        {
                            strImage = string.Format(" {0}", mProperty.Groups["unit"].Value);
                        }
                        break;
                }

            }

            string strShortcut = string.Format("list-style:{0}{1}{2};", strType, strPosition, strImage);
            string strNewBlock = re.Replace(InputText, "");
            strNewBlock = strNewBlock.Insert(1, strShortcut);
            return strNewBlock;
        }

        #endregion Private methods

    }
}
