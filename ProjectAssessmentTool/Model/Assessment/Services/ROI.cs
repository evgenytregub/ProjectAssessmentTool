using ProjectAssessmentTool.Model.Assessment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAssessmentTool.Model.Assessment.Services
{
    public class ROI : IROI
    {
        public decimal calculation(decimal income, decimal investment)
        {
            if (investment == 0)
                return 0;

            return Math.Round((income - investment) / investment * 100, 2);
        }
    }
}
