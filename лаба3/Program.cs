using System;

namespace TextTokenizer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Читаем текст
            Text myText = TextParser.ParseFile("input.txt");

            Console.WriteLine("=== 1. Сортировка по количеству слов ===");
            foreach (var sentence in TextProcessor.SortByWordCount(myText))
                Console.WriteLine($"[{sentence.GetWordCount()}] {sentence}");
            Console.WriteLine();

            Console.WriteLine("=== 2. Сортировка по длине ===");
            foreach (var sentence in TextProcessor.SortByLength(myText))
                Console.WriteLine($"[{sentence.GetLength()}] {sentence}");
            Console.WriteLine();

            Console.WriteLine("=== 3. Слова длины 4 в вопросах ===");
            foreach (var word in TextProcessor.FindWordsInQuestions(myText, 4))
                Console.WriteLine(word);
            Console.WriteLine();

            Console.WriteLine("=== 4. Удаление слов длины 3 на согласную ===");
            Console.WriteLine(TextProcessor.RemoveWordsStartingWithConsonant(myText, 3));
            Console.WriteLine();

            Console.WriteLine("=== 5. Замена слов длины 4 во 2-м предложении ===");
            Console.WriteLine(TextProcessor.ReplaceWordsInSentence(myText, 1, 4, "[REPLACED]").Sentences[1]);
            Console.WriteLine();

            Console.WriteLine("=== 6. Удаление стоп-слов ===");
            Console.WriteLine(TextProcessor.RemoveStopWords(myText, "stopwords_ru.txt"));
            Console.WriteLine();

            Console.WriteLine("=== 7. Экспорт в XML ===");
            TextProcessor.ExportToXml(myText, "output.xml");
            Console.WriteLine("Экспорт завершен!");

            Console.ReadKey();
        }
    }
}