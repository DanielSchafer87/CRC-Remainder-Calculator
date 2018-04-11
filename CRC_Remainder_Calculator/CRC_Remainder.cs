using System;

/// <summary>
/// CRC Calculation Program Wrriten by Daniel Schafer
/// </summary>
namespace CRC_Remainder_Calculator
{
    class CRC_Remainder
    {
        static void Main(string[] args)
        {
            string message = "10001010000011101000";
            string polynom = "10010001111";

            //string message = "01000011100011011001";
            //string polynom = "10010001111";

            //string message = "1011001001001011";
            //string polynom = "100000111";

            //string message = "10011010";
            //string polynom = "1101";

            ///
            /// Example how to run the remainder method.
            ///
            string remainder = CalculateRemainder(message, polynom);
            Console.WriteLine("\n\nResult: " + remainder);

            Console.ReadKey();

            ///
            /// Example how to run the remainder check method.
            ///
            string result = CheckCRCMessage(message, polynom, remainder);
            Console.WriteLine("\n\nResult: " + result);

            Console.ReadKey();
        }

        /// <summary>
        /// The message is first padded with zeros corresponding to the bit length n of the polynome degree.
        /// </summary>
        /// <param name="message">message in binary</param>
        /// <param name="polynom">polynome in binary</param>
        /// <returns>the remainder</returns>
        public static string CalculateRemainder(string message, string polynom)
        {
            //Add zeros to the message according to the degree of the polynom.
            for (int i = 0; i < (polynom.Length - 1); i++) { message += 0; }

            return CRCRemainder(message, polynom);
        }

        /// <summary>
        /// The message is first padded with the remainder.
        /// If there is no mistake the method will return a string of zeros.
        /// </summary>
        /// <param name="message">message in binary</param>
        /// <param name="polynom">polynome in binary</param>
        /// <param name="remainder">remainer of the message</param>
        /// <returns>if the remainder is correct, a string of zeros will be returned</returns>
        public static string CheckCRCMessage(string message, string polynom, string remainder)
        {
            message += remainder;
            return CRCRemainder(message, polynom);
        }

        /// <summary>
        /// The algorithm acts on the bits directly above the divisor in each step.
        /// The result for that iteration is the bitwise XOR of the polynomial divisor with the bits above it. 
        /// The bits not above the divisor are simply copied directly below for that step. 
        /// The divisor is then shifted one bit to the right, 
        /// and the process is repeated until the divisor reaches the right-hand end of the input row. 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="polynom"></param>
        /// <returns></returns>
        public static string CRCRemainder(string message, string polynom)
        {
            int position = 0;
            string xorResult = string.Empty;
            string tempXORResult = string.Empty;

            //Do the first XOR between the message and the polynom.
            for (int i = 0; i < polynom.Length; i++) { tempXORResult += (message[i] ^ polynom[i]); }

            xorResult = tempXORResult;
            tempXORResult = string.Empty;

            Console.WriteLine(message);
            Console.WriteLine(polynom);
            Console.WriteLine(xorResult);

            //Run until we reach the last bit of the message.
            while ((position + xorResult.Length) != message.Length)
            {
                if (xorResult.IndexOf("1") != 0)
                {
                    //Get the amount of zeros until the first "1"
                    int limit = xorResult.IndexOf("1");
                    
                    //Remove all zeros until the first "1"
                    xorResult = xorResult.Substring(xorResult.IndexOf("1"));

                    //Add the number of zeros we removed to the new current position.
                    position += limit;

                    //Remeber the current size of result without the leading zeros.
                    int tempPosition = xorResult.Length;

                    //Complete the missing numbers from tje message to match the size of the polynom
                    tempXORResult = xorResult;
                    for (int j=0; j < limit; j++)
                    {
                        try
                        {
                            //Add bits to the result from the original message.
                            tempXORResult += message[position + tempPosition + j];
                        }
                        catch(IndexOutOfRangeException e)
                        {
                            //The message is not large enough, return the last result.
                            return xorResult;
                        }
                    }

                    xorResult = tempXORResult;
                    tempXORResult = string.Empty;
                }

                printWithSpaces(xorResult, position);

                printWithSpaces(polynom, position);

                //Do the XOR.
                for (int i = 0; i < polynom.Length; i++) { tempXORResult += (xorResult[i] ^ polynom[i]); }

                xorResult = tempXORResult;
                tempXORResult = string.Empty;

                printWithSpaces(xorResult, position);
            }

            if(xorResult.IndexOf("1") > 0)
                xorResult = xorResult.Substring(xorResult.IndexOf("1"));

            return xorResult;           
        }

        /// <summary>
        /// Print everything nicely :)
        /// </summary>
        /// <param name="valurToPrint"></param>
        /// <param name="position"></param>
        private static void printWithSpaces(string valurToPrint, int position)
        {
            for (int spaces = 0; spaces < position; spaces++)
            {
                Console.Write(" ");
            }
            Console.WriteLine(valurToPrint);
        }
    }
}
