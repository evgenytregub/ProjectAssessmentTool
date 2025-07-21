using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAssessmentTool.Model.Projects.DataModel
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        // Initial Data
        public decimal PlantСapacity { get; set; }
        public List<ProjectDataModel> PlantCapacityData { get; set; } = new List<ProjectDataModel>();
        public decimal PlantCapacityTotal { get; set; }
        public List<ProjectDataModel> OpexData { get; set; } = new List<ProjectDataModel>();
        public decimal OpexStaff { get; set; }
        public decimal OpexTotalSum { get; set; }
        public List<ProjectDataModel> CapexData { get; set; } = new List<ProjectDataModel>();
        public decimal CapexTotalSum { get; set; }
        public List<ProjectDataModel> CashFlowData { get; set; } = new List<ProjectDataModel>();
        public List<object> CashFlowSum { get; set; } = new List<object>();

        public decimal NPV { get; set; }
        public decimal IRR { get; set; }
        public decimal PaybackPeriod { get; set; }
        public decimal PI { get; set; }
        public decimal ROI { get; set; }
    }
}
