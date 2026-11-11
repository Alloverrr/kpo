using System;
using System.Collections.Generic;

namespace TextTokenizer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Читаем текст
            Text myText = TextParser.ParseFile("input.txt");

            // Сортировка по количеству слов
            Console.WriteLine("=== 1. Предложения по возрастанию количества слов ===");
            List<Sentence> sortedByWords = TextProcessor.SortByWordCount(myText);
            foreach (var sentence in sortedByWords)
            {
                Console.WriteLine($"[{sentence.GetWordCount()} слов] {sentence}");
            }
            Console.WriteLine();

            // Сортировка по длине предложения
            Console.WriteLine("=== 2. Предложения по возрастанию длины ===");
            List<Sentence> sortedByLength = TextProcessor.SortByLength(myText);
            foreach (var sentence in sortedByLength)
            {
                Console.WriteLine($"[{sentence.GetLength()} символов] {sentence}");
            }

            Console.ReadKey();
        }
    }
}