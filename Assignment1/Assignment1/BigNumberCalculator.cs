using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Assignment1
{
    public class BigNumberCalculator
    {
        public EMode Mode { get; set; }
        public int BitCount { get; set; }
        public BigNumberCalculator(int bitCount, EMode mode)
        {
            Mode = mode;
            BitCount = bitCount;
        }

        public static string GetOnesComplementOrNull(string num)
        {
            if (num.Length < 3)
            {
                return null;
            }
            string userInputFormat = num.Substring(0, 2);
            string userInputBinary = num.Substring(2);

            if (userInputFormat != "0b")
            {
                return null;
            }
            /*if (userInputBinary == null || userInputBinary == "")
            {
                return null;
            }*/
            char[] userInputBinaryInCharArr = userInputBinary.ToCharArray();

            for (int i = 0; i < userInputBinaryInCharArr.Length; i++)
            {
                if (userInputBinaryInCharArr[i] == '0')
                {
                    userInputBinaryInCharArr[i] = '1';
                }
                else if (userInputBinaryInCharArr[i] == '1')
                {
                    userInputBinaryInCharArr[i] = '0';
                }
                else
                {
                    return null;
                }
            }

            return userInputFormat + new string(userInputBinaryInCharArr);
        }

        public static string GetTwosComplementOrNull(string num)
        {
            string userInput = GetOnesComplementOrNull(num);
            
            if (userInput == null)
            {
                return null;
            }
            string userInputFormat = userInput.Substring(0, 2);
            string userInputBinary = userInput.Substring(2);
            char[] userInputBinaryInCharArr = userInputBinary.ToCharArray();

            for (int i = userInputBinaryInCharArr.Length - 1; i >= 0; i--)
            {
                if (userInputBinaryInCharArr[i] == '0')
                {
                    userInputBinaryInCharArr[i] = '1';
                    break;
                }
                else
                {
                    userInputBinaryInCharArr[i] = '0';
                }
            }
            
            return userInputFormat + new string(userInputBinaryInCharArr);
        }

        public static string ToBinaryOrNull(string num)
        {
            if (num.Length < 2)
            {
                goto integer;
            }
            string userInputFormat = num.Substring(0, 2);
            string userInputBinary = num.Substring(2);

            if (userInputFormat == "0b")
            { 
                return num;
            }

            if (userInputFormat == "0x")
            {
                return "0b" + HexToBinary(userInputBinary);
            }
            //integer
            integer:
            {
                int quotient;
                int input = int.Parse(num);

                bool bIsNegative = false;
                if (input < 0)
                {
                    bIsNegative = true;
                    input *= -1;
                }

                StringBuilder binaryFormat = new StringBuilder();
                do
                {
                    quotient = input / 2;
                    binaryFormat.Append(input % 2);
                    input = quotient;
                }
                while (quotient > 1);

                binaryFormat.Append('1');

                if (bIsNegative == false)
                {
                    char[] tempArr = binaryFormat.ToString().ToCharArray();
                    string reversedArr = null;

                    for (int i = tempArr.Length - 1; i >= 0; i--)
                    {
                        reversedArr += tempArr[i];
                    }

                    return "0b0" + reversedArr;
                }
                else
                {
                    char[] tempArr = binaryFormat.ToString().ToCharArray();
                    string reversedArr = null;

                    for (int i = tempArr.Length - 1; i >= 0; i--)
                    {
                        reversedArr += tempArr[i];
                    }

                    
                    return GetTwosComplementOrNull("0b0" + reversedArr);
                }
            }
        }

        public static string ToHexOrNull(string num)
        {
            string userInputFormat = num.Substring(0, 2);
            string userInputBinary = num.Substring(2);

            if (userInputFormat == "0x")
            {
                return num;
            }

            if (userInputFormat == "0b")
            {         
                return ChangeBinaryToHexFormat(userInputBinary);
            }

            //integer
            {
                string decToBinary = ToBinaryOrNull(num);
                string binaryFormat = decToBinary.Substring(2);

                return ChangeBinaryToHexFormat(binaryFormat);
            }
        }

        public static string ToDecimalOrNull(string num)
        {
            string userInputFormat = num.Substring(0, 2);
            string userInputBinary = num.Substring(2);

            if (userInputFormat == "0b")
            {
                char[] userInputBinaryToCharArray = userInputBinary.ToCharArray();
                int result = 0;
                int index = 0;

                if (userInputBinary[0] == '0')
                {
                    for (int i = userInputBinaryToCharArray.Length - 1; i >= 1; i--)
                    {
                        result += (userInputBinaryToCharArray[i] - '0') * (int)Math.Pow(2, index);
                        index++;
                    }
                    return result.ToString();
                }
                else
                {
                    string temp = GetTwosComplementOrNull(num);
                    char[] tempArr = temp.ToCharArray();

                    for (int i = tempArr.Length - 1; i >= 2; i--)
                    {
                        result += (tempArr[i] - '0') * (int)Math.Pow(2, index);
                        index++;
                    }
                    return (result * -1).ToString();
                }
            }
           
            if (userInputFormat == "0x")
            {
                int result = 0;
                int index = 0;
                string binaryString = HexToBinary(userInputBinary);
                char[] binaryCharArr = binaryString.ToCharArray();

               
                if (binaryString[0] == '0')
                {
                    for (int i = binaryCharArr.Length - 1; i >= 1; i--)
                    {
                        result += (binaryCharArr[i] - '0') * (int)Math.Pow(2, index);
                        index++;
                    }
                    return result.ToString();
                }
                else
                {
                    string temp = GetTwosComplementOrNull("0b" + binaryString);
                    char[] tempArr = temp.ToCharArray();

                    for (int i = tempArr.Length - 1; i >= 3; i--)
                    {
                        result += (tempArr[i] - '0') * (int)Math.Pow(2, index);
                        index++;
                    }
                    return (result * -1).ToString();
                }
            }

            //integer
            return num;
        }

        public string AddOrNull(string num1, string num2, out bool bOverflow)
        {
            int num1Int = int.Parse(ToDecimalOrNull(num1));
            int num2Int = int.Parse(ToDecimalOrNull(num2));

            int validRangePositive = (int)(Math.Pow(2, BitCount) / 2 - 1);
            int validRangeNegative = validRangePositive * -1 - 1;

            if (num1Int > validRangePositive || num1Int < validRangeNegative || num2Int > validRangePositive || num2Int < validRangeNegative)
            {
                bOverflow = false;
                return null;
            }

            int result = num1Int + num2Int;

            if (result > validRangePositive)
            {
                bOverflow = true;
                result = result - validRangePositive + validRangeNegative - 1;
                if (Mode == EMode.Decimal)
                {
                    return result.ToString();
                }
                else
                {
                    return "0b" + ToBinaryOrNull(result.ToString());
                }
            }
            else if (result < validRangeNegative)
            {
                bOverflow = true;
                result = result - validRangeNegative + validRangePositive + 1;

                if (Mode == EMode.Decimal)
                {
                    return result.ToString();
                }
                else
                {
                    return "0b" + ToBinaryOrNull(result.ToString());
                }
            }
            else
            {
                bOverflow = false;
                if (Mode == EMode.Decimal)
                {
                    return result.ToString();
                }
                else
                {
                    string result2 = ToBinaryOrNull(result.ToString());
                    int fillup = BitCount - result2.Substring(2).Length;
                    string insert = null;
                    char bitChar = result2[2] == '1' ? '1' : '0';

                    for (int i = 0; i < fillup; i++)
                    {
                        insert += bitChar;
                    }
                    if (insert == null)
                    {
                        return ToBinaryOrNull(result.ToString());
                    }

                    string formattedInput = result2.Insert(2, insert);
                    return ToBinaryOrNull(formattedInput.ToString());
                }
            }
        }
        public string SubtractOrNull(string num1, string num2, out bool bOverflow)
        {
            int num1Int = int.Parse(ToDecimalOrNull(num1));
            int num2Int = int.Parse(ToDecimalOrNull(num2));

            int validRangePositive = (int)(Math.Pow(2, BitCount) / 2 - 1);
            int validRangeNegative = validRangePositive * -1 - 1;

            if (num1Int > validRangePositive || num1Int < validRangeNegative || num2Int > validRangePositive || num2Int < validRangeNegative)
            {
                bOverflow = false;
                return null;
            }

            int result = num1Int - num2Int;
            int resultAbs = Math.Abs(num1Int) - Math.Abs(num2Int);

            if (result > validRangePositive)
            {
                bOverflow = true;
                result = result - validRangePositive + validRangeNegative - 1;
                if (Mode == EMode.Decimal)
                {
                    return result.ToString();
                }
                else
                {
                    return "0b" + ToBinaryOrNull(result.ToString());
                }
            }
            else if (result < validRangeNegative)
            {
                bOverflow = true;
                result = result - validRangeNegative + validRangePositive + 1;

                if (Mode == EMode.Decimal)
                {
                    return result.ToString();
                }
                else
                {
                    string result2 = ToBinaryOrNull(result.ToString());
                    int fillup = BitCount - result2.Substring(2).Length;

                    string insert = null;
                    char bitChar = result2[2] == '1' ? '1' : '0';

                    for (int i = 0; i < fillup; i++)
                    {
                        insert += bitChar;
                    }

                    if (insert == null)
                    {
                        return ToBinaryOrNull(result.ToString());
                    }
                    string formattedInput = result2.Insert(2, insert);
                    return ToBinaryOrNull(formattedInput.ToString());
                }
            }
            else
            {
                bOverflow = false;
                if (Mode == EMode.Decimal)
                {
                    return result.ToString();
                }
                else
                {
                    string result2 = ToBinaryOrNull(result.ToString());
                    int fillup = BitCount - result2.Substring(2).Length;

                    string insert = null;
                    char bitChar = result2[2] == '1' ? '1' : '0';

                    for (int i = 0; i < fillup; i++)
                    {
                        insert += bitChar;
                    }

                    if (insert == null)
                    {
                        return ToBinaryOrNull(result.ToString());
                    }
                    string formattedInput = result2.Insert(2, insert);
                    return ToBinaryOrNull(formattedInput.ToString());
                    //return "0b" + ToBinaryOrNull(result.ToString());
                }
            }
        }

        public static string HexToBinary(string hexInput)
        {
            StringBuilder hexToBinary = new StringBuilder(hexInput.Length * 4);

            foreach (char ch in hexInput)
            {
                switch (ch)
                {
                    case '0': 
                        hexToBinary.Append("0000");
                        break;
                    case '1':
                        hexToBinary.Append("0001");
                        break;
                    case '2':
                        hexToBinary.Append("0010");
                        break;
                    case '3':
                        hexToBinary.Append("0011");
                        break;
                    case '4':
                        hexToBinary.Append("0100");
                        break;
                    case '5':
                        hexToBinary.Append("0101");
                        break;
                    case '6':
                        hexToBinary.Append("0110");
                        break;
                    case '7':
                        hexToBinary.Append("0111");
                        break;
                    case '8':
                        hexToBinary.Append("1000");
                        break;
                    case '9':
                        hexToBinary.Append("1001");
                        break;
                    case 'A':
                        hexToBinary.Append("1010");
                        break;
                    case 'B':
                        hexToBinary.Append("1011");
                        break;
                    case 'C':
                        hexToBinary.Append("1100");
                        break;
                    case 'D':
                        hexToBinary.Append("1101");
                        break;
                    case 'E':
                        hexToBinary.Append("1110");
                        break;
                    case 'F':
                        hexToBinary.Append("1111");
                        break;
                }
            }

            return hexToBinary.ToString();
        }

        public static string ChangeBinaryToHex(string[] binaryInput)
        {
            StringBuilder binaryToHex = new StringBuilder(binaryInput.Length / 4);

            foreach (string binary in binaryInput)
            {
                switch (binary)
                {
                    case "0000":
                        binaryToHex.Append('0');
                        break;
                    case "0001":
                        binaryToHex.Append('1');
                        break;
                    case "0010":
                        binaryToHex.Append('2');
                        break;
                    case "0011":
                        binaryToHex.Append('3');
                        break;
                    case "0100":
                        binaryToHex.Append('4');
                        break;
                    case "0101":
                        binaryToHex.Append('5');
                        break;
                    case "0110":
                        binaryToHex.Append('6');
                        break;
                    case "0111":
                        binaryToHex.Append('7');
                        break;
                    case "1000":
                        binaryToHex.Append('8');
                        break;
                    case "1001":
                        binaryToHex.Append('9');
                        break;
                    case "1010":
                        binaryToHex.Append('A');
                        break;
                    case "1011":
                        binaryToHex.Append('B');
                        break;
                    case "1100":
                        binaryToHex.Append('C');
                        break;
                    case "1101":
                        binaryToHex.Append('D');
                        break;
                    case "1110":
                        binaryToHex.Append('E');
                        break;
                    case "1111":
                        binaryToHex.Append('F');
                        break;
                }
            }

            return binaryToHex.ToString();
        }

        public static string ChangeBinaryToHexFormat(string binaryInput)
        {
            
            int fillup = 4 - binaryInput.Length % 4;
            string insert = null;
            char bitChar = binaryInput[0] == '1' ? '1' : '0';
            
            for (int i = 0; i < fillup; i++)
            {
                insert += bitChar;
            }

            string formattedInput = binaryInput.Insert(0, insert);

            int chunkSize = 4;
            int stringLength = formattedInput.Length;
            string[] binaryArray = new string[stringLength / 4];
            int index = 0;

            for (int i = 0; i < stringLength; i += chunkSize)
            {
                binaryArray[index] = formattedInput.Substring(i, chunkSize);
                index++;
            }

            return "0x" + ChangeBinaryToHex(binaryArray);
        }

    }
}
