using System;

namespace TextTokenizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Text myText = TextParser.ParseFile("input.txt");
            
            Console.WriteLine("=== 5. Замена слов длины 3 на '[REPLACED]' во 2-м предложении ===");
            Text replacedText = TextProcessor.ReplaceWordsInSentence(myText, 1, 3, "[REPLACED]");
            Console.WriteLine(replacedText.Sentences[1]); // Выводим только измененное предложение
            Console.WriteLine();

            Console.WriteLine("=== 6. Текст без стоп-слов (рус/англ) ===");
            // В файле stopwords_ru.txt только русские слова. 
            // Если хочешь проверить на английском, можешь создать stopwords_en.txt с: a, the, was, is, it, etc.
            Text noStopWordsText = TextProcessor.RemoveStopWords(myText, "stopwords_ru.txt");
            Console.WriteLine(noStopWordsText);

            Console.ReadKey();
        }
    }
}