using System;
using System.Xml.Serialization;

namespace TextTokenizer
{
    // Говорим сериализатору, какие классы могут быть внутри Token
    [XmlInclude(typeof(Word))]
    [XmlInclude(typeof(Punctuation))]
    public abstract class Token
    {
        [XmlText] // Значение выводится прямо внутрь тега (например, <word>Привет</word>)
        public string Value { get; set; }

        public Token() { }

        public Token(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }

    public class Word : Token
    {
        public Word() : base() { }
        public Word(string value) : base(value) { }
    }

    public class Punctuation : Token
    {
        public Punctuation() : base() { }
        public Punctuation(string value) : base(value) { }
    }
}