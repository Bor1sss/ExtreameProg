using App;

namespace Test;

[TestClass]
public class RomanNumberTest
{
    [TestMethod]
    public void ParseTest_Validation()
    {
        Assert.AreEqual(1, RomanNumber.Parse("I").Value);
        Assert.AreEqual(5, RomanNumber.Parse("V").Value);
        Assert.AreEqual(10, RomanNumber.Parse("X").Value);
        Assert.AreEqual(50, RomanNumber.Parse("L").Value);
        Assert.AreEqual(100, RomanNumber.Parse("C").Value);
        Assert.AreEqual(500, RomanNumber.Parse("D").Value);
        Assert.AreEqual(1000, RomanNumber.Parse("M").Value);
    }
    
}