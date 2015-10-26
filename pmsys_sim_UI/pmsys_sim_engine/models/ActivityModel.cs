using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pmsys_sim_engine.models
{
    public class ActivityModel: Repository, IModel
    {

        private Int32? m_id;
        private string m_name;
        private string m_description;
        private DateTime m_plannedStart;
        private DateTime m_plannedFinish;
        private DateTime? m_actualStart;
        private DateTime? m_actualFinish;

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

        public DateTime PlannedStart
        {
            get
            {
                return m_plannedStart;
            }

            set
            {
                m_plannedStart = value;
            }
        }

        public DateTime PlannedFinish
        {
            get
            {
                return m_plannedFinish;
            }

            set
            {
                m_plannedFinish = value;
            }
        }

        public DateTime? ActualStart
        {
            get
            {
                return m_actualStart;
            }

            set
            {
                m_actualStart = value;
            }
        }

        public DateTime ?ActualFinish
        {
            get
            {
                return m_actualFinish;
            }

            set
            {
                m_actualFinish = value;
            }
        }

        public string TableName
        {
            get
            {
                return "Activities";
            }
        }

        public string TablePK
        {
            get
            {
                return "act_id";
            }
        }

        internal Int32? ProjectId { get; set; }

        public Int32? Persist()
        {
            this.Id = Persist(this);
            return this.Id;
        }

        public void AssignUser(Int32? userId)
        {   
            UserActivity u = new UserActivity();
            u.UserId = userId;
            u.ProjectId = this.ProjectId;
            u.ActivityId = this.Id;
            u.Progress = 0;
            u.Comments = "";
            Persist(u);
        }

        public void RemoveUser(Int32? userId)
        {
            UserActivity u = new UserActivity();
            u.UserId = userId;
            u.ProjectId = this.ProjectId;
            u.ActivityId = this.Id;
            u.Progress = 0;
            u.Comments = "";
            Remove<UserActivity>(u);
        }



        public void UpdateProgress(Int32? userId, int progress, string comments)
        {   
            UserActivity u = new UserActivity();
            u.UserId = userId;
            u.ProjectId = this.ProjectId;
            u.ActivityId = this.Id;
            u.Progress = progress;
            u.Comments = comments;
            Persist(u);
        }
        
    }

   
}
