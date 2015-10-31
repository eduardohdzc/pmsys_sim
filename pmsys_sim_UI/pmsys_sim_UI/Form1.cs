﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pmsys_sim_engine.models;
using MySql.Data.MySqlClient;

namespace pmsys_sim_UI
{
	public enum Role2
    {
        DEVELOPER,
        LEAD,
        TESTER,
    }
    
    public partial class Form1 : Form
    {
        private UserModel m_currentUser;
       // private ProjectUser m_projectUser;
        private static MySqlConnection m_connection;
        private static string SERVER = "localhost";
        private static string DATABASE = "pmsys_db";
        private static string USERNAME = "root";
        private static string PASSWORD = "MySQL";
        private string SELECT_PROID = "SELECT projects.prj_id FROM {0}";
        private string SELECT_ALL = "SELECT * FROM {0}";
        private string proact ="";
        private string actname ="";
        private string idfinal = "";
        private string userfinal = "";

        public Form1(UserModel user)
        {
            //Form2 login = new Form2();
            //login.m
            m_currentUser = user;
           // m_projectUser = projUser;

            InitializeComponent();
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

        private void Form1_Load(object sender, EventArgs e)
        {

            Repository repository2 = new Repository();
            List<ProjectModel> project2 = repository2.GetAll<ProjectModel>();
            ProjectModel prj2 = new ProjectModel();

            Repository repositoryallus = new Repository();
            List<UserModel> allusers = repositoryallus.GetAll<UserModel>();
            UserModel allus = new UserModel();

            Repository repository3 = new Repository();
            List<ActivityModel> project3 = repository3.GetAll<ActivityModel>();
            ActivityModel prj3 = new ActivityModel();

            Repository repository4 = new Repository();
            List<ProjectUser> project4 = repository4.GetAll<ProjectUser>();
            ProjectUser prj4 = new ProjectUser();

            if (m_connection == null)
            {
                m_connection = new MySqlConnection(string.Format("SERVER={0};DATABASE={1};UID={2};PASSWORD={3}",
                    SERVER, DATABASE, USERNAME, PASSWORD));
            }

            foreach (UserModel usr2 in allusers)
            {
                comboBox10.Items.Add(usr2.Name);
                comboBox10.SelectedItem = usr2.Id;
            }
            foreach (ProjectUser pr in project4)
            {
            	string idchingon = pr.User.Id + "" ;
                string idchingon2 = pr.Project.Id + "";
                string idcurrent = m_currentUser.Id + ""; 
               
                if (pr.User.Id== m_currentUser.Id)
                {
                	foreach (ProjectModel pm in project2)
                	{
                		if (pm.Id == pr.Project.Id)
                		{
                			comboBox5.Items.Add(pm.Name);
                			comboBox5.SelectedItem = comboBox5.FindStringExact(pm.Id +"");
                			comboBox11.Items.Add(pm.Name);
                			comboBox11.SelectedItem = comboBox5.FindStringExact(pm.Id + "");
                			comboBox4.Items.Add(pm.Name);
                			comboBox4.SelectedItem = comboBox4.FindStringExact(pm.Id + "");
                		}
                	}                  
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Repository repository4 = new Repository();
            List<ProjectUser> project4 = repository4.GetAll<ProjectUser>();
            ProjectUser prj4 = new ProjectUser();

            Repository repository5 = new Repository();
            List<ActivityModel> project5 = repository5.GetAll<ActivityModel>();
            ActivityModel prj5 = new ActivityModel();

            Repository repository6 = new Repository();
            List<ProjectUser> project6 = repository6.GetAll<ProjectUser>();
            ProjectUser prj6 = new ProjectUser();

            Repository repository7 = new Repository();
            List<UserActivity> project7 = repository7.GetAll<UserActivity>();
            UserActivity prj7 = new UserActivity();

  
            string idproyecto = comboBox4.SelectedIndex + "";
            string role;
            string usname;
            string prname;
            string practive;
            

			foreach (ProjectUser pr in project4){
			    if(pr.Project.Id==(comboBox4.SelectedIndex + 1)){
			    	foreach (ActivityModel am in project5)
			        {
			        	if (pr.Project.Id == am.projectid )
			            {
                            idfinal = am.Id + "";
                            proact = am.Name + "";

                            foreach (UserActivity ua in project7)
                            {
                                idfinal=idfinal;
                                if ((idfinal.Equals(ua.ActivityId + "")) && pr.Project.Id == ua.ProjectId)
                                {

                                    userfinal = ua.UserId + "";

                                    if ((pr.User.Id + "").Equals(userfinal))
                                    {
                                        role = pr.Role + "";
                                        usname = pr.User.Name + "";
                                        prname = pr.Project.Name + "";
                                        practive = pr.Project.Active + "";

                                        dataGridView2.Rows.Add(new object[] { prname, usname, role, proact, practive });
                                       
                                    }


                                }


                            }
			               
			            }
			        }
			        
			    }
			}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (m_currentUser.Privileges.Equals(UserModel.UserPrivileges.ADMIN))
            {
            	if (monthCalendar1.SelectionEnd.Date <= monthCalendar2.SelectionEnd.Date)
            	    {
            	        ProjectUser user1 = new ProjectUser();
            	        ProjectModel project1 = new ProjectModel();
            	        project1.Name = textBox1.Text;
            	        project1.Description = textBox2.Text;
            	        project1.Active = true;
            	
            	        ActivityModel activity = new ActivityModel();
            	        activity.Name = textBox3.Text;
            	        activity.Description = textBox4.Text;
            	        activity.PlannedStart = monthCalendar1.SelectionEnd;
            	        activity.PlannedFinish = monthCalendar2.SelectionEnd;
            	
            	        project1.Activities.Add(activity);
            	        project1.Persist();
            	        project1.Activities[0].AssignUser(m_currentUser.Id);
            	        project1.AssignUser(m_currentUser, pmsys_sim_engine.models.Role.LEAD);
            	        System.Windows.Forms.MessageBox.Show("Project correctly created");
            	    }
            	    else
            	    {
            	        System.Windows.Forms.MessageBox.Show("End date can not be prior to Start date");
            	    }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Just the admin can create projects");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ProjectModel project2 = new ProjectModel();
             ActivityModel activity = new ActivityModel();
            activity.Name = textBox3.Text;
            activity.Description = textBox4.Text;
            activity.PlannedStart = new DateTime();
            activity.PlannedFinish = new DateTime();
            activity.ActualStart = new DateTime();
            activity.ActualFinish = new DateTime();

            project2.Activities.Add(activity);
            //project2.Activities[0].AssignUser();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ProjectModel project = new ProjectModel();
            if (comboBox11.Text == "" || label19.Text == "DEVELOPER")
            {
                System.Windows.Forms.MessageBox.Show("You must select a project and have the LEAD role");
            }
            else
            {
                if (m_currentUser.Privileges.Equals(UserModel.UserPrivileges.ADMIN))
                {
                    if (monthCalendar4.SelectionEnd.Date <= monthCalendar3.SelectionEnd.Date)
                    {
                        Repository repository = new Repository();
                        List<ProjectModel> projects = repository.GetAll<ProjectModel>();
                        List<UserModel> userList = repository.GetAll<UserModel>();

                        foreach (ProjectModel pr in projects)
                        {
                            if(pr.Name.Equals(comboBox11.Text))
                            {
                                ActivityModel activity = new ActivityModel();
                                activity.Name = textBox6.Text;
                                activity.Description = textBox5.Text;
                                activity.PlannedStart = monthCalendar1.SelectionEnd;
                                activity.PlannedFinish = monthCalendar2.SelectionEnd;

                                pr.Activities.Add(activity);
                                pr.Persist();

                                foreach (UserModel usr in userList)
                                {
                                    if(usr.Name.Equals(comboBox10.Text))
                                    {
                                        activity.AssignUser(usr.Id);
                                     pr.AssignUser(usr, pmsys_sim_engine.models.Role.DEVELOPER);
                                   
                                        // pr.Persist();
                                        //pr.AssignUser(usr, pmsys_sim_engine.models.Role.DEVELOPER);
                                        //pr.Activities[1].AssignUser(usr.Id);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("End date can not be prior to Start date");
                    }
                }
            }
        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
        	Repository repository4 = new Repository();
            List<ProjectUser> project4 = repository4.GetAll<ProjectUser>();
            ProjectUser prj4 = new ProjectUser();

            Repository repository5 = new Repository();
            List<ProjectModel> project5 = repository5.GetAll<ProjectModel>();
            ProjectModel prj5 = new ProjectModel();

            foreach (ProjectUser pr in project4)
            {
                string currentchingon = pr.User.Id + "";
                string asdas = m_currentUser.Id + "";
                string bbb = pr.Project.Id + "";
                string aaaa = (comboBox11.SelectedIndex + 1 ) + "";

                if (pr.User.Id == m_currentUser.Id && (pr.Project.Id + "").Equals((comboBox11.SelectedIndex+1) + ""))
                {
                		label19.Text = pr.Role + "";   
                }
            }
        }
        
        private void button5_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
