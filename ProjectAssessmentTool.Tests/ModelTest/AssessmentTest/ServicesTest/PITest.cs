using Microsoft.VisualStudio.TestPlatform.Utilities;
using ProjectAssessmentTool.Model.Assessment.Services;
using Xunit.Abstractions;

namespace ProjectAssessmentTool.Tests
{
    public class PITest
    {
        private readonly ITestOutputHelper _output;

        public PITest(ITestOutputHelper output)
        {
            _output = output;
        }
        [Fact]
        public void PITest_ShouldBeGreaterThanOne_WhenProjectProfitable()
        {
            List<decimal> cashFlow = new List<decimal> { 200, 300, 400 };
            decimal investment = 700m;
            decimal expected = 1.04m;

            var pi = new PI();
            var actual = pi.calculation(cashFlow, investment);

            Assert.Equal(expected, actual);

            _output.WriteLine($"CashFlow = {string.Join(", ", cashFlow)}, investment = {investment}");
            _output.WriteLine($"Expected: {expected}, Actual: {actual}");
        }

        [Fact]
        public void PITest_ShouldBeZero_WhenInvestmentIsZero()
        {
            List<decimal> cashFlow = new List<decimal> { 200, 300, 400 };
            decimal investment = 0m;
            decimal expected = 0m;

            var pi = new PI();
            var actual = pi.calculation(cashFlow, investment);

            Assert.Equal(expected, actual);

            _output.WriteLine($"CashFlow = {string.Join(", ", cashFlow)}, investment = {investment}");
            _output.WriteLine($"Expected: {expected}, Actual: {actual}");
        }

        [Fact]
        public void PITest_ShouldBeZero_WhenCashFlowsAreEmpty()
        {
            List<decimal> cashFlow = new List<decimal>();
            decimal investment = 0m;
            decimal expected = 0m;

            var pi = new PI();
            var actual = pi.calculation(cashFlow, investment);

            Assert.Equal(expected, actual);

            _output.WriteLine($"CashFlow = {string.Join(", ", cashFlow)}, investment = {investment}");
            _output.WriteLine($"Expected: {expected}, Actual: {actual}");
        }

        [Fact]
        public void PITest_ShouldBeZero_WhenCashFlowsAreNull()
        {
            List<decimal> cashFlow = new List<decimal>();
            decimal investment = 0m;
            decimal expected = 0m;

            var pi = new PI();
            var actual = pi.calculation(null, investment);

            Assert.Equal(expected, actual);

            _output.WriteLine($"CashFlow = {string.Join(", ", cashFlow)}, investment = {investment}");
            _output.WriteLine($"Expected: {expected}, Actual: {actual}");
        }
    }

}
