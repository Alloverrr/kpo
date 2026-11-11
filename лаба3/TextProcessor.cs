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

        //Заменить слова заданной длины на подстроку в конкретном предложении
        public static Text ReplaceWordsInSentence(Text text, int sentenceIndex, int wordLength, string replacement)
        {
            // Создаем копию текста, чтобы не ломать оригинал
            Text newText = new Text();

            for (int i = 0; i < text.Sentences.Count; i++)
            {
                Sentence oldSentence = text.Sentences[i];
                Sentence newSentence = new Sentence();

                foreach (var token in oldSentence.Tokens)
                {
                    // Если это нужное предложение, и токен — слово нужной длины
                    if (i == sentenceIndex && token is Word word && word.Value.Length == wordLength)
                    {
                        // Заменяем слово на указанную подстроку
                        newSentence.AddToken(new Word(replacement));
                    }
                    else
                    {
                        // Оставляем как было
                        newSentence.AddToken(token);
                    }
                }
                newText.AddSentence(newSentence);
            }

            return newText;
        }
        // Задание 6. Удалить стоп-слова из текста
        public static Text RemoveStopWords(Text text, string stopWordsFilePath)
        {
            // 1. Загружаем стоп-слова в HashSet для быстрого поиска (O(1))
            HashSet<string> stopWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (System.IO.File.Exists(stopWordsFilePath))
            {
                string[] lines = System.IO.File.ReadAllLines(stopWordsFilePath);
                foreach (var line in lines)
                {
                    string word = line.Trim();
                    if (!string.IsNullOrEmpty(word))
                    {
                        stopWords.Add(word);
                    }
                }
            }
            else
            {
                Console.WriteLine($"Внимание: файл {stopWordsFilePath} не найден!");
            }

            //Формируем новый текст без стоп-слов
            Text newText = new Text();

            foreach (var sentence in text.Sentences)
            {
                Sentence newSentence = new Sentence();

                foreach (var token in sentence.Tokens)
                {
                    bool isStopWord = false;

                    if (token is Word word)
                    {
                        // Приводим к нижнему регистру для сравнения
                        if (stopWords.Contains(word.Value.ToLower()))
                        {
                            isStopWord = true;
                        }
                    }

                    // Если это не стоп-слово, добавляем
                    if (!isStopWord)
                    {
                        newSentence.AddToken(token);
                    }
                }

                // Если в предложении остались слова, добавляем его
                if (newSentence.Tokens.Count > 0)
                {
                    newText.AddSentence(newSentence);
                }
            }

            return newText;
        }
    }


}