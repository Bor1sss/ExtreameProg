using App;
using System.Linq;
using System.Reflection;

namespace Test
{
    [TestClass]
    public class RomanNumberTest
    {
        [TestMethod]
        public void ParseTest()
        {
            var testCases = new Dictionary<string, int>()
        {
            { "N",      0 },
            { "I",      1 },
            { "II",     2 },
            { "III",    3 },
            //{ "IIII",   4 },
            //{ "IV",     4 },
            { "V",      5 },
            { "VI",     6 },
            { "VII",    7 },
            { "VIII",   8 },
            { "D",      500 },
            //{ "CM",     900 },
            { "M",      1000 },
            { "MC",     1100 },
            //{ "MCM",    1900 },
            { "MM",     2000 },

            //Not optimal
            //{ "XXXX",   40 },
            //{ "LL",     100 },
            //{ "CCCC",   400 },
            //{ "DDD",    1500 },
            
            // Incorrect
            //{ "IC",     -1 },
            //{ "IL",     -1 },
            //{ "XD",     -1 },
            //{ "VVVV",   -1 },
            //{ "MMMM",   -1 }
        };

            foreach (var testCase in testCases)
            {
                RomanNumber rn = RomanNumber.Parse(testCase.Key);
                Assert.IsNotNull(rn, $"Parsing failed for '{testCase.Key}'");

                Assert.AreEqual(testCase.Value, rn.Value, 
                    $"Expected: {testCase.Value}, Actual: {rn.Value}. Test case: '{testCase.Key}'");
            }


            Dictionary<string, (char, int)[]> exTestCases = new()
            {
                {"W", new[] {('W', 0)}},
                {"Q", new[] {('Q', 0)}},
                {"s", new[] {('s', 0)}},
                {"Xd", new[] {('d', 1)}},
                //{"SWXF", new[] {('S', 0), ('W', 1), ('F', 3)}},
                {"XXFX", new[] {('F', 2)}},
                //{"VVVFX", new[] {('F', 3)}},
                {"IVF", new[] {('F', 2)}},
            };

            foreach (var testCase in exTestCases)
            {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumber.Parse(testCase.Key),
                    $"{nameof(FormatException)} Parse '{testCase.Key}' must throw");

                foreach (var (symbol, position) in testCase.Value)
                {
                    Assert.IsTrue(ex.Message.Contains($"Invalid symbol '{symbol}' in position {position}"),
                        $"{nameof(FormatException)} must contain data about symbol '{symbol}' at position {position}. " +
                        $"TestCase: '{testCase.Key}', ex.Message: '{ex.Message}'");
                }
            }




            Dictionary<String, Object[]> exTestCases2 = new()
            {
                { "IM",  ['I', 'M', 0] },
                //{ "XIM", ['I', 'M', 1] },
                { "IMX", ['I', 'M', 0] },
                { "XMD", ['X', 'M', 0] },
                //{ "XID", ['I', 'D', 1] },
                { "ID", ['I', 'D', 0] },
                { "XM", ['X', 'M', 0] },


            };
            foreach (var testCase in exTestCases2)
            {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumber.Parse(testCase.Key),
                    $"Expected FormatException for '{testCase.Key}'");

                Assert.IsTrue(ex.Message.Contains($"Invalid order '{testCase.Value[0]}' before '{testCase.Value[1]}' in position {testCase.Value[2]}"),
                    $"Message does not match expected order for symbols '{testCase.Value[0]}' and '{testCase.Value[1]}'. Test case: '{testCase.Key}'");
            }

            

            string[] exTestCases4 =
            {
                "IXC", "IIX", "VIX",
                "IIXC", "IIIX", "VIIX",
                "VIXC", "IVIX", "CVIIX",
                "IXCC", "IXCM", "IXXC",
            };

            foreach (var testCase in exTestCases4)
            {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumber.Parse(testCase),
                    $"FormatException expected for '{testCase}'");

                Assert.IsTrue(
                    ex.Message.Contains("Invalid sequence: more than one smaller digit before") &&
                    ex.Message.Contains(testCase),
                    $"Expected message about invalid sequence in '{testCase}'");
            }

    }

 


        Dictionary<int, String> _digitValues = new Dictionary<int, String>();



        [TestMethod]
        public void DigitalValueTest()
        {
            var romanToInt = new Dictionary<string, int>()
        {
            { "N", 0 },
            { "I", 1 },
            { "V", 5 },
            { "X", 10 },
            { "L", 50 },
            { "C", 100 },
            { "D", 500 },
            { "M", 1000 },
        };

            foreach (var kvp in romanToInt)
            {

                Assert.AreEqual(
                    kvp.Value,
                    RomanNumber.DigitalValue(kvp.Key),
                    $"{kvp.Value} parsing failed. Expected {kvp}, got {kvp.Value}."
                );
            }


            Random random = new();
            for (int i = 0; i < 100; i++)
            {
                string invalidDigit = ((char)random.Next(256)).ToString();

                if (romanToInt.ContainsKey(invalidDigit))
                {
                    i--;
                    continue;
                }

                ArgumentException ex = Assert.ThrowsException<ArgumentException>(
                    () => RomanNumber.DigitalValue(invalidDigit),
                    $"Expected ArgumentException for invalid digit '{invalidDigit}'");

                Assert.IsTrue(
                    ex.Message.Contains($"'digit' has invalid value '{invalidDigit}'"),
                    $"Message should contain 'digit has invalid value' for '{invalidDigit}'");
            }

        }

        [TestMethod]
        public void ToStringTest()
        {
            Dictionary<int, string> testCases = new Dictionary<int, string>()
    {
        { 1, "I" },
        { 2, "II"},
        { 3343, "MMMCCCXLIII" },
        { 4, "IV" },
        { 44, "XLIV" },
        { 9, "IX" },
        { 90, "XC" },
        { 1400, "MCD" },
        { 900, "CM" },
        { 990, "CMXC" },


    };
            _digitValues.Keys.ToList().ForEach(k => testCases.Add(k, _digitValues[k]));
            foreach (var testCase in testCases)
            {
                Assert.AreEqual(
                    new RomanNumber(testCase.Key).ToString(),
                    testCase.Value,
                    $"ToString({testCase.Key})--> {testCase.Value}");
            }
        }


        [TestMethod]
        public void PlusTest()
        {

            RomanNumber rn1 = new(1);
            RomanNumber rn2 = new(2);
            RomanNumber rn3 = rn1.Plus(rn2);
            Assert.IsNotNull(rn3, "Result of Plus should not be null");
            Assert.AreEqual(rn1.Value + rn2.Value, rn3.Value, "Incorrect addition result");

            Assert.IsInstanceOfType(rn3, typeof(RomanNumber),
                "Plus result must have RomanNumber type");
            Assert.AreNotSame(rn3, rn1, "Plus result is new instance, neither (v)first, nor second arg");
            Assert.AreNotSame(rn3, rn2, "Plus result is new instance, neither first, nor (v)second arg");
            Assert.AreEqual(rn1.Value + rn2.Value, rn3.Value, "Plus arithmetic");
       

            RomanNumber rn1_2 = RomanNumber.Parse("IV");
            String rn2_2 = "VI";
            RomanNumber rn3_2 = rn1_2.Plus(rn2_2);

            Assert.IsNotNull(rn3_2);
            Assert.AreNotSame(rn3_2, rn1_2, "Plus str result is new instance, neither (v)first, nor second arg");
            Assert.AreEqual("X", rn3_2.ToString(), "Plus str arithmetic");
           
        }
    }
}


