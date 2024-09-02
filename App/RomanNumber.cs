namespace App;

public record RomanNumber(int Value)
{
    private static readonly Dictionary<char, int> RomanDigitValues = new()
    {
        { 'I', 1 },
        { 'V', 5 },
        { 'X', 10 },
        { 'L', 50 },
        { 'C', 100 },
        { 'D', 500 },
        { 'M', 1000 }
    };

    public int Value { get; } = Value;

    public static RomanNumber Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Input is empty");
        }

        input = input.Trim().ToUpper();

        if (input.Length != 1 || !RomanDigitValues.ContainsKey(input[0]))
        {
            throw new ArgumentException("Invalid Roman num.");
        }

        return new RomanNumber(RomanDigitValues[input[0]]);
    }
}