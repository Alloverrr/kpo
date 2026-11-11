using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TextTokenizer
{
    public class TextParser
    {
        // Метод читает файл и возвращает готовый объект Text
        public static Text ParseFile(string filePath)
        {
            Text text = new Text();
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл {filePath} не найден!");
                return text;
            }

            string content = File.ReadAllText(filePath);
            return ParseText(content);
        }

        // Метод разбивает строку на предложения, а затем на токены
        public static Text ParseText(string content)
        {
            Text text = new Text();

            // Регулярное выражение для поиска предложений. 
            // Оно ищет всё до точки, восклицательного или вопросительного знака (включая кавычки после них).
            // (?<=[.!?]) - означает, что мы ищем разрыв ПОСЛЕ этих знаков.
            string[] rawSentences = Regex.Split(content, @"(?<=[.!?])\s+");

            foreach (string rawSentence in rawSentences)
            {
                if (string.IsNullOrWhiteSpace(rawSentence)) continue;

                Sentence sentence = new Sentence();

                // Регулярное выражение для поиска слов и знаков препинания.
                // [А-Яа-яA-Za-z0-9]+ — это слова (буквы и цифры)
                // [^\sА-Яа-яA-Za-z0-9] — это всё, что не буква, не цифра и не пробел (т.е. знаки препинания)
                string pattern = @"([А-Яа-яA-Za-z0-9]+)|([^\sА-Яа-яA-Za-z0-9])";
                MatchCollection matches = Regex.Matches(rawSentence, pattern);

                foreach (Match match in matches)
                {
                    if (match.Groups[1].Success) // Это слово
                    {
                        sentence.AddToken(new Word(match.Value));
                    }
                    else if (match.Groups[2].Success) // Это знак препинания
                    {
                        sentence.AddToken(new Punctuation(match.Value));
                    }
                }

                if (sentence.Tokens.Count > 0)
                {
                    text.AddSentence(sentence);
                }
            }

            return text;
        }
    }
}