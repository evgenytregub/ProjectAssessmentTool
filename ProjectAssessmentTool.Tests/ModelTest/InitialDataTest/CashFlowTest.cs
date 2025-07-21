using ProjectAssessmentTool.Model;
using Xunit;
using System.ComponentModel;
namespace ProjectAssessmentTool.Tests;


public class CashFlowTests
{
    [Fact]
    public void ValueA_And_ValueB_ShouldBeSettable_AndAffectSums()
    {
        var cashFlow = new CashFlow();

        cashFlow.ValueA = 40;
        cashFlow.ValueB = 100;

        Assert.Equal(60, cashFlow.CashFlowSum);    // Income - Expenses
        Assert.Equal(40, cashFlow.ExpensesSum);    // = ValueA
        Assert.Equal(100, cashFlow.IncomeSum);     // = ValueB
    }

    [Fact]
    public void Changing_ValueA_ShouldRaise_CashFlowSum_PropertyChanged()
    {
        var cashFlow = new CashFlow();
        bool cashFlowSumChanged = false;

        cashFlow.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(CashFlow.CashFlowSum))
                cashFlowSumChanged = true;
        };

        cashFlow.ValueA = 123;

        Assert.True(cashFlowSumChanged);
    }

    [Fact]
    public void Changing_ValueB_ShouldRaise_CashFlowSum_PropertyChanged()
    {
        var cashFlow = new CashFlow();
        bool cashFlowSumChanged = false;

        cashFlow.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(CashFlow.CashFlowSum))
                cashFlowSumChanged = true;
        };

        cashFlow.ValueB = 456;

        Assert.True(cashFlowSumChanged);
    }

    [Fact]
    public void CashFlowYear_ShouldBe_Null_ByDefault_AndSettable()
    {
        var cashFlow = new CashFlow();

        Assert.Null(cashFlow.CashFlowYear);

        cashFlow.CashFlowYear = "2025";

        Assert.Equal("2025", cashFlow.CashFlowYear);
    }

    [Fact]
    public void Setting_CashFlowYear_ShouldRaise_PropertyChanged()
    {
        var cashFlow = new CashFlow();
        bool yearChanged = false;

        cashFlow.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(CashFlow.CashFlowYear))
                yearChanged = true;
        };

        cashFlow.CashFlowYear = "2030";

        Assert.True(yearChanged);
    }
}
