using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pmsys_sim_engine.models
{

    public enum Role
    {
        DEVELOPER,
        LEAD,
        TESTER,

    }

    public class ProjectModel : Repository
    {
        private int m_id;
        private string m_name;
        private string m_description;
        private Boolean m_active;

        private List<ActivityModel> m_activities;

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

        public string Description
        {
            get
            {
                return m_description;
            }

            set
            {
                m_description = value;
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
                
                return "Projects";
            }
        }

        public List<ActivityModel> Activities
        {
            get
            {
                if(m_activities == null)
                {
                    m_activities = new List<ActivityModel>();
                }
                return m_activities;
            }         
        }

        public int Persist()
        {
            this.Id = Persist(this);

            foreach (ActivityModel activity in this.Activities)
            {
                activity.ProjectId = this.Id;
                activity.Persist();                
            }

            return this.Id;
        }

        public void AddUser(UserModel user, Role role)
        {
            LinkUserToProject(this.Id, user.Id, role.ToString());
        }
            

    }
}
