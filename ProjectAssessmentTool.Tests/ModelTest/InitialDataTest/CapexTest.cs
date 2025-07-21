namespace ProjectAssessmentTool.Tests;
using ProjectAssessmentTool.Model;
using Xunit.Abstractions;

public class CapexTest
{
    public class CapexTests
    {
        [Fact]
        public void ValueA_SetAndGet_ShouldWorkCorrectly()
        {
            var capex = new Capex();
            capex.ValueA = 123.45m;

            Assert.Equal(123.45m, capex.ValueA);
        }

        [Fact]
        public void CapexSum_ShouldReturnSameAsValueA()
        {
            var capex = new Capex();
            capex.ValueA = 88.8m;

            Assert.Equal(88.8m, capex.CapexSum);
        }

        [Fact]
        public void SettingValueA_ShouldRaisePropertyChanged_ForCapexSum()
        {
            var capex = new Capex();
            bool capexSumChanged = false;

            capex.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Capex.CapexSum))
                    capexSumChanged = true;
            };

            capex.ValueA = 10;

            Assert.True(capexSumChanged);
        }

        [Fact]
        public void UnitName_ShouldDefaultToNull_AndBeSettable()
        {
            var capex = new Capex();

            Assert.Null(capex.UnitName);

            capex.UnitName = "Unit 1";

            Assert.Equal("Unit 1", capex.UnitName);
        }

        [Fact]
        public void SettingUnitName_ShouldRaisePropertyChanged()
        {
            var capex = new Capex();
            bool unitNameChanged = false;

            capex.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Capex.UnitName))
                    unitNameChanged = true;
            };

            capex.UnitName = "Unit A";

            Assert.True(unitNameChanged);
        }
    }
}
