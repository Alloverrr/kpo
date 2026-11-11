using System;
using System.Collections.Generic;
using System.Linq;

namespace TextTokenizer
{
    public class Sentence
    {
        // Коллекция токенов (слова и знаки препинания)
        public List<Token> Tokens { get; set; }

        public Sentence()
        {
            Tokens = new List<Token>();
        }

        // Добавить токен в предложение
        public void AddToken(Token token)
        {
            Tokens.Add(token);
        }

        // Получить только слова из предложения (без знаков препинания)
        public List<Word> GetWords()
        {
            return Tokens.OfType<Word>().ToList();
        }

        // Количество слов в предложении (для сортировки)
        public int GetWordCount()
        {
            return Tokens.Count(t => t is Word);
        }

        // Длина предложения (сумма длин всех токенов)
        public int GetLength()
        {
            return Tokens.Sum(t => t.Value.Length);
        }

        // Проверка, является ли предложение вопросительным (заканчивается на "?" или "?!" и т.д.)
        public bool IsQuestion()
        {
            var lastToken = Tokens.LastOrDefault();
            return lastToken is Punctuation && lastToken.Value.Contains("?");
        }

        public override string ToString()
        {
            // Склеиваем токены обратно в строку с пробелами
            return string.Join(" ", Tokens.Select(t => t.Value));
        }
    }
}