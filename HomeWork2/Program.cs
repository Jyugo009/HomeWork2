using System.Text;
using System.Text.RegularExpressions;

namespace HomeWork2
{
    internal class Program
    {
        private static readonly string key = "mySecretKey";
        static void Main(string[] args)
        {
            //Task 1

            string textExample = "Have a good day!";

            Console.WriteLine($"Your text reversed:{TextReverse(textExample)}");

            //Task 2

            string input = "This assistant is so fucking!!!! good, i like it! A Two tea from me!";

            var badWords = new HashSet<string> { "cunt", "suck", "ass", "idiot", "fuck", "fucking", "reee"  };

            var matches = Regex.Matches(input, @"\w+|\W+");

            string[] censoredWords = new string[matches.Count];

            for (int i = 0; i < matches.Count; i++)
            {
                var word = matches[i].Value;
               
                if (Regex.IsMatch(word, @"^\w+$") && badWords.Contains(word))
                {
                    censoredWords[i] = new string('*', word.Length);
                }
                else
                {
                    censoredWords[i] = word;
                }
            }

            string result = String.Join("", censoredWords);

            Console.WriteLine(result);

            //Task 3

            int numberOfChars = 10;

            string randomChars = RandomCharGenerator(numberOfChars);

            Console.WriteLine(randomChars);

            //Task 4

            int[] exampleArray = { 0, 1, 2, 4, 5 };

            int missingNumber = LookingForHole(exampleArray);

            Console.WriteLine($"The hole in the array is: {missingNumber}");

            //Task 5

            string dna = "AAACCGTTTGGGA";

            string compressedDna = Compress(dna);

            string decompressedDna = Decompress(compressedDna);

            Console.WriteLine($"Original DNA: {dna}");

            Console.WriteLine($"Compressed DNA: {compressedDna}");

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

        static string Compress(string dna)
        {
            StringBuilder compressed = new StringBuilder();
            int count = 1;

            for (int i = 1; i < dna.Length; i++)
            {
                if (dna[i] == dna[i - 1])
                {
                    count++;
                }
                else
                {
                    compressed.Append(dna[i - 1]);
                    if (count > 1) compressed.Append(count);
                    count = 1;
                }
            }

            compressed.Append(dna[dna.Length - 1]);
            if (count > 1) compressed.Append(count);

            return compressed.ToString();
        }

        static string Decompress(string compressedDna)
        {
            StringBuilder decompressed = new StringBuilder();

            for (int i = 0; i < compressedDna.Length; i++)
            {
                char nucleotide = compressedDna[i];
                if (Char.IsLetter(nucleotide))
                {
                    decompressed.Append(nucleotide);
                }
                else
                {
                    int repeatCount = int.Parse(compressedDna[i].ToString());
                    char lastNucleotide = decompressed[decompressed.Length - 1];
                    decompressed.Append(lastNucleotide, repeatCount - 1);
                }
            }

            return decompressed.ToString();
        }

        static string Encrypt(string text)
        {
            StringBuilder encrypted = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                char character = (char)(text[i] ^ key[i % key.Length]);
                encrypted.Append(character);
            }

            return encrypted.ToString();
        }

        static string Decrypt(string text)
        {
            return Encrypt(text);
        }
    }
}