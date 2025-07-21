using ProjectAssessmentTool.Model.Projects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using ProjectAssessmentTool.Model.Projects.DataModel;

namespace ProjectAssessmentTool.Model.Projects.Services
{
    public class CreateProject : GetProjectData, ICreateProject
    {
        protected static bool ProjectNameValidation(ProjectModel projectData, List<ProjectModel> projects)
        {
            return !projects.Any(p => p.Name.Equals(projectData.Name, StringComparison.OrdinalIgnoreCase));
        }

        public bool Create(ProjectModel projectData)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Projects.json");

            var projects = new GetProjectData().GetAllProjectData();

            if (projects == null || projects.Count == 0)
                return false;

            if (!ProjectNameValidation(projectData, projects))
                return false;

            projectData.Id = projects.Max(p => p.Id) + 1;
            projects.Add(projectData);

            var options = new JsonSerializerOptions { WriteIndented = true };
            string updatedJson = JsonSerializer.Serialize(projects, options);
            File.WriteAllText(path, updatedJson);

            return true;
        }

    }
}
