using ProjectAssessmentTool.Model.Projects.DataModel;
using ProjectAssessmentTool.Model.Projects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace ProjectAssessmentTool.Model.Projects.Services
{
    public class GetProjectData : IGetProjectData
    {
        public List<ProjectModel> GetAllProjectData()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Projects.json");

            if (!File.Exists(path))
                return new List<ProjectModel>();

            string json = File.ReadAllText(path);

            if (string.IsNullOrWhiteSpace(json))
                return new List<ProjectModel>();

            return JsonSerializer.Deserialize<List<ProjectModel>>(json) ?? new List<ProjectModel>();
        }
    }
}
