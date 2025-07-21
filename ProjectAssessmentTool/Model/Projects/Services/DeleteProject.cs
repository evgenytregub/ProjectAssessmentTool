using ProjectAssessmentTool.Model.Projects.DataModel;
using ProjectAssessmentTool.Model.Projects.Interfaces;
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
    public class DeleteProject : GetProjectData, IDeleteProject
    {
        public bool Delete(string projectName)
        {
            MessageBox.Show(projectName); // MessageBox is part of System.Windows namespace
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Projects.json");

            var projectDataService = new GetProjectData();
            var projects = projectDataService.GetAllProjectData();

            if (projects == null)
                return false;

            var existing = projects.FirstOrDefault(p => p.Name.Equals(projectName, StringComparison.OrdinalIgnoreCase));

            if (existing != null)
            {
                projects.Remove(existing);
            }
            else
            {
                return false; // Project not found
            }

            var json = JsonSerializer.Serialize(projects, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);

            return true;
        }
    }
}
