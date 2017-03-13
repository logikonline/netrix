//
// In order to convert some functionality to Visual C#, the Java Language Conversion Assistant
// creates "support classes" that duplicate the original functionality.  
//
// Support classes replicate the functionality of the original code, but in some cases they are 
// substantially different architecturally. Although every effort is made to preserve the 
// original architecture of the application in the converted project, the user should be aware that 
// the primary goal of these support classes is to replicate functionality, and that at times 
// the architecture of the resulting solution may differ somewhat.
//
using System;

namespace Comzept.Genesis.Tidy
{
    /// <summary>
    /// Contains conversion support elements such as classes, interfaces and static methods.
    /// </summary>
    public class SupportClass
    {
        /// <summary>
        /// The class performs token processing in strings.
        /// </summary>
        public class Tokenizer : System.Collections.IEnumerator
        {
            /// Position over the string
            private long currentPos = 0;

            /// Include demiliters in the results.
            private bool includeDelims = false;

            /// Char representation of the String to tokenize.
            private char[] chars = null;

            //The tokenizer uses the default delimiter set: the space character, the tab character, the newline character, and the carriage-return character and the form-feed character
            private string delimiters = " \t\n\r\f";

            /// <summary>
            /// Initializes a new class instance with a specified string to process
            /// </summary>
            /// <param name="source">String to tokenize</param>
            public Tokenizer(System.String source)
            {
                this.chars = source.ToCharArray();
            }

            /// <summary>
            /// Initializes a new class instance with a specified string to process
            /// and the specified token delimiters to use
            /// </summary>
            /// <param name="source">String to tokenize</param>
            /// <param name="delimiters">String containing the delimiters</param>
            public Tokenizer(System.String source, System.String delimiters)
                : this(source)
            {
                this.delimiters = delimiters;
            }


            /// <summary>
            /// Initializes a new class instance with a specified string to process, the specified token 
            /// delimiters to use, and whether the delimiters must be included in the results.
            /// </summary>
            /// <param name="source">String to tokenize</param>
            /// <param name="delimiters">String containing the delimiters</param>
            /// <param name="includeDelims">Determines if delimiters are included in the results.</param>
            public Tokenizer(System.String source, System.String delimiters, bool includeDelims)
                : this(source, delimiters)
            {
                this.includeDelims = includeDelims;
            }


            /// <summary>
            /// Returns the next token from the token list
            /// </summary>
            /// <returns>The string value of the token</returns>
            public System.String NextToken()
            {
                return NextToken(this.delimiters);
            }

            /// <summary>
            /// Returns the next token from the source string, using the provided
            /// token delimiters
            /// </summary>
            /// <param name="delimiters">String containing the delimiters to use</param>
            /// <returns>The string value of the token</returns>
            public System.String NextToken(System.String delimiters)
            {
                //According to documentation, the usage of the received delimiters should be temporary (only for this call).
                //However, it seems it is not true, so the following line is necessary.
                this.delimiters = delimiters;

                //at the end 
                if (this.currentPos == this.chars.Length)
                    throw new System.ArgumentOutOfRangeException();
                //if over a delimiter and delimiters must be returned
                else if ((System.Array.IndexOf(delimiters.ToCharArray(), chars[this.currentPos]) != -1)
                         && this.includeDelims)
                    return "" + this.chars[this.currentPos++];
                //need to get the token wo delimiters.
                else
                    return nextToken(delimiters.ToCharArray());
            }

            //Returns the nextToken wo delimiters
            private System.String nextToken(char[] delimiters)
            {
                string token = "";
                long pos = this.currentPos;

                //skip possible delimiters
                while (System.Array.IndexOf(delimiters, this.chars[currentPos]) != -1)
                    //The last one is a delimiter (i.e there is no more tokens)
                    if (++this.currentPos == this.chars.Length)
                    {
                        this.currentPos = pos;
                        throw new System.ArgumentOutOfRangeException();
                    }

                //getting the token
                while (System.Array.IndexOf(delimiters, this.chars[this.currentPos]) == -1)
                {
                    token += this.chars[this.currentPos];
                    //the last one is not a delimiter
                    if (++this.currentPos == this.chars.Length)
                        break;
                }
                return token;
            }


            /// <summary>
            /// Determines if there are more tokens to return from the source string
            /// </summary>
            /// <returns>True or false, depending if there are more tokens</returns>
            public bool HasMoreTokens()
            {
                //keeping the current pos
                long pos = this.currentPos;

                try
                {
                    this.NextToken();
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    return false;
                }
                finally
                {
                    this.currentPos = pos;
                }
                return true;
            }

            /// <summary>
            /// Remaining tokens count
            /// </summary>
            public int Count
            {
                get
                {
                    //keeping the current pos
                    long pos = this.currentPos;
                    int i = 0;

                    try
                    {
                        while (true)
                        {
                            this.NextToken();
                            i++;
                        }
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        this.currentPos = pos;
                        return i;
                    }
                }
            }

            /// <summary>
            ///  Performs the same action as NextToken.
            /// </summary>
            public System.Object Current
            {
                get
                {
                    return (Object)this.NextToken();
                }
            }

            /// <summary>
            //  Performs the same action as HasMoreTokens.
            /// </summary>
            /// <returns>True or false, depending if there are more tokens</returns>
            public bool MoveNext()
            {
                return this.HasMoreTokens();
            }

            /// <summary>
            /// Does nothing.
            /// </summary>
            public void Reset()
            {
                ;
            }
        }
        /*******************************/
        /// <summary>
        /// Receives a byte array and returns it transformed in an sbyte array
        /// </summary>
        /// <param name="byteArray">Byte array to process</param>
        /// <returns>The transformed array</returns>
        public static sbyte[] ToSByteArray(byte[] byteArray)
        {
            sbyte[] sbyteArray = null;
            if (byteArray != null)
            {
                sbyteArray = new sbyte[byteArray.Length];
                for (int index = 0; index < byteArray.Length; index++)
                    sbyteArray[index] = (sbyte)byteArray[index];
            }
            return sbyteArray;
        }

        /*******************************/
        /// <summary>
        /// Converts an array of sbytes to an array of bytes
        /// </summary>
        /// <param name="sbyteArray">The array of sbytes to be converted</param>
        /// <returns>The new array of bytes</returns>
        public static byte[] ToByteArray(sbyte[] sbyteArray)
        {
            byte[] byteArray = null;

            if (sbyteArray != null)
            {
                byteArray = new byte[sbyteArray.Length];
                for (int index = 0; index < sbyteArray.Length; index++)
                    byteArray[index] = (byte)sbyteArray[index];
            }
            return byteArray;
        }

        /// <summary>
        /// Converts a string to an array of bytes
        /// </summary>
        /// <param name="sourceString">The string to be converted</param>
        /// <returns>The new array of bytes</returns>
        public static byte[] ToByteArray(System.String sourceString)
        {
            return System.Text.UTF8Encoding.UTF8.GetBytes(sourceString);
        }

        /// <summary>
        /// Converts a array of object-type instances to a byte-type array.
        /// </summary>
        /// <param name="tempObjectArray">Array to convert.</param>
        /// <returns>An array of byte type elements.</returns>
        public static byte[] ToByteArray(System.Object[] tempObjectArray)
        {
            byte[] byteArray = null;
            if (tempObjectArray != null)
            {
                byteArray = new byte[tempObjectArray.Length];
                for (int index = 0; index < tempObjectArray.Length; index++)
                    byteArray[index] = (byte)tempObjectArray[index];
            }
            return byteArray;
        }

        /*******************************/
        /// <summary>
        /// SupportClass for the Stack class.
        /// </summary>
        public class StackSupport
        {
            /// <summary>
            /// Removes the element at the top of the stack and returns it.
            /// </summary>
            /// <param name="stack">The stack where the element at the top will be returned and removed.</param>
            /// <returns>The element at the top of the stack.</returns>
            public static System.Object Pop(System.Collections.ArrayList stack)
            {
                System.Object obj = stack[stack.Count - 1];
                stack.RemoveAt(stack.Count - 1);

                return obj;
            }
        }


        /*******************************/
        /// <summary>
        /// Converts an array of sbytes to an array of chars
        /// </summary>
        /// <param name="sByteArray">The array of sbytes to convert</param>
        /// <returns>The new array of chars</returns>
        public static char[] ToCharArray(sbyte[] sByteArray)
        {
            return System.Text.UTF8Encoding.UTF8.GetChars(ToByteArray(sByteArray));
        }

        /// <summary>
        /// Converts an array of bytes to an array of chars
        /// </summary>
        /// <param name="byteArray">The array of bytes to convert</param>
        /// <returns>The new array of chars</returns>
        public static char[] ToCharArray(byte[] byteArray)
        {
            return System.Text.UTF8Encoding.UTF8.GetChars(byteArray);
        }

    }
}