﻿using System.Text.RegularExpressions;

namespace ViaCepLibrary.Dtos
{
    public class ZipCodeRequest
    {
        public ZipCodeRequest(string zipCodeNumber)
        {
            zipCodeNumber = ClearZipCodeNumber(zipCodeNumber);
            Validate(zipCodeNumber);

            ZipCodeNumber = zipCodeNumber;
        }

        public string ZipCodeNumber { get; private set; }

        private string ClearZipCodeNumber(string? zipCodeNumber)
        {
            if (string.IsNullOrWhiteSpace(zipCodeNumber))
            {
                return string.Empty;
            }

            return Regex.Replace(zipCodeNumber, @"[^\d]", "");
        }

        private void Validate(string? zipCodeNumber)
        {
            if (string.IsNullOrWhiteSpace(zipCodeNumber))
                throw new ArgumentNullException("ZipCodeNumber");

            Regex regex = new(@"^\d{8}$");
            if (!regex.IsMatch(zipCodeNumber))
                throw new ArgumentException("The zip code number is invalid. Only eight numbers are allowed.");
        }
    }
}