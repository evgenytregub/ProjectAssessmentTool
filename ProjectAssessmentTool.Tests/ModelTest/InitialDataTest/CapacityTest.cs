namespace ProjectAssessmentTool.Tests;

using ProjectAssessmentTool.Model;
using Xunit.Abstractions;

public class CapacityTests
{
    private readonly ITestOutputHelper _output;
    public CapacityTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void SumTest_ShouldBeProductOfValueAAndValueB()
    {
        var capacity = new Capacity();
        capacity.ValueA = 10;
        capacity.ValueB = 2.5m;

        decimal actual = capacity.Sum;
        decimal expected = 25.0m;

        Assert.Equal(expected, capacity.Sum);

        _output.WriteLine($"Expected: {expected}, Actual: {actual}");
    }

    [Fact]
    public void SettingValueA_ShouldRaisePropertyChanged_ForSum()
    {
        var capacity = new Capacity();
        bool sumChanged = false;

        capacity.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(Capacity.Sum))
                sumChanged = true;
        };

        capacity.ValueA = 5;

        Assert.True(sumChanged);

        _output.WriteLine($"Actual: {sumChanged}");
    }

    [Fact]
    public void SettingValueB_ShouldRaisePropertyChanged_ForSum()
    {
        var capacity = new Capacity();
        bool sumChanged = false;

        capacity.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(Capacity.Sum))
                sumChanged = true;
        };

        capacity.ValueB = 3;

        Assert.True(sumChanged);

        _output.WriteLine($"Actual: {sumChanged}");
    }
}
