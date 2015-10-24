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
        public void GetAllTest()
        {
           // Repository repository = Repository.Instance;
           // List<UserModel> userList = repository.GetAll<UserModel>();
                        
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
            
            project.AddUser(user, Role.DEVELOPER);

            project.Activities[0].AssignUser(user.Id);
            project.Activities[0].UpdateProgress(user.Id, 100, "finish");


        }

    }
}