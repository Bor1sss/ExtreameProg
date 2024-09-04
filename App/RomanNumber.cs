namespace App
{
    public record RomanNumber(int Value)
    {
        public static RomanNumber Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input cannot be null or empty.");

            int totalValue = 0;
            int previousValue = 0;

            foreach (char currentChar in input.Reverse())
            {
                if (!RomanDigitValues.TryGetValue(currentChar, out int currentValue))
                    throw new ArgumentException($"Invalid Roman numeral character: {currentChar}");

                totalValue += currentValue < previousValue ? -currentValue : currentValue;
                previousValue = currentValue;
            }

            return new RomanNumber(totalValue);
        }
        
        private static readonly Dictionary<char, int> RomanDigitValues = new()
        {
            { 'I', 1 }, { 'V', 5 }, { 'X', 10 }, { 'L', 50 }, { 'C', 100 }, { 'D', 500 }, { 'M', 1000 }
        };
    }
}
