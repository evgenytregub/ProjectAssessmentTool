using ProjectAssessmentTool.Model;
using System.ComponentModel;
using Xunit;

namespace ProjectAssessmentTool.Tests;


public class OpexTests
{
    [Fact]
    public void ValueA_And_ValueB_ShouldAffect_OpexSum()
    {
        var opex = new Opex();

        opex.ValueA = 10;
        opex.ValueB = 5;

        Assert.Equal(50, opex.OpexSum);
    }

    [Fact]
    public void Changing_ValueA_ShouldRaise_PropertyChanged_For_OpexSum()
    {
        var opex = new Opex();
        bool opexSumChanged = false;

        opex.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(Opex.OpexSum))
                opexSumChanged = true;
        };

        opex.ValueA = 20;

        Assert.True(opexSumChanged);
    }

    [Fact]
    public void Changing_ValueB_ShouldRaise_PropertyChanged_For_OpexSum()
    {
        var opex = new Opex();
        bool opexSumChanged = false;

        opex.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(Opex.OpexSum))
                opexSumChanged = true;
        };

        opex.ValueB = 3;

        Assert.True(opexSumChanged);
    }

    [Fact]
    public void MaterialName_ShouldBe_NullByDefault_AndSettable()
    {
        var opex = new Opex();

        Assert.Null(opex.MaterialName);

        opex.MaterialName = "Steel";

        Assert.Equal("Steel", opex.MaterialName);
    }

    [Fact]
    public void Changing_MaterialName_ShouldRaise_PropertyChanged()
    {
        var opex = new Opex();
        bool materialNameChanged = false;

        opex.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(Opex.MaterialName))
                materialNameChanged = true;
        };

        opex.MaterialName = "Plastic";

        Assert.True(materialNameChanged);
    }
}
