using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using ProjectAssessmentTool.Model.Projects.DataModel;
using ProjectAssessmentTool.Model.Projects.Interfaces;
using System.Runtime.Serialization;

namespace ProjectAssessmentTool.Model.Projects.Services
{
    public class SaveProjectData : GetProjectData, ISaveProjectData
    {
        public bool Save(ProjectModel projectData)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Projects.json");

            var projectDataService = new GetProjectData();
            var projects = projectDataService.GetAllProjectData();

            if (projects == null)
                return false;

            var existing = projects.FirstOrDefault(p => p.Name.Equals(projectData.Name, StringComparison.OrdinalIgnoreCase));

            if (existing != null)
            {
                UpdateProject(existing, projectData);
            }
            else
            {
                projects.Add(projectData);
            }

            var json = JsonSerializer.Serialize(projects, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);

            return true;
        }
        private void UpdateProject(ProjectModel target, ProjectModel source)
        {
            target.Name = source.Name;
            target.CreateDate = source.CreateDate;
            target.PlantСapacity = source.PlantСapacity;
            target.PlantCapacityData = source.PlantCapacityData;
            target.PlantCapacityTotal = source.PlantCapacityTotal;
            target.OpexData = source.OpexData;
            target.OpexStaff = source.OpexStaff;
            target.OpexTotalSum = source.OpexTotalSum;
            target.CapexData = source.CapexData;
            target.CapexTotalSum = source.CapexTotalSum;
            target.CashFlowData = source.CashFlowData;
            target.CashFlowSum = source.CashFlowSum;
            target.NPV = source.NPV;
            target.IRR = source.IRR;
            target.PaybackPeriod = source.PaybackPeriod;
            target.PI = source.PI;
            target.ROI = source.ROI;
        }

    }
}
