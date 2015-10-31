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
        private static string PASSWORD="MySQL";
        private string SELECT_ALL = "SELECT * FROM {0}";
        private string DELETE_BY_PK = "DELETE FROM pmsys_db.{0} WHERE {1} = ?pkId";

        
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
            "(projects_prj_id, users_usr_id, prj_usr_role) VALUES (?prjId, ?usrId, ?role)";

        private string INSERT_USER_ACTIVITY = "INSERT INTO pmsys_db.users_has_activities " +
           "(users_usr_id, activities_act_id, activities_projects_prj_id) "+
            "VALUES (?usrId, ?actId, ?prjId)";

        private string UPDATE_USER_ACTIVITY = "UPDATE pmsys_db.users_has_activities SET " +
            "usr_act_progress = ?progress, usr_act_comments = ?comments " +
            "WHERE users_usr_id = ?usrId AND activities_act_id = ?actId AND activities_projects_prj_id = ?prjId";

        private string NEXT_ID = "SELECT auto_increment FROM INFORMATION_SCHEMA.TABLES " +
            "WHERE table_schema ='pmsys_db' AND table_name = '{0}'";

        public Repository()
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
                    
        protected Int32? Persist(ProjectModel project)
        {
            Int32? id = 0;
            if (OpenConnection())
            {
                MySqlCommand cmd;
                if (project.Id == null || project.Id == 0)
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
        
        protected Int32? Persist(UserModel user)
        {
            Int32? id = 0;
            if (OpenConnection())
            {
                MySqlCommand cmd;
                if (user.Id == null || user.Id == 0)
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
                      
        protected Int32? Persist(ActivityModel activity)
        {
            Int32? id = 0;
            if (OpenConnection())
            {
                MySqlCommand cmd;
                if (activity.Id == null || activity.Id == 0)
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

        protected void LinkUserToProject(Int32? projectId, Int32? userId, string role)
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

        public void Remove<T>(T model) where T : IModel
        {
            if (OpenConnection())
            {

                MySqlCommand cmd;

                Type modelType = typeof(T);

                if (modelType == typeof(UserActivity))
                {
                    string qry = DELETE_BY_PK + " AND {2} = ?pkId2 AND {3} = ?pkId3;" ;

                    cmd = new MySqlCommand(string.Format(qry,
                        Activator.CreateInstance<T>().TableName,
                        "users_usr_id", "activities_act_id", "activities_projects_prj_id"),
                        m_connection);

                    UserActivity us = (UserActivity)Convert.ChangeType(model, typeof(UserActivity));

                    cmd.Parameters.AddWithValue("?pkId", us.ActivityId);
                    cmd.Parameters.AddWithValue("?pkId2", us.ProjectId);
                    cmd.Parameters.AddWithValue("?pkId3", us.UserId);
                }
                else
                {
                    cmd = new MySqlCommand(string.Format(DELETE_BY_PK,
                        Activator.CreateInstance<T>().TableName, Activator.CreateInstance<T>().TablePK),
                        m_connection);
                    cmd.Parameters.AddWithValue("?pkId", model.Id);
                }

                cmd.ExecuteNonQuery();
                CloseConnection();
            }

        }

        public List<T> GetAll<T>() where T : IModel
        {
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(string.Format(SELECT_ALL,
                    Activator.CreateInstance<T>().TableName), m_connection);
                               
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<T> resultList = new List<T>();
                while (dataReader.Read())
                {
                    resultList.Add(CreateModel<T>(dataReader));
                }

                dataReader.Close();
                CloseConnection();

                return resultList;
            }
            return new List<T>();
        }

        private string CreateWhereClasuse(List<MySqlParameter> sqlParams)
        {
            StringBuilder where = new StringBuilder(" WHERE ");
            
            for (int i = 0; i < sqlParams.Count; i++)
            {               
                MySqlParameter currSqlParam = sqlParams[i];
                string format = currSqlParam.Value is string ? "{0}='{1}'" : "{0}={1}";
                
                where.AppendFormat(format, currSqlParam.ParameterName.Replace("?",""), currSqlParam.Value);

                if (i+1 == sqlParams.Count)
                {
                    where.Append(";");
                }
                else
                {
                    where.Append(",");
                }
            }
            
            return where.ToString();
        }

        private T CreateModel<T>(MySqlDataReader dataReader) 
        {
            Type modelType = typeof(T);

            if (modelType == typeof(UserModel)) {
                UserModel user = new UserModel();
                user.Id = dataReader.GetInt32(0);
                user.Name = dataReader.GetString(1);
                user.Pswd = dataReader.GetString(2);

                user.Privileges = (UserModel.UserPrivileges)
                    Enum.Parse(typeof(UserModel.UserPrivileges), dataReader.GetString(4));
                return (T)Convert.ChangeType(user, typeof(T));
            }
            else if (modelType == typeof(ProjectModel)) {
                ProjectModel projectModel = new ProjectModel();
                projectModel.Id = dataReader.GetInt32(0);
                projectModel.Name = dataReader.GetString(1);
                projectModel.Description = dataReader.IsDBNull(2) ? null : dataReader.GetString(2);
                return (T)Convert.ChangeType(projectModel, typeof(T));
            }
            else if (modelType == typeof(ActivityModel)) {
                ActivityModel activity = new ActivityModel();
                activity.Id = dataReader.GetInt32(0);
                activity.Name = dataReader.GetString(1);
                activity.Description = dataReader.IsDBNull(2) ? null : dataReader.GetString(2);
                activity.PlannedStart = dataReader.GetDateTime(3);
                activity.PlannedFinish = dataReader.GetDateTime(4);
                activity.ActualStart = dataReader.IsDBNull(5) ?  (DateTime?)null : dataReader.GetDateTime(5);
                activity.ActualFinish = dataReader.IsDBNull(6) ? (DateTime?)null : dataReader.GetDateTime(6);
                activity.projectid = dataReader.GetInt32(7);
                return (T)Convert.ChangeType(activity, typeof(T));
            } else if (modelType == typeof(ProjectUser))
            {
                ProjectUser projectUser = new ProjectUser();
                projectUser.Project.Id = dataReader.GetInt32(0);
                projectUser.Project.Name = dataReader.GetString(1);
                projectUser.Project.Description = dataReader.IsDBNull(2) ? null : dataReader.GetString(2);
                projectUser.Project.Active = dataReader.GetBoolean(3);

                projectUser.User.Name = dataReader.IsDBNull(5) ? null : dataReader.GetString(5);
                
                
                    projectUser.User.Id = dataReader.IsDBNull(4)? (int?) null : dataReader.GetInt32(4);
                    
                    projectUser.User.Privileges = dataReader.IsDBNull(6) ? (UserModel.UserPrivileges?) null : 
                        (UserModel.UserPrivileges)Enum.Parse(typeof(UserModel.UserPrivileges), dataReader.GetString(6));

                    projectUser.Role = dataReader.IsDBNull(7) ?(Role?) null : 
                        (Role)Enum.Parse(typeof(Role), dataReader.GetString(7));

                    projectUser.User.Active = dataReader.IsDBNull(8) ? false : dataReader.GetBoolean(8);
                
               
                
                return (T)Convert.ChangeType(projectUser, typeof(T));
            }
            else if (modelType == typeof(UserActivity))
            {
                UserActivity userAct = new UserActivity();
                userAct.UserId = dataReader.GetInt32(0);
                userAct.ActivityId = dataReader.GetInt32(1);
                userAct.ProjectId = dataReader.GetInt32(2);
                userAct.Progress = dataReader.GetInt32(3);
                userAct.Comments = dataReader.IsDBNull(4) ? null : dataReader.GetString(4); 

                return (T)Convert.ChangeType(userAct, typeof(T));
            }

            return default(T); 
        }          

    }
}
