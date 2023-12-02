using FluentAssertions;

namespace Day2.Dirty.Tests;

public class Tests
{
    [Test]
    public void Test1()
    {
        var text = File.ReadAllText("TestData");
        text.Should().NotBeEmpty();
        First.Solve(text).Should().Be(8);
    }
    
    [Test]
    public void Test2()
    {
        var text = File.ReadAllText("Data");
        text.Should().NotBeEmpty();
        First.Solve(text).Should().Be(2795);
    }
    
    [Test]
    public void Test3()
    {
        var text = File.ReadAllText("TestData");
        text.Should().NotBeEmpty();
        Second.Solve(text).Should().Be(2286);
    }
    
    [Test]
    public void Test4()
    {
        var text = File.ReadAllText("Data");
        text.Should().NotBeEmpty();
        Second.Solve(text).Should().Be(75561);
    }
}