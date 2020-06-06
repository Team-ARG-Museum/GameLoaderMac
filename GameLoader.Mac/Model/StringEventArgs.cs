using System;
namespace GameLoader.Mac.Model
{
    public class StringEventArgs : EventArgs
    {
        public string Value { get; set; }
        public StringEventArgs(string value) => Value = value;
    }
}
