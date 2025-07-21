using Newtonsoft.Json.Linq;
using ProjectAssessmentTool.Model.Assessment.Interfaces;
using ProjectAssessmentTool.Model.Assessment.Services;
using Xunit.Abstractions;

namespace ProjectAssessmentTool.Tests
{
    public class ROITest
    {
        private readonly ITestOutputHelper _output;

        public ROITest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ROITest_ShouldBePositive()
        {
            decimal income = 1500m;
            decimal investment = 1000m;
            decimal expected = 50m;

            var roi = new ROI();
            var actual = roi.calculation(income, investment);

            Assert.Equal(expected, actual);

            _output.WriteLine($"Income = {income}, investment = {investment}");
            _output.WriteLine($"Expected: {expected}, Actual: {actual}");
        }

        [Fact]
        public void ROITest_DivideByZero_ShouldThrow()
        {
            decimal income = 1500m;
            decimal investment = 0m;
            decimal expected = 0m;

            var roi = new ROI();
            var actual = roi.calculation(income, investment);

            Assert.Equal(expected, actual);

            _output.WriteLine($"Income = {income}, investment = {investment}");
            _output.WriteLine($"Expected: {expected}, Actual: {actual}");
        }
    }
}
