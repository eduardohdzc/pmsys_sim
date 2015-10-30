using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pmsys_sim_engine.models
{
    public class UserModel:Repository, IModel
    {
        public static string FIELD_ID = "?usr_id";
        public static string FIELD_NAME = "?usr_name";
        public static string FIELD_STATUS = "?usr_status";
        public static string FIELD_PRIVILEGES = "?usr_privileges";
        public static string FIELD_PSWD= "?usr_pswd";
        

        private Int32? m_id;
        private string m_name;
        private string m_pswd;
        private Boolean m_active;
        
        public enum UserPrivileges {
            USER,
            ADMIN
        }


        public Int32? Id
        {
            get
            {
                return m_id;
            }

            set
            {
                m_id = value;
            }
        }
        
        public string Name
        {
            get
            {
                return m_name;
            }

            set
            {              
                m_name = value;
            }
        }

        public string Pswd
        {
            get
            {
                return m_pswd;
            }

            set
            {
                m_pswd = value;
            }
        }

        public bool Active
        {
            get
            {
                return m_active;
            }

            set
            {
                m_active = value;
            }
        }

        public string TableName
        {
            get
            {
                return "users";
            }
        }

        public UserPrivileges? Privileges { get; set; }

        public string TablePK
        {
            get
            {
                return "usr_id";
            }
        }

        public Int32? Persist()
        {
            this.Id = Persist(this);
            return Id;
        }

    }
}
