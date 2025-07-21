using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAssessmentTool.Model.Projects.DataModel
{
    public class ProjectDataModel
    {
        public string LineName { get; set; } = string.Empty;
        public decimal ValueA { get; set; } = 0.0m;
        public decimal? ValueB { get; set; } = 0.0m;
        public decimal? Sum { get; set; } = 0.0m;
    }
}
