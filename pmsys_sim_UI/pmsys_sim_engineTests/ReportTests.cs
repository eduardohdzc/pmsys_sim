using Microsoft.VisualStudio.TestTools.UnitTesting;
using pmsys_sim_engine;
using pmsys_sim_engine.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pmsys_sim_engine.Tests
{
    [TestClass()]
    public class ReportTests
    {
        [TestMethod()]
        public void GetUsersTest()
        {
            Report report = new Report();

            UserModel user = new UserModel();
            user.Name = "user";

            //List<UserModel> users = report.GetUsers(user);

            
        }
    }
}