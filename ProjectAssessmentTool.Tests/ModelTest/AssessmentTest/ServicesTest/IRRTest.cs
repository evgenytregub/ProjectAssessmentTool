using ProjectAssessmentTool.Model.Assessment.Interfaces;
using ProjectAssessmentTool.Model.Assessment.Services;
using Xunit.Abstractions;

namespace ProjectAssessmentTool.Tests
{
    public class IRRTest
    {
        private readonly ITestOutputHelper _output;

        public IRRTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void IRRTest_Expected_18()
        {
            List<decimal> data = new List<decimal> { -1000m, 200m, 300m, 500m, 600m };
            decimal expected = 18m;

            IIRR total = new IRR();
            decimal actual = total.calculation(data);

            double expectedD = (double)expected;
            double actualD = (double)actual;
            double delta = 0.5;

            _output.WriteLine($"Expected: {expected}, Actual: {actual}, Delta: {delta}");

            Assert.InRange(actualD, expectedD - delta, expectedD + delta);
        }

        [Fact]
        public void IRRTest_Expected_0()
        {
            List<decimal> data = new List<decimal> { -1000, 100, 100, 100 };
            decimal expected = 0m;

            IIRR total = new IRR();
            decimal actual = total.calculation(data);

            double expectedD = (double)expected;
            double actualD = (double)actual;
            double delta = 0.5;

            _output.WriteLine($"Expected: {expected}, Actual: {actual}, Delta: {delta}");

            Assert.InRange(actualD, expectedD - delta, expectedD + delta);
        }

        [Fact]
        public void IRRTest_Expected_EmptyList()
        {
            List<decimal> data = new List<decimal>();
            decimal expected = 0m;

            IIRR total = new IRR();
            decimal actual = total.calculation(data);

            double expectedD = (double)expected;
            double actualD = (double)actual;
            double delta = 0.5;

            _output.WriteLine($"Expected: {expected}, Actual: {actual}, Delta: {delta}");

            Assert.InRange(actualD, expectedD - delta, expectedD + delta);
        }
    }
}


