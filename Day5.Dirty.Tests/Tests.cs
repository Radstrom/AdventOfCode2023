using FluentAssertions;

namespace Day5.Dirty.Tests;

public class Tests
{
    [Test]
    public void Test1()
    {
        var text = File.ReadAllText("TestData");
        text.Should().NotBeEmpty();
        new First().Solve(text).Should().Be(35);
    }
    
    [Test]
    public void Test2()
    {
        var text = File.ReadAllText("Data");
        text.Should().NotBeEmpty();
        new First().Solve(text).Should().Be(51580674UL);
    }
    
    [Test]
    public void Test3()
    {
        var text = File.ReadAllText("TestData");
        text.Should().NotBeEmpty();
        new Second().Solve(text).Should().Be(46);
    }
    
    [Test]
    public void Test4()
    {
        var text = File.ReadAllText("Data");
        text.Should().NotBeEmpty();
        new Second().Solve(text).Should().Be(99751240UL);
    }
}