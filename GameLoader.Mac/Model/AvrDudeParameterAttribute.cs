using System;
namespace GameLoader.Mac.Model
{
    public class AvrDudeParameterAttribute : Attribute
    {
        public char Option { get; }
        public string DefaultValue { get; }

        public AvrDudeParameterAttribute(char option, string defaultValue = "")
        {
            Option = option;
            DefaultValue = defaultValue;
        }
    }
}
