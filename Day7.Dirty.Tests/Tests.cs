using FluentAssertions;

namespace Day7.Dirty.Tests;

public class Tests
{
    [Test]
    public void Test1()
    {
        var text = File.ReadAllText("TestData");
        text.Should().NotBeEmpty();
        new First().Solve(text).Should().Be(6440);
    }
    
    [Test]
    [TestCase("11111", First.HandType.FiveOfAKind)]
    [TestCase("11112", First.HandType.FourOfAKind)]
    [TestCase("11122", First.HandType.FullHouse)]
    [TestCase("11123", First.HandType.ThreeOfAKind)]
    [TestCase("11224", First.HandType.TwoPair)]
    [TestCase("11234", First.HandType.OnePair)]
    [TestCase("12345", First.HandType.HighCard)]
    public void GetHandType(string str, First.HandType expected)
    {
        new First().GetHandType(str).Should().Be(expected);
    }
    
    [Test]
    public void Test2()
    {
        var text = File.ReadAllText("Data");
        text.Should().NotBeEmpty();
        new First().Solve(text).Should().Be(250946742);
    }
    
    [Test]
    public void Test3()
    {
        var text = File.ReadAllText("TestData");
        text.Should().NotBeEmpty();
        new Second().Solve(text).Should().Be(5905);
    }
    
    [Test]
    [TestCase("11J11", Second.HandType.FiveOfAKind)]
    [TestCase("JJJJJ", Second.HandType.FiveOfAKind)]
    [TestCase("J4444", Second.HandType.FiveOfAKind)]
    [TestCase("1J112", Second.HandType.FourOfAKind)]
    [TestCase("JJJ21", Second.HandType.FourOfAKind)]
    [TestCase("1J122", Second.HandType.FullHouse)]
    [TestCase("11123", Second.HandType.ThreeOfAKind)]
    [TestCase("11224", Second.HandType.TwoPair)]
    [TestCase("1J234", Second.HandType.OnePair)]
    [TestCase("12345", Second.HandType.HighCard)]
    public void GetHandTypeWithJokers(string str, Second.HandType expected)
    {
        new Second().GetHandType(str).Should().Be(expected);
    }
    
    [Test]
    public void Test4()
    {
        var text = File.ReadAllText("Data");
        text.Should().NotBeEmpty();
        new Second().Solve(text).Should().Be(251824095);
    }
}