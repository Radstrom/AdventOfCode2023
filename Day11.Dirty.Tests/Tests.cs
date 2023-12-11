using FluentAssertions;

namespace Day11.Dirty.Tests;

public class Tests
{
    // [Test]
    // public void Easy()
    // {
    //     var text = File.ReadAllText("TestDataEasy");
    //     text.Should().NotBeEmpty();
    //     new First().Solve(text).Should().Be(2);
    // }
    
    [Test]
    public void Test1()
    {
        var text = File.ReadAllText("TestData");
        text.Should().NotBeEmpty();
        new First().Solve(text).Should().Be(374);
    }
    
    [Test]
    public void ActualPart1()
    {
        var text = File.ReadAllText("Data");
        text.Should().NotBeEmpty();
        new First().Solve(text).Should().Be(9723824L);
    }
    
    [Test]
    public void Test3()
    {
        var text = File.ReadAllText("TestData");
        text.Should().NotBeEmpty();
        new Second().Solve(text, 10).Should().Be(1030);
        new Second().Solve(text, 100).Should().Be(8410);
    }
    
    [Test]
    public void ActualPart2()
    {
        var text = File.ReadAllText("Data");
        text.Should().NotBeEmpty();
        new Second().Solve(text, 1000000).Should().Be(731244261352L);
    }
}