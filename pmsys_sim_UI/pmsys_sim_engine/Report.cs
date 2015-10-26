using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pmsys_sim_engine.models;
using MySql.Data.MySqlClient;

namespace pmsys_sim_engine
{
    public class Report : Repository
    {

        //public List<UserModel> GetUsers()
        //{
        //    return GetAll<UserModel>(null);
        //}

        //public List<ProjectModel> GetProjects()
        //{
        //    return GetAll<ProjectModel>(null);
        //}

        //public List<ProjectUser> GetProjectUsers()
        //{
        //    return GetAll<ProjectUser>(null);
        //}


        //public List<UserModel> GetUsers(UserModel user)
        //{
        //    List<MySqlParameter> c = new List<MySqlParameter>();


        //    if (!string.IsNullOrEmpty(user.Name))
        //    {
        //        c.Add(new MySqlParameter(UserModel.FIELD_NAME, user.Name));
        //    }

        //    if (!string.IsNullOrEmpty(user.Name))
        //    {
        //        c.Add(new MySqlParameter(UserModel.FIELD_ID, user.Id));
        //    }

        //    c.Add(new MySqlParameter(UserModel.FIELD_PRIVILEGES, user.Privileges));

        //    return GetAll<UserModel>(c);
        //}

    }
}
