using FluentAssertions;

namespace Day9.Dirty.Tests;

public class Tests
{
    [Test]
    public void Test1()
    {
        var text = File.ReadAllText("TestData");
        text.Should().NotBeEmpty();
        new First().Solve(text).Should().Be(3);
    }
    
    [Test]
    public void ActualPart1()
    {
        var text = File.ReadAllText("Data");
        text.Should().NotBeEmpty();
        new First().Solve(text).Should().Be(2008960228L);
    }
    
    [Test]
    public void Test3()
    {
        var text = File.ReadAllText("TestData2");
        text.Should().NotBeEmpty();
        new Second().Solve(text).Should().Be(2);
    }
    
    [Test]
    public void ActualPart2()
    {
        var text = File.ReadAllText("Data");
        text.Should().NotBeEmpty();
        new Second().Solve(text).Should().Be(1097);
    }
}