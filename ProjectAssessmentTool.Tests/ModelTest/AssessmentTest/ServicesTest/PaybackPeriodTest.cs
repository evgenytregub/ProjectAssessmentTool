using Microsoft.VisualStudio.TestPlatform.Utilities;
using ProjectAssessmentTool.Model.Assessment.Services;
using Xunit.Abstractions;

namespace ProjectAssessmentTool.Tests
{
    public class PaybackPeriodTest
    {
        private readonly ITestOutputHelper _output;

        public PaybackPeriodTest(ITestOutputHelper output)
        {
            _output = output;
        }
        [Fact]
        public void PaybackPeriodTest_ShouldBeCalculatedCorrectly()
        {
            decimal investments = 1000m;
            decimal annualInCame = 250m;
            decimal expected = 4m;

            var paybackPeriod = new PaybackPeriod();
            var actual = paybackPeriod.calculation(investments, annualInCame);

            Assert.Equal(expected, actual);

            _output.WriteLine($"Investments = {investments}, AnnualInCame = {annualInCame}");
            _output.WriteLine($"Expected: {expected}, Actual: {actual}");
        }

        [Fact]
        public void PaybackPeriodTest_ZeroIncome_ShouldUseOneAsDenominator()
        {
            decimal investments = 0m;
            decimal annualInCame = 250m;
            decimal expected = 0m;

            var paybackPeriod = new PaybackPeriod();
            var actual = paybackPeriod.calculation(investments, annualInCame);

            Assert.Equal(expected, actual);

            _output.WriteLine($"Investments = {investments}, AnnualInCame = {annualInCame}");
            _output.WriteLine($"Expected: {expected}, Actual: {actual}");
        }

        [Fact]
        public void PaybackPeriod_NegativeIncome_ShouldUseOneAsDenominator()
        {
            decimal investments = 1000m;
            decimal annualInCame = -100m;
            decimal expected = 1000m;

            var paybackPeriod = new PaybackPeriod();
            var actual = paybackPeriod.calculation(investments, annualInCame);

            Assert.Equal(expected, actual);

            _output.WriteLine($"Investments = {investments}, AnnualInCame = {annualInCame}");
            _output.WriteLine($"Expected: {expected}, Actual: {actual}");
        }
    }
}
