using ProjectAssessmentTool.Model.Projects.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;


namespace ProjectAssessmentTool.Model.Projects.Services
{
    public class ProjectData
    {
        public List<ProjectModel> GetData()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Projects.json");

            if (!File.Exists(path))
                return new List<ProjectModel>();

            var json = File.ReadAllText(path);

            return string.IsNullOrWhiteSpace(json)
                ? new List<ProjectModel>()
                : JsonSerializer.Deserialize<List<ProjectModel>>(json) ?? new List<ProjectModel>();
        }
        public List<ProjectModel> GetProjectData(string searchName)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Projects.json");

            if (!File.Exists(path))
                return new List<ProjectModel>();

            var json = File.ReadAllText(path);

            if (string.IsNullOrWhiteSpace(json))
                return new List<ProjectModel>();

            var projects = JsonSerializer.Deserialize<List<ProjectModel>>(json) ?? new();

            var match = projects.FirstOrDefault(p => p.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));

            return match != null ? new List<ProjectModel> { match } : new List<ProjectModel>();
        }

    }
}
