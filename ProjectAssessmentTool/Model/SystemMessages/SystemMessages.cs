using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectAssessmentTool.Model.SystemMessages
{
    public static class SystemMessages
    {
        public static void ValueOutOfRange(string min, string max)
        {
            MessageBox.Show($"The value is out of the allowed range. Valid range from {min} to {max}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void CreateProject()
        {
            MessageBox.Show($"A project with this name already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void DeleteProject(bool deleteStatus)
        {
            MessageBox.Show(deleteStatus ? "Project deleted successfully." : "Failed to delete project.", "Delete Project", MessageBoxButton.OK, deleteStatus ? MessageBoxImage.Information : MessageBoxImage.Error);

        }
    }
}
