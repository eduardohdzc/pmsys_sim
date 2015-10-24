using pmsys_sim_engine.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace pmsys_sim_engine.models
{
    public class Repository        
    {
        private static MySqlConnection m_connection;
        private static string SERVER="localhost";
        private static string DATABASE="pmsys_db";
        private static string USERNAME="root";
        private static string PASSWORD="mysql";
        private string SELECT_ALL = "SELECT * FROM {0}";
        private static Repository m_instance;

        private String INSERT_USER =
         "INSERT INTO pmsys_db.users (usr_name, usr_pswd, usr_status, usr_privileges) VALUES " +
             "(?name, ?password, ?status, ?privileges);";

        private String UPDATE_USER =
        "UPDATE pmsys_db.users SET usr_name = ?name, usr_pswd = ?password, usr_status = ?status, usr_privileges = ?privileges " +
           "WHERE usr_id = ?;";

        private String INSERT_PROJECT =
           "INSERT INTO pmsys_db.projects (prj_name, prj_description, prj_status) VALUES " +
               "(?name, ?description, ?status);";

        private String UPDATE_PROJECT =
            "UPDATE pmsys_db.projects SET prj_name = ?name , prj_description = ?description, prj_status = ?status " +
                "WHERE prj_id = ?id;";

        private String INSERT_ACTIVITY = "INSERT INTO pmsys_db.activities " +
           "(act_name, act_description, act_planned_start, act_planned_finish, act_real_start, act_real_finish, projects_prj_id) " +
           "VALUES (?name, ?description, ?pStart, ?pFinish, ?aStart, ?aFinish, ?aProjectId);";

        private String UPDATE_ACTIVITY = "UPDATE pmsys_db.activities SET " +
            "act_name = ?name, act_description = ?description, act_planned_start = ?pStart, act_planned_finish = ?pFinish, " +
            "act_real_start = ?aStart, act_real_finish = ?aFinish WHERE act_id = ?;";
        
        private string INSERT_USER_PROJECT = "INSERT INTO pmsys_db.projects_has_users "+
            "(projects_prj_id, users_usr_id, role) VALUES (?prjId, ?usrId, ?role)";

        private string INSERT_USER_ACTIVITY = "INSERT INTO pmsys_db.users_has_activities " +
           "(users_usr_id, activities_act_id, activities_projects_prj_id) "+
            "VALUES (?usrId, ?actId, ?prjId)";

        private string UPDATE_USER_ACTIVITY = "UPDATE pmsys_db.users_has_activities SET " +
            "usr_act_progress = ?progress, usr_act_comments = ?comments " +
            "WHERE users_usr_id = ?usrId AND activities_act_id = ?actId AND activities_projects_prj_id = ?prjId";

        private string NEXT_ID = "SELECT auto_increment FROM INFORMATION_SCHEMA.TABLES " +
            "WHERE table_schema ='pmsys_db' AND table_name = '{0}'";

        protected Repository()
        {
            Initialize();
        }
                
        private void Initialize()
        {
            if (m_connection == null)
            {
                m_connection = new MySqlConnection(string.Format("SERVER={0};DATABASE={1};UID={2};PASSWORD={3}",
                    SERVER, DATABASE, USERNAME, PASSWORD));
            }
        }

        private bool OpenConnection()
        {
            try
            {
                m_connection.Open();
                return true;
            }
            catch (MySqlException e)
            {                
                return false;
            }
        }
        
        private bool CloseConnection()
        {
            try
            {
                m_connection.Close();
                return true;
            }
            catch (MySqlException e)
            {
                return false;
            }
        }
                    
        protected int Persist(ProjectModel project)
        {
            int id = 0;
            if (OpenConnection())
            {
                MySqlCommand cmd;
                if (project.Id == 0)
                {
                    id = GetNextId(project.TableName);
                    cmd = new MySqlCommand(INSERT_PROJECT, m_connection);
                }
                else
                {
                    cmd = new MySqlCommand(UPDATE_PROJECT, m_connection);
                    cmd.Parameters.AddWithValue("?id", project.Id);
                    id = project.Id;
                }

                cmd.Parameters.AddWithValue("?name", project.Name);
                cmd.Parameters.AddWithValue("?description", project.Description);
                cmd.Parameters.AddWithValue("?status", true);
                cmd.ExecuteNonQuery();
                CloseConnection();                
            }
            return id;
        }
        
        protected int Persist(UserModel user)
        {
            int id = 0;
            if (OpenConnection())
            {
                MySqlCommand cmd;
                if (user.Id == 0)
                {
                    id = GetNextId(user.TableName);
                    cmd = new MySqlCommand(INSERT_USER, m_connection);
                }
                else
                {
                    cmd = new MySqlCommand(UPDATE_USER, m_connection);
                    cmd.Parameters.AddWithValue("?id", user.Id);
                    id = user.Id;
                }                

                cmd.Parameters.AddWithValue("?name", user.Name);
                cmd.Parameters.AddWithValue("?password", user.Pswd);
                cmd.Parameters.AddWithValue("?status", true);
                cmd.Parameters.AddWithValue("?privileges", user.Privileges.ToString());
                cmd.ExecuteNonQuery();

                CloseConnection();
            }
            return id;
        }
                      
        protected int Persist(ActivityModel activity)
        {
            int id = 0;
            if (OpenConnection())
            {
                MySqlCommand cmd;
                if (activity.Id == 0)
                {
                    id = GetNextId(activity.TableName);
                    cmd = new MySqlCommand(INSERT_ACTIVITY, m_connection);
                }
                else
                {
                    cmd = new MySqlCommand(UPDATE_ACTIVITY, m_connection);
                    cmd.Parameters.AddWithValue("?id", activity.Id);
                    id = activity.Id;
                }                

                cmd.Parameters.AddWithValue("?name", activity.Name);
                cmd.Parameters.AddWithValue("?description", activity.Description);
                cmd.Parameters.AddWithValue("?pStart", activity.PlannedStart);
                cmd.Parameters.AddWithValue("?pFinish", activity.PlannedFinish);
                cmd.Parameters.AddWithValue("?aStart", activity.ActualStart);
                cmd.Parameters.AddWithValue("?aFinish", activity.ActualFinish);
                cmd.Parameters.AddWithValue("?aProjectId", activity.ProjectId);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
            return id;
        }
         
        protected void Persist(UserActivity userActivity )
        {            
            if (OpenConnection())
            {
                MySqlCommand cmd;
                if (userActivity.Progress == 0)
                {
                    cmd = new MySqlCommand(INSERT_USER_ACTIVITY, m_connection);                    
                }
                else
                {
                    cmd = new MySqlCommand(UPDATE_USER_ACTIVITY, m_connection);
                    
                    cmd.Parameters.AddWithValue("?progress", userActivity.Progress);
                    cmd.Parameters.AddWithValue("?comments", userActivity.Comments);                    
                }

                cmd.Parameters.AddWithValue("?usrId", userActivity.UserId);
                cmd.Parameters.AddWithValue("?actId", userActivity.ActivityId);
                cmd.Parameters.AddWithValue("?prjId", userActivity.ProjectId);

                cmd.ExecuteNonQuery();
                CloseConnection();
            }                       
        }

        protected void LinkUserToProject(int projectId, int userId, string role)
        {
            if (OpenConnection())
            {
                MySqlCommand cmd;
                cmd = new MySqlCommand(INSERT_USER_PROJECT, m_connection);
                cmd.Parameters.AddWithValue("?prjId", projectId);
                cmd.Parameters.AddWithValue("?usrId", userId);
                cmd.Parameters.AddWithValue("?role", role);
               
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }

        private int GetNextId(string table)
        {
            int id = 1;
            MySqlCommand cmd = new MySqlCommand(string.Format(NEXT_ID, table), m_connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                dataReader.Read();
                id = dataReader.GetInt32(0);                                
            }
            dataReader.Close();
            return id;
        }

        void Remove()
        {


        }

        void Get(int id)
        {

        }

        //public List<T> GetAll<T>() where T : IModel
        //{
        //    if (OpenConnection())
        //    {                
        //        MySqlCommand cmd = new MySqlCommand(string.Format(SELECT_ALL, 
        //            Activator.CreateInstance<T>().TableName), m_connection);
        //        MySqlDataReader dataReader = cmd.ExecuteReader();

        //        List<T> resultList = new List<T>();
        //        while (dataReader.Read())
        //        {
        //            resultList.Add(CreateModel<T>(dataReader));                    
        //        }
                
        //        dataReader.Close();
        //        CloseConnection();

        //        return resultList;
        //    }
        //    return new List<T>();
        //}

        private T CreateModel<T>(MySqlDataReader dataReader) 
        {
            Type modelType = typeof(T);
            
            if (modelType == typeof(UserModel)){
                UserModel user = new UserModel();
                user.Id = dataReader.GetInt32(0);
                user.Name = dataReader.GetString(1);
                
                user.Privileges =(UserModel.UserPrivileges) 
                    Enum.Parse(typeof(UserModel.UserPrivileges), dataReader.GetString(4));
                return (T)Convert.ChangeType(user, typeof(T));
            }
            else if(modelType == typeof(ProjectModel)){
                ProjectModel projectModel = new ProjectModel();
                projectModel.Id = dataReader.GetInt32(0);
                projectModel.Name = dataReader.GetString(1);
                projectModel.Description = dataReader.GetString(2);                
                return (T)Convert.ChangeType(projectModel, typeof(T));
            }
            else if(modelType == typeof(ActivityModel)){
                ActivityModel activity = new ActivityModel();
                activity.Id = dataReader.GetInt32(0);
                activity.Name = dataReader.GetString(1);
                activity.Description = dataReader.GetString(2);
                activity.PlannedStart = dataReader.GetDateTime(3);
                activity.PlannedFinish= dataReader.GetDateTime(4);
                activity.ActualStart = dataReader.GetDateTime(5);
                activity.ActualFinish = dataReader.GetDateTime(6);
                return (T)Convert.ChangeType(activity, typeof(T));
            }            

            return default(T); 
        }          

    }
}
