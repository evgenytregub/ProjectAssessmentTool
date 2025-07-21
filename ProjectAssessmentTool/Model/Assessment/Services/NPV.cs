using ProjectAssessmentTool.Model.Assessment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAssessmentTool.Model.Assessment.Services
{
    public class NPV : INPV
    {
        public List<decimal> cashFlow { get; set; } = new List<decimal>();

        public decimal calculation(List<decimal> cashFlow, decimal discountRate, decimal investment)
        {
            decimal npv = 0m;
            for (int i = 0; i < cashFlow.Count; i++)
            {
                npv += cashFlow[i] / (decimal)Math.Pow(1 + (double)discountRate, i + 1);
            }

            npv -= investment;

            return Math.Round(npv, 2);
        }
    }
}
