using System;

namespace HLTools
{

    public class TextureDimensionException : Exception
    {
        public TextureDimensionException() : base() { }
        public TextureDimensionException(string message) : base(message) { }
        public TextureDimensionException(string message, Exception inner) : base(message, inner) { }

    }

    public class HLToolsUnsupportedFile : Exception
    {
        public HLToolsUnsupportedFile() : base() { }
        public HLToolsUnsupportedFile(string message) : base(message) { }
        public HLToolsUnsupportedFile(string message, Exception inner) : base(message, inner) { }

    }
}
