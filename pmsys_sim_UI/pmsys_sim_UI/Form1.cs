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

namespace pmsys_sim_UI
{
    public partial class Form1 : Form
    {
        private UserModel m_currentUser;

        public Form1(UserModel user)
        {
            //Form2 login = new Form2();
            //login.m

            m_currentUser = user;

            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void progressBar2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {



            Repository repository2 = new Repository();
            List<ProjectModel> project2 = repository2.GetAll<ProjectModel>();
            ProjectModel prj2 = new ProjectModel();

            Repository repository = new Repository();
            List<ProjectUser> project = repository.GetAll<ProjectUser>();
            ProjectUser prj = new ProjectUser();

            foreach (ProjectUser pr in project)
            {
                if (pr.User.Id== m_currentUser.Id)
                {
                    
                    comboBox5.Items.Add(pr.Project.Name);
                }
                


            }

           
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
