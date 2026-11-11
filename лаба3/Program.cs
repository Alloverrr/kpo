using System;
using System.Collections.Generic;

namespace TextTokenizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Text myText = TextParser.ParseFile("input.txt");

            Console.WriteLine("=== 3. Слова длины 4 в вопросительных предложениях ===");
            var wordsInQuestions = TextProcessor.FindWordsInQuestions(myText, 4);
            foreach (var word in wordsInQuestions)
            {
                Console.WriteLine(word);
            }
            Console.WriteLine();

            Console.WriteLine("=== 4. Текст после удаления слов длины 3 на согласную ===");
            // Возьмем текст из 2-х предложений для теста
            Text filteredText = TextProcessor.RemoveWordsStartingWithConsonant(myText, 3);
            foreach (var sentence in filteredText.Sentences)
            {
                Console.WriteLine(sentence);
            }

            Console.ReadKey();
        }
    }
}