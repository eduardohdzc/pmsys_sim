using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pmsys_sim_engine.models
{
    public class ProjectUser : IModel
    {
        private ProjectModel m_project;
        private UserModel m_user;
        private Role? m_role;


        public ProjectUser()
        {
            m_project = new ProjectModel();
            m_user = new UserModel();
        }

        public ProjectModel Project
        {
            get
            {
                return m_project;
            }

            set
            {
                m_project = value;
            }
        }

        public UserModel User
        {
            get
            {
                return m_user;
            }

            set
            {
                m_user = value;
            }
        }

        public Role? Role
        {
            get
            {
                return m_role;
            }

            set
            {
                m_role = value;
            }
        }

        public string TableName
        {
            get
            {
                return "project_user";
            }                      
        }

        public string TablePK
        {
            get
            {
                return "prj_id";
            }
        }

        public int Id
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        int? IModel.Id
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
