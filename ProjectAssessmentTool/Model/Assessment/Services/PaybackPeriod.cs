using ProjectAssessmentTool.Model.Assessment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAssessmentTool.Model.Assessment.Services
{
    public class PaybackPeriod : IPaybackPeriod
    {
        public decimal calculation(decimal investments, decimal annualInCame)
        {
            decimal paybackPeriod = Math.Round(investments / (annualInCame <= 0 ? 1 : annualInCame), 2);
            return paybackPeriod;
        }
    }
}
