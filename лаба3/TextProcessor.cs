using System;
using System.Collections.Generic;
using System.Linq;

namespace TextTokenizer
{
    public class TextProcessor
    {
        // Сортировка по количеству слов (по возрастанию)
        public static List<Sentence> SortByWordCount(Text text)
        {
            // OrderBy сортирует по возрастанию
            return text.Sentences.OrderBy(s => s.GetWordCount()).ToList();
        }

        // Сортировка по длине предложения (по возрастанию)
        public static List<Sentence> SortByLength(Text text)
        {
            // Сортируем по длине (сумме символов во всех токенах)
            return text.Sentences.OrderBy(s => s.GetLength()).ToList();
        }
    }
}