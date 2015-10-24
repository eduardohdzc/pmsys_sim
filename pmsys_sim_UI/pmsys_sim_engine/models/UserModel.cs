using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pmsys_sim_engine.models
{
    public class UserModel:Repository
    {
        private int m_id;
        private string m_name;
        private string m_pswd;
        private Boolean m_active;

        public enum UserPrivileges {
            USER,
            ADMIN
        }


        public int Id
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
                return "Users";
            }
        }

        public UserPrivileges Privileges { get; set; }

        public int Persist()
        {
            this.Id = Persist(this);
            return Id;
        }

    }
}
