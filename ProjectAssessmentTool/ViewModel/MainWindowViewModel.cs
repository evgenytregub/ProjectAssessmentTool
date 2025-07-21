using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectAssessmentTool.Model;
using ProjectAssessmentTool.Model.Projects.DataModel;
using ProjectAssessmentTool.Model.Projects.Interfaces;
using ProjectAssessmentTool.Model.Projects.Services;
using ProjectAssessmentTool.Model.SystemMessages;
using ProjectAssessmentTool.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace ProjectAssessmentTool.ViewModel
{
    public partial class MainWindowViewModel : ObservableObject
    {
        public ICommand CreateNewProjectCommand { get; } // Command for creating a new project
        public ICommand OpenProjectCommand { get; } // Command for opening a selected project
        public ICommand DeleteProjectCommand { get; } // Command for delete a selected project
        public ObservableCollection<ProjectModel> Projects { get; } = new(); // Collection of all available projects

        // Currently selected project
        [ObservableProperty]
        private ProjectModel? selectedProject;

        [ObservableProperty]
        private string projectName = string.Empty;

        [ObservableProperty]
        private string paybackPeriod = string.Empty;

        [ObservableProperty]
        private string npv = string.Empty;

        [ObservableProperty]
        private string irr = string.Empty;

        [ObservableProperty]
        private string pi = string.Empty;

        [ObservableProperty]
        private string roi = string.Empty;

        [ObservableProperty]
        private string createDate = string.Empty;

        [ObservableProperty]
        private string updateDate = string.Empty;

        // Name of the project to be created
        [ObservableProperty]
        private string newProjectName = string.Empty;

        // Constructor: loads project list and sets up commands
        public MainWindowViewModel()
        {
            LoadProjects();

            CreateNewProjectCommand = new RelayCommand(CreateNewProject);
            OpenProjectCommand = new RelayCommand(() => OpenProject(projectName));
            DeleteProjectCommand = new RelayCommand(() => DeleteProject(projectName));
        }

        // Logic for creating a new project
        private void CreateNewProject()
        {
            var projectData = new ProjectModel
            {
                Name = NewProjectName,
                CreateDate = DateTime.Now,
            };

            ICreateProject newProject = new CreateProject();
            bool newProjectStatus = newProject.Create(projectData);
            if (!newProjectStatus)
            {
                SystemMessages.CreateProject();
                return;
            }
            else
            {
                OpenProject(NewProjectName);
            }
        }

        // Opens the selected project in a new window and closes the main window
        private void OpenProject(string projectName)
        {
            var newWindow = new Assessment(projectName);
            newWindow.Show();
            Application.Current.MainWindow.Close();
        }

        // Delete a selected project
        private void DeleteProject(string projectName)
        {
            var deleteProject = new DeleteProject();
            bool deleteStatus = deleteProject.Delete(projectName);
            SystemMessages.DeleteProject(deleteStatus);
            LoadProjects();
        }

        // Loads all saved projects from data source
        private void LoadProjects()
        {
            var projectsList = new ProjectData().GetData();
            Projects.Clear();
            foreach (var project in projectsList)
                Projects.Add(project);
        }

        // Called automatically when SelectedProject changes
        // Updates all bound fields with data from the selected project
        partial void OnSelectedProjectChanged(ProjectModel? value)
        {
            if (value is not null)
            {
                ProjectName = value.Name;
                CreateDate = value.CreateDate.ToString();
                UpdateDate = value.UpdateDate.ToString();
                Npv = value.NPV.ToString();
                Irr = value.IRR.ToString();
                PaybackPeriod = value.PaybackPeriod.ToString();
                Pi = value.PI.ToString();
                Roi = value.ROI.ToString();
                
            }
        }
    }
}
