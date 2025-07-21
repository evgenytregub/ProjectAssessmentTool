using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAssessmentTool.Model.Assessment.Interfaces
{
    public interface IIRR
    {
        decimal calculation(List<decimal> data);
    }
}
