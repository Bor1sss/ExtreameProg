namespace App
{
    public record RomanNumber(int Value)
    {
        private readonly int _value = Value;
        public int Value => _value;

        public static RomanNumber Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input cannot be null or empty.");
            int value = 0;
            int prevDigit = 0;
            foreach (char c in input.Reverse())
            {
                int digit = DigitValue(c.ToString());
                value += digit >= prevDigit ? digit : -digit;
                prevDigit = digit;
            }
            if (!IsValidRomanNumber(input))
                throw new ArgumentException($"Invalid Roman number format: {input}");

            return new(value);
        }

        public static int DigitValue(String digit) => digit switch
        {
            "N" => 0,
            "I" => 1,
            "V" => 5,
            "X" => 10,
            "L" => 50,
            "C" => 100,
            "D" => 500,
            _ => 1000
            // "M" => 1000,
            // _ => throw new ArgumentException("Invalid Roman digit.")
        };
        private static bool IsValidRomanNumber(string input)
        {
            return !System.Text.RegularExpressions.Regex.IsMatch(input, "IIII|VV|XXXX|LL|CCCC|DD|MMMM");
        }
    }
}