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

        // Найти слова заданной длины в вопросительных предложениях
        public static List<string> FindWordsInQuestions(Text text, int length)
        {
            // автоматически убирает дубликаты
            HashSet<string> uniqueWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var sentence in text.Sentences)
            {
                if (sentence.IsQuestion())
                {
                    
                    foreach (var word in sentence.GetWords())
                    {
                        if (word.Value.Length == length)
                        {
                            uniqueWords.Add(word.Value.ToLower());
                        }
                    }
                }
            }

            return uniqueWords.ToList();
        }

        // Удалить слова заданной длины, начинающиеся с согласной буквы
        public static Text RemoveWordsStartingWithConsonant(Text text, int length)
        {
            Text newText = new Text();
            string consonants = "бвгджзйклмнпрстфхцчшщbcdfghjklmnpqrstvwxz"; // согласные

            foreach (var sentence in text.Sentences)
            {
                Sentence newSentence = new Sentence();

                foreach (var token in sentence.Tokens)
                {
                    bool shouldRemove = false;

                    if (token is Word word)
                    {
                        if (word.Value.Length == length && word.Value.Length > 0)
                        {
                            char firstChar = char.ToLower(word.Value[0]);
                            if (consonants.Contains(firstChar))
                            {
                                shouldRemove = true;
                            }
                        }
                    }

                    // Если слово не нужно удалять, добавляем его в новое предложение
                    if (!shouldRemove)
                    {
                        newSentence.AddToken(token);
                    }
                }

                // Если после удаления в предложении остались токены, добавляем его в текст
                if (newSentence.Tokens.Count > 0)
                {
                    newText.AddSentence(newSentence);
                }
            }

            return newText;
        }
    }

}