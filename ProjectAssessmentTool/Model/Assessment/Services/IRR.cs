using ProjectAssessmentTool.Model.Assessment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAssessmentTool.Model.Assessment.Services
{
    public class IRR : IIRR
    {
        public List<decimal> cashFlow { get; set; }
        public decimal calculation(List<decimal> cashFlows)
        {
            if (cashFlows == null || cashFlows.Count == 0)
               return 0;

            const int maxIterations = 1000;
            const decimal tolerance = 0.00001m;
            decimal rate = 0.1m; // initial guess

            for (int iter = 0; iter < maxIterations; iter++)
            {
                decimal npv = 0;
                decimal derivative = 0;

                for (int t = 0; t < cashFlows.Count; t++)
                {
                    decimal cf = cashFlows[t];
                    double denom = Math.Pow(1 + (double)rate, t);

                    if (denom == 0 || double.IsInfinity(denom))
                        return 0;

                    npv += cf / (decimal)denom;

                    if (t > 0)
                    {
                        derivative -= (t * cf) / (decimal)(denom * (1 + (double)rate));
                    }
                }

                if (Math.Abs(npv) < tolerance)
                    return Math.Round(rate * 100, 5); // IRR as percentage

                if (derivative == 0)
                    return 0;

                rate -= npv / derivative;

                if (rate <= -1 || rate > 10) // sanity bounds
                    return 0;
            }

            throw new Exception("IRR did not converge.");
        }

    }
}
