using FluentAssertions;

namespace Day6.Dirty.Tests;

public class Tests
{
    [Test]
    public void Test1()
    {
        var text = File.ReadAllText("TestData");
        text.Should().NotBeEmpty();
        new First().Solve(text).Should().Be(288);
    }
    
    [Test]
    public void Test2()
    {
        var text = File.ReadAllText("Data");
        text.Should().NotBeEmpty();
        new First().Solve(text).Should().Be(625968);
    }
    
    [Test]
    public void Test3()
    {
        var text = File.ReadAllText("TestData2");
        text.Should().NotBeEmpty();
        new Second().Solve(text).Should().Be(71503);
    }
    
    [Test]
    public void Test4()
    {
        var text = File.ReadAllText("Data2");
        text.Should().NotBeEmpty();
        new Second().Solve(text).Should().Be(43663323);
    }
}