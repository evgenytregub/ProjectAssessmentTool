using Newtonsoft.Json.Linq;
using ProjectAssessmentTool.Model.Assessment.Interfaces;
using ProjectAssessmentTool.Model.Assessment.Services;
using Xunit.Abstractions;

namespace ProjectAssessmentTool.Tests
{
    public class NPVTest
    {
        private readonly ITestOutputHelper _output;

        public NPVTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void NPVTest_ShouldReturnExpectedValue()
        {
            List<decimal> cashFlow = new List<decimal> { 500, 600, 700 };
            decimal discountRate = 0.1m;
            decimal investment = 1000m;
            decimal expected = 476.33m;

            var npv = new NPV();
            var actual = npv.calculation(cashFlow, discountRate, investment);

            Assert.Equal(expected, actual);

            _output.WriteLine($"CashFlow = {string.Join(", ", cashFlow)}, discountRate = {discountRate}, investment = {investment}");
            _output.WriteLine($"Expected: {expected}, Actual: {actual}");
        }

        [Fact]
        public void NPVTest_WithEmptyCashFlows_ShouldReturnNegativeInvestment()
        {
            List<decimal> cashFlow = new List<decimal>();
            decimal discountRate = 0.1m;
            decimal investment = 1000m;
            decimal expected = -1000m;

            var npv = new NPV();
            var actual = npv.calculation(cashFlow, discountRate, investment);

            Assert.Equal(expected, actual);

            _output.WriteLine($"CashFlow = {string.Join(", ", cashFlow)}, discountRate = {discountRate}, investment = {investment}");
            _output.WriteLine($"Expected: {expected}, Actual: {actual}");
        }

        [Fact]
        public void NPV_WithZeroDiscountRate_ShouldBeSimpleSumMinusInvestment()
        {
            List<decimal> cashFlow = new List<decimal> { 100, 200 };
            decimal discountRate = 0m;
            decimal investment = 250m;
            decimal expected = 50m;

            var npv = new NPV();
            var actual = npv.calculation(cashFlow, discountRate, investment);

            Assert.Equal(expected, actual);

            _output.WriteLine($"CashFlow = {string.Join(", ", cashFlow)}, discountRate = {discountRate}, investment = {investment}");
            _output.WriteLine($"Expected: {expected}, Actual: {actual}");
        }

        [Fact]
        public void NPV_WithHighDiscountRate_ShouldBeLower()
        {
            List<decimal> cashFlow = new List<decimal> { 100, 200 };
            decimal discountRate = 1m;
            decimal investment = 150m;
            decimal expected = -50m;

            var npv = new NPV();
            var actual = npv.calculation(cashFlow, discountRate, investment);

            Assert.Equal(expected, actual);

            _output.WriteLine($"CashFlow = {string.Join(", ", cashFlow)}, discountRate = {discountRate}, investment = {investment}");
            _output.WriteLine($"Expected: {expected}, Actual: {actual}");
        }
    }
}

