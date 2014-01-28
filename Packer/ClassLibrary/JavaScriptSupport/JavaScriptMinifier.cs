using System;
using System.IO;

namespace SmallSharpTools.Packer.Utilities.JavaScriptSupport
{
    /// <summary>
    /// Minifies a JavaScript file stripping out unneccessary white space and comments etc.
    /// </summary>
    /// <remarks>
    /// jsmin.c
    /// 2007-01-08
    /// Copyright (c) 2002 Douglas Crockford  (www.crockford.com)
    ///
    /// Permission is hereby granted, free of charge, to any person obtaining a copy of
    /// this software and associated documentation files (the "Software"), to deal in
    /// the Software without restriction, including without limitation the rights to
    /// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
    /// of the Software, and to permit persons to whom the Software is furnished to do
    /// so, subject to the following conditions:
    /// 
    /// The above copyright notice and this permission notice shall be included in all
    /// copies or substantial portions of the Software.
    /// 
    /// The Software shall be used for Good, not Evil.
    /// 
    /// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    /// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    /// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    /// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    /// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    /// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    /// SOFTWARE.
    ///
    /// Originally written in 'C', this code has been converted to the C# language.
    /// The author's copyright message is reproduced below.
    /// All modifications from the original to C# are placed in the public domain.
    /// 
    /// This version has been refactored converted to static library method.
    /// </remarks>
    public static class JavaScriptMinifier
    {
        #region Constants

        private const int EOF = -1;

        #endregion Constants

        #region Private fields

        private static StringReader _sr;
        private static StringWriter _sw;
        private static int _theA;
        private static int _theB;
        private static int _theLookahead = EOF;

        #endregion Private fields

        #region Enumerations

        /// <summary>
        /// 1   Output A. Copy B to A. Get the next B.
        /// 2   Copy B to A. Get the next B. (Delete A).
        /// 3   Get the next B. (Delete B).
        /// </summary>
        private enum ActionTask
        {
            OutputA = 1,
            CopyBtoA = 2,
            GetNextB = 3
        }

        #endregion Enumerations

        #region Public methods

        /// <summary>
        /// Minifies a script
        /// </summary>
        /// <param name="script">Script to minify</param>
        /// <returns>Minified script</returns>
        public static string Minify(string script)
        {
            string output;
            using (_sr = new StringReader(script))
                using (_sw = new StringWriter())
                {
                    JSmin();
                    output = _sw.ToString();
                }
            
            return output;
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Copy the input to the output, deleting the characters which are
        /// insignificant to JavaScript. Comments will be removed. Tabs will be
        /// replaced with spaces. Carriage returns will be replaced with linefeeds.
        /// Most spaces and linefeeds will be removed.
        /// </summary>
        private static void JSmin()
        {
            _theA = '\n';
            Action(ActionTask.GetNextB);
            while (!_theA.Equals(EOF))
            {
                switch (_theA)
                {
                    case ' ':
                        if (IsAlphanum(_theB))
                        {
                            Action(ActionTask.OutputA);
                        }
                        else
                        {
                            Action(ActionTask.CopyBtoA);
                        }
                        break;
                    case '\n':
                        switch (_theB)
                        {
                            case '{':
                            case '[':
                            case '(':
                            case '+':
                            case '-':
                            {
                                Action(ActionTask.OutputA);
                                break;
                            }
                            case ' ':
                            {
                                Action(ActionTask.GetNextB);
                                break;
                            }
                            default:
                            {
                                if (IsAlphanum(_theB))
                                {
                                    Action(ActionTask.OutputA);
                                }
                                else
                                {
                                    Action(ActionTask.CopyBtoA);
                                }
                                break;
                            }
                        }
                        break;
                    default:
                        switch (_theB)
                        {
                            case ' ':
                            {
                                if (IsAlphanum(_theA))
                                {
                                    Action(ActionTask.OutputA);
                                    break;
                                }
                                Action(ActionTask.GetNextB);
                                break;
                            }
                            case '\n':
                            {
                                switch (_theA)
                                {
                                    case '}':
                                    case ']':
                                    case ')':
                                    case '+':
                                    case '-':
                                    case '"':
                                    case '\'':
                                    {
                                        Action(ActionTask.OutputA);
                                        break;
                                    }
                                    default:
                                    {
                                        if (IsAlphanum(_theA))
                                        {
                                            Action(ActionTask.OutputA);
                                        }
                                        else
                                        {
                                            Action(ActionTask.GetNextB);
                                        }
                                        break;
                                    }
                                }
                                break;
                            }
                            default:
                            {
                                Action(ActionTask.OutputA);
                                break;
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Do something! What you do is determined by the argument.
        /// Treats a string as a single character. Wow!
        /// Recognizes a regular expression if it is preceded by ( or , or =.
        /// </summary>
        /// <param name="d">
        /// 1   Output A. Copy B to A. Get the next B.
        /// 2   Copy B to A. Get the next B. (Delete A).
        /// 3   Get the next B. (Delete B).
        /// </param>
        private static void Action(ActionTask d)
        {
            if (d.Equals(ActionTask.OutputA))
            {
                Put(_theA);
            }
            if (d.Equals(ActionTask.OutputA) || d.Equals(ActionTask.CopyBtoA))
            {
                _theA = _theB;
                if (_theA.Equals('\'') || _theA.Equals('"'))
                {
                    for (;;)
                    {
                        Put(_theA);
                        _theA = Get();
                        if (_theA == _theB)
                        {
                            break;
                        }
                        if (_theA <= '\n')
                        {
                            throw new Exception(string.Format("Error: JSMIN unterminated string literal: {0}\n", _theA));
                        }
                        if (_theA.Equals('\\'))
                        {
                            Put(_theA);
                            _theA = Get();
                        }
                    }
                }
            }
            //if (d.Equals(ActionTask.OutputA) || d.Equals(ActionTask.CopyBtoA) || d.Equals(ActionTask.GetNextB))
            //{
            _theB = Next();
            if (_theB.Equals('/') && (_theA.Equals('(') || _theA.Equals(',') || _theA.Equals('=') ||
                                      _theA.Equals('[') || _theA.Equals('!') || _theA.Equals(':') ||
                                      _theA.Equals('&') || _theA.Equals('|') || _theA.Equals('?')))
            {
                Put(_theA);
                Put(_theB);
                for (;;)
                {
                    _theA = Get();
                    if (_theA.Equals('/'))
                    {
                        break;
                    }
                    else if (_theA.Equals('\\'))
                    {
                        Put(_theA);
                        _theA = Get();
                    }
                    else if (_theA <= '\n')
                    {
                        throw new Exception(string.Format("Error: JSMIN unterminated Regular Expression literal : {0}.\n", _theA));
                    }
                    Put(_theA);
                }
                _theB = Next();
            }
            //}
        }

        /// <summary>
        /// Get the next character, excluding comments. Peek() is used to see
        /// if a '/' is followed by a '/' or '*'.
        /// </summary>
        /// <returns>Next character</returns>
        private static int Next()
        {
            int c = Get();
            if (c.Equals('/'))
            {
                switch (Peek())
                {
                    case '/':
                        for (;;)
                        {
                            c = Get();
                            if (c <= '\n')
                            {
                                return c;
                            }
                        }
                    case '*':
                        Get();
                        for (;;)
                        {
                            switch (Get())
                            {
                                case '*':
                                {
                                    if (Peek().Equals('/'))
                                    {
                                        Get();
                                        return ' ';
                                    }
                                    break;
                                }
                                case EOF:
                                {
                                    throw new Exception("Error: JSMIN Unterminated comment.\n");
                                }
                            }
                        }
                    default:
                        return c;
                }
            }
            return c;
        }

        /// <summary>
        /// Get the next character without getting it.
        /// </summary>
        /// <returns></returns>
        private static int Peek()
        {
            _theLookahead = Get();
            return _theLookahead;
        }
        
        /// <summary>
        /// Return the next character from stdin. Watch out for lookahead. If
        /// the character is a control character, translate it to a space or
        /// linefeed.
        /// </summary>
        /// <returns></returns>
        private static int Get()
        {
            int c = _theLookahead;
            _theLookahead = EOF;
            if (c.Equals(EOF))
            {
                c = _sr.Read();
            }
            if (c >= ' ' || c.Equals('\n') || c.Equals(EOF))
            {
                return c;
            }
            if (c.Equals('\r'))
            {
                return '\n';
            }
            return ' ';
        }

        /// <summary>
        /// Write character to stream writer
        /// </summary>
        /// <param name="c"></param>
        private static void Put(int c)
        {
            _sw.Write((char)c);
        }
        
        /// <summary>
        /// Return true if the character is a letter, digit, underscore,
        /// dollar sign, or non-ASCII character.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private static bool IsAlphanum(int c)
        {
            return ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || 
                    c.Equals('_') || c.Equals('$') || c.Equals('\\') || c > 126);
        }

        #endregion Private methods
    }
}