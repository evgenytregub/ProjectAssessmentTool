using ProjectAssessmentTool.Model.Projects.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAssessmentTool.Model.Projects.Interfaces
{
    public interface ICreateProject
    {
        bool Create(ProjectModel projectData);
    }
}
