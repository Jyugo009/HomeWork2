using Microsoft.VisualBasic;
using System.Security.Cryptography;
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

            string dna = "ACGT";

            byte[] compressedDna = Compress(dna);

            Console.WriteLine("Compressed DNA:");

            foreach (byte b in compressedDna)
            {
                Console.Write($"{b} ");
            }

            Console.WriteLine();

            string decompressedDna = Decompress(compressedDna);

            Console.WriteLine("Decompressed DNA:");

            Console.WriteLine(decompressedDna);

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


        public static byte[] Compress(string dna)
        {
            byte[] bytes = new byte[dna.Length];

            for (int i = 0; i < dna.Length; i++)
            {
                switch (dna[i])
                {
                    case 'A':
                        bytes[i] = 0;
                        break;
                    case 'C':
                        bytes[i] = 1;
                        break;
                    case 'G':
                        bytes[i] = 2;
                        break;
                    case 'T':
                        bytes[i] = 3;
                        break;
                    default:
                        throw new ArgumentException("Invalid nucleotide.");
                }
            }

            return bytes;
        }

        public static string Decompress(byte[] compressedDna)
        {
            char[] dnaChars = new char[compressedDna.Length];

            for (int i = 0; i < compressedDna.Length; i++)
            {
                switch (compressedDna[i])
                {
                    case 0:
                        dnaChars[i] = 'A';
                        break;
                    case 1:
                        dnaChars[i] = 'C';
                        break;
                    case 2:
                        dnaChars[i] = 'G';
                        break;
                    case 3:
                        dnaChars[i] = 'T';
                        break;
                    default:
                        throw new ArgumentException("Invalid compressed nucleotide value.");
                }
            }

            return new string(dnaChars);
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
