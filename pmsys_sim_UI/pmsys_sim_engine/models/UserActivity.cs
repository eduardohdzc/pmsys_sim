using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pmsys_sim_engine.models
{
    public class UserActivity : IModel
    {
        private Int32? userId;
        private Int32? projectId;
        private Int32? activityId;
        private int progress;
        private string comments;

        public Int32? UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value;
            }
        }

        public Int32? ProjectId
        {
            get
            {
                return projectId;
            }

            set
            {
                projectId = value;
            }
        }

        public Int32? ActivityId
        {
            get
            {
                return activityId;
            }

            set
            {
                activityId = value;
            }
        }

        public int Progress
        {
            get
            {
                return progress;
            }

            set
            {
                progress = value;
            }
        }

        public string Comments
        {
            get
            {
                return comments;
            }

            set
            {
                comments = value;
            }
        }

        public string TableName
        {
            get
            {
                return "users_has_activities";
            }
        }

        public string TablePK
        {
            get
            {
                return "users_usr_id";
            }
        }

        public int? Id
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
