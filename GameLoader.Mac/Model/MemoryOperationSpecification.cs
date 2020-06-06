using System;
namespace GameLoader.Mac.Model
{
    public class MemoryOperationSpecification
    {
		public string Filename { get; set; }
        public string MemoryType { get; set; }
        public string Operation { get; set; }
        public string Format { get; set; }

        public MemoryOperationSpecification(string filename, string memoryType = "flash", string operation = "w", string format = "i")
        {
			Filename = filename;
            MemoryType = memoryType;
            Operation = operation;
            Format = format;
        }

        public override string ToString() => $"{MemoryType}:{Operation}:{Filename}:{Format}";
    }
}
