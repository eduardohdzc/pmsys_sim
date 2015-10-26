using Microsoft.VisualStudio.TestTools.UnitTesting;
using pmsys_sim_engine.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pmsys_sim_engine.Tests
{
    [TestClass()]
    public class RepositoryTests
    {
        [TestMethod()]
        public void GetAll()
        {
            Repository repository = new Repository();

            List<UserModel> userList = repository.GetAll<UserModel>();
            List<ProjectModel> projects = repository.GetAll<ProjectModel>();
            List<ActivityModel> activities = repository.GetAll<ActivityModel>();
            List<ProjectUser> projectsUsers = repository.GetAll<ProjectUser>();

        }

        [TestMethod()]
        public void PersistProject()
        {
            UserModel user = new UserModel();

            user.Name = "user";
            user.Pswd = "pswd";
            user.Privileges = UserModel.UserPrivileges.USER;
            user.Id = user.Persist();

            ProjectModel project = new ProjectModel();
            project.Name = "projectName";
            project.Description = "description";
            project.Active = true;

            ActivityModel activity = new ActivityModel();
            activity.Name = "name";
            activity.Description = "desc";
            activity.PlannedStart = new DateTime();
            activity.PlannedFinish = new DateTime();
            activity.ActualStart = new DateTime();
            activity.ActualFinish = new DateTime();

            project.Activities.Add(activity);
            project.Persist();
            
            project.AssignUser(user, Role.DEVELOPER);

            project.Activities[0].AssignUser(user.Id);
            project.Activities[0].UpdateProgress(user.Id, 100, "finish");


            project.Activities[0].RemoveUser(user.Id);

        }


        [TestMethod()]
        public void Remove()
        {
            Repository repository = new Repository();
            UserModel user = new UserModel();
            user.Id = 9;
            repository.Remove<UserModel>(user);
            

        }

    }
}