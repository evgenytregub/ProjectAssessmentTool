using ProjectAssessmentTool.Model.Assessment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAssessmentTool.Model.Assessment.Services
{
    public class PI : IPI
    {
        public decimal calculation(List<decimal> cashFlows, decimal investments)
        {
            if (investments == 0 || cashFlows == null || cashFlows.Count == 0)
                return 0m;

            decimal pi = 0m;
            decimal discountRate = 0.1m;

            for (int year = 0; year < cashFlows.Count; year++)
            {
                double denom = Math.Pow(1 + (double)discountRate, year + 1);
                pi += cashFlows[year] / (decimal)denom;
            }

            return Math.Round(pi / investments, 2);
        }
    }
}
