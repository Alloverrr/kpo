using System;

namespace TextTokenizer
{
    // Базовый класс для всех токенов
    public abstract class Token
    {
        public string Value { get; set; }

        public Token(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }

    // Класс для слов
    public class Word : Token
    {
        public Word(string value) : base(value) { }

        // Сюда мы потом добавим логику проверки на согласные буквы
    }

    // Класс для знаков препинания
    public class Punctuation : Token
    {
        public Punctuation(string value) : base(value) { }
    }
}