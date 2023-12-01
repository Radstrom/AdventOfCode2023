using FluentAssertions;

namespace Day1.Dirty.Tests;

public class Tests
{
    [Test]
    public void Test1()
    {
        var text = File.ReadAllText("TestData");
        text.Should().NotBeEmpty();
        First.Solve(text).Should().Be(142);
    }
    
    [Test]
    public void Test2()
    {
        var text = File.ReadAllText("Data");
        text.Should().NotBeEmpty();
        First.Solve(text).Should().Be(56042);
    }
    
    [Test]
    public void Test3()
    {
        var text = File.ReadAllText("TestData2");
        text.Should().NotBeEmpty();
        Second.Solve(text).Should().Be(281);
    }
    
    [Test]
    public void Test4()
    {
        var text = File.ReadAllText("Data");
        text.Should().NotBeEmpty();
        Second.Solve(text).Should().Be(55358);
    }
}