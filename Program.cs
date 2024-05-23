using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        IList<string> words = new List<string>();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Import Words from File");
            Console.WriteLine("2. Bubble Sort words");
            Console.WriteLine("3. LINQ/Lambda sort words");
            Console.WriteLine("4. Count the Distinct Words");
            Console.WriteLine("5. Take the last 10 words");
            Console.WriteLine("6. Reverse print the words");
            Console.WriteLine("7. Get and display the words that end with 'd'");
            Console.WriteLine("8. Get and display the words that start with 'q'");
            Console.WriteLine("9. Get and display the words that are more than 3 characters long and contain the letter 'a'");
            Console.WriteLine("X. Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    words = ImportWordsFromFile("Words.txt");
                    Console.WriteLine($"{words.Count} words imported.");
                    break;
                case "2":
                    var sortedWordsBubble = MeasureExecutionTime(() => BubbleSort(new List<string>(words)));
                    Console.WriteLine("Words sorted using Bubble Sort.");
                    
                    break;
                case "3":
                    var sortedWordsLinq = MeasureExecutionTime(() => LINQSort(new List<string>(words)));
                    Console.WriteLine("Words sorted using LINQ.");
                   
                    break;
                case "4":
                    int distinctCount = CountDistinctWords(words);
                    Console.WriteLine($"{distinctCount} distinct words.");
                    break;
                case "5":
                    var lastTenWords = TakeLastTenWords(words);
                    Console.WriteLine("The last 10 words are: " + string.Join("\n", lastTenWords));
                    break;
                case "6":
                    var reversedWords = ReversePrintWords(words);
                    Console.WriteLine("Reversed words: " + string.Join(", ", reversedWords));
                    break;
                case "7":
                    var wordsEndingWithD = WordsEndingWithD(words);
                    Console.WriteLine($"Words ending with 'd': {string.Join(", ", wordsEndingWithD)} ({wordsEndingWithD.Count} words)");
                    break;
                case "8":
                    var wordsStartingWithQ = WordsStartingWithQ(words);
                    Console.WriteLine($"Words starting with 'q': {string.Join(", ", wordsStartingWithQ)} ({wordsStartingWithQ.Count} words)");
                    break;
                case "9":
                    var wordsWithA = WordsWithMoreThan3CharsAndContainA(words);
                    Console.WriteLine($"Words with more than 3 chars and contain 'a': {string.Join(", ", wordsWithA)} ({wordsWithA.Count} words)");
                    break;
                case "X":
                case "x":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static IList<string> ImportWordsFromFile(string path)
    {
        IList<string> words = new List<string>();
        try
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    foreach (var word in line.Split(new[] { ' ', '\t', ',', ';', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        words.Add(word);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"The file could not be read: {e.Message}");
        }
        return words;
    }

    static IList<string> BubbleSort(IList<string> words)
    {
        for (int i = 0; i < words.Count - 1; i++)
        {
            for (int j = 0; j < words.Count - i - 1; j++)
            {
                if (string.Compare(words[j], words[j + 1]) > 0)
                {
                    var temp = words[j];
                    words[j] = words[j + 1];
                    words[j + 1] = temp;
                }
            }
        }
        return words;
    }

    static IList<string> LINQSort(IList<string> words)
    {
        return words.OrderBy(word => word).ToList();
    }

    static int CountDistinctWords(IList<string> words)
    {
        return words.Distinct().Count();
    }

    static IList<string> TakeLastTenWords(IList<string> words)
    {
        return words.Skip(Math.Max(0, words.Count - 10)).ToList();
    }

    static IList<string> ReversePrintWords(IList<string> words)
    {
        return words.Reverse().ToList();
    }

    static IList<string> WordsEndingWithD(IList<string> words)
    {
        return words.Where(word => word.EndsWith('d')).ToList();
    }

    static IList<string> WordsStartingWithQ(IList<string> words)
    {
        return words.Where(word => word.StartsWith('q')).ToList();
    }

    static IList<string> WordsWithMoreThan3CharsAndContainA(IList<string> words)
    {
        return words.Where(word => word.Length > 3 && word.Contains('a')).ToList();
    }

    static T MeasureExecutionTime<T>(Func<T> func)
    {
        var stopwatch = Stopwatch.StartNew();
        T result = func();
        stopwatch.Stop();
        Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");
        return result;
    }
}
