using FluentAssertions;

namespace Day8.Dirty.Tests;

public class Tests
{
    [Test]
    public void Test1()
    {
        var text = File.ReadAllText("TestData");
        text.Should().NotBeEmpty();
        new First().Solve(text).Should().Be(2);
    }
    
    [Test]
    public void Test2()
    {
        var text = File.ReadAllText("TestData2");
        text.Should().NotBeEmpty();
        new First().Solve(text).Should().Be(6);
    }

    
    [Test]
    public void ActualPart1()
    {
        var text = File.ReadAllText("Data");
        text.Should().NotBeEmpty();
        new First().Solve(text).Should().Be(17621);
    }
    
    [Test]
    public void Test3()
    {
        var text = File.ReadAllText("TestData3");
        text.Should().NotBeEmpty();
        new Third().Solve(text).Should().Be(6);
    }
    
    [Test]
    public void ActualPart2()
    {
        var text = File.ReadAllText("Data");
        text.Should().NotBeEmpty();
        new Third().Solve(text).Should().Be(20685524831999UL);
    }
}