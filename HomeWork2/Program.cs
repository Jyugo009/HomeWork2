using Microsoft.VisualBasic;
using System.Text;
using System.Text.RegularExpressions;

namespace HomeWork2
{
    public class Program
    {
        private static readonly string key = "onigirigiri";

        static void Main(string[] args)
        {
            //Task 1

            string textExample = "Have a good day!";

            Console.WriteLine($"Your text reversed:{TextReverse(textExample)}");

            //Task 2

            string input = "This assistant is so FuCKing!!!! good, i like it! A Two tea from me!";

            string[] exceptWords = { "cunt", "suck", "ass", "idiot", "fuck", "fucking", "reee" };

            string filteredString = Filter(input, exceptWords);

            Console.WriteLine(filteredString);

            //Task 3

            int numberOfChars = 10;

            string randomChars = RandomCharGenerator(numberOfChars);

            Console.WriteLine(randomChars);

            //Task 4

            int[] exampleArray = { 0, 1, 2, 4, 5 };

            int missingNumber = LookingForHole(exampleArray);

            Console.WriteLine($"The hole in the array is: {missingNumber}");

            //Task 5

            string originalDna = "ACGT";
            Console.WriteLine($"Original DNA: {originalDna}");

            string compressedDna = Compress(originalDna);

            Console.WriteLine($"Compressed DNA: {compressedDna}");

            string decompressedDna = Decompress(compressedDna);

            Console.WriteLine($"Decompressed DNA: {decompressedDna}");

            //Task 6

            string originalText = "Meet me at dawn, at the harbor. Just make sure you don't get tailed.";

            string encryptedText = Encrypt(originalText);

            string decryptedText = Decrypt(encryptedText);

            Console.WriteLine($"Original Text: {originalText}");

            Console.WriteLine($"Encrypted Text: {encryptedText}");

            Console.WriteLine($"Decrypted Text: {decryptedText}");




        }

        static string TextReverse(string text)
        {
            int textLenght = text.Length;

            char[] textArray = new char[textLenght];

            for (int i = 0; i < textLenght; i++)
            {
                textArray[i] = text[textLenght - 1 - i];
            }

            string reversedText = new string(textArray);

            return reversedText;
        }

        static string RandomCharGenerator(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            var stringChars = new char[length];

            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }


            return new string(stringChars);
        }

        static int LookingForHole(int[] exArray)
        {
            int n = exArray.Length;

            int total = (n * (n + 1)) / 2;

            for (int i = 0; i < n; i++)
            {
                total -= exArray[i];
            }

            return total;
        }

        public static string Compress(string dna)
        {
            StringBuilder compressedDNA = new StringBuilder();

            foreach (char nucleotide in dna)
            {
                switch (nucleotide)
                {
                    case 'A':
                        compressedDNA.Append("00");
                        break;
                    case 'C':
                        compressedDNA.Append("01");
                        break;
                    case 'G':
                        compressedDNA.Append("10");
                        break;
                    case 'T':
                        compressedDNA.Append("11");
                        break;
                    default:
                        throw new ArgumentException("Invalid nucleotide.");
                }
            }

            return compressedDNA.ToString();
        }

        public static string Decompress(string compressedDna)
        {
            if (compressedDna.Length % 2 != 0)
                throw new ArgumentException("Invalid compressed DNA length.");

            StringBuilder decompressedDNA = new StringBuilder();

            for (int i = 0; i < compressedDna.Length; i += 2)
            {
                string code = compressedDna.Substring(i, 2);

                switch (code)
                {
                    case "00":
                        decompressedDNA.Append('A');
                        break;
                    case "01":
                        decompressedDNA.Append('C');
                        break;
                    case "10":
                        decompressedDNA.Append('G');
                        break;
                    case "11":
                        decompressedDNA.Append('T');
                        break;
                    default:
                        throw new ArgumentException("Invalid DNA code.");
                }
            }

            return decompressedDNA.ToString();
        }

        static string Encrypt(string text)
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            byte[] encryptedBytes = new byte[textBytes.Length];

            for (int i = 0; i < textBytes.Length; i++)
            {
                encryptedBytes[i] = (byte)(textBytes[i] ^ key[i % key.Length]);
            }

            return Convert.ToBase64String(encryptedBytes);
        }

        static string Decrypt(string encryptedText)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] decryptedBytes = new byte[encryptedBytes.Length];

            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                decryptedBytes[i] = (byte)(encryptedBytes[i] ^ key[i % key.Length]);
            }

            return Encoding.UTF8.GetString(decryptedBytes);
        }

        public static string Filter(string textExample, string[] exceptWords)
        {
            HashSet<string> exceptWordsHash = new HashSet<string>(exceptWords, StringComparer.OrdinalIgnoreCase);

            string[] splitResult = Regex.Split(textExample, @"(\s|,|\.|!|\?)");

            for (int i = 0; i < splitResult.Length; i++)
            {
                if (exceptWordsHash.Contains(splitResult[i]))
                {
                    splitResult[i] = "***";
                }
            }

            return string.Join("", splitResult);
        }

        


    }
}
