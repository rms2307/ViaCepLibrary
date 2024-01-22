﻿namespace ViaCepLibrary.Exceptions
{
    public class ZipCodeNotFoundException : Exception
    {
        public ZipCodeNotFoundException() : base()
        {
        }

        public ZipCodeNotFoundException(string message) : base(message)
        {
        }
    }
}
