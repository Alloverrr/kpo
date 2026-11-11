using System;

namespace TextTokenizer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Читаем текст из файла
            Text myText = TextParser.ParseFile("input.txt");

            Console.WriteLine($"Всего предложений: {myText.Sentences.Count}");

            // Выводим предложения и количество слов в них
            int i = 1;
            foreach (var sentence in myText.Sentences)
            {
                Console.WriteLine($"Предложение {i}: {sentence} (Слов: {sentence.GetWordCount()})");
                i++;
            }

            Console.ReadKey();
        }
    }
}