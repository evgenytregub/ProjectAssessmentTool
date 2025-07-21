using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAssessmentTool.Model.Assessment.Interfaces
{
    public interface IPaybackPeriod
    {
        decimal calculation(decimal investments, decimal annualInCame);
    }
}
