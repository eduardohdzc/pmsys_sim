using System;
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
	public partial class Form2 : Form
    {
        UserModel m_user;
        
        public Form2()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
        	System.Windows.Forms.MessageBox.Show("Enter user and password.");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Repository repository = new Repository();

            List<UserModel> users =  repository.GetAll<UserModel>();

            bool valid = false;
            bool exist = false;
           
            UserModel test = new UserModel();

                if (checkBox1.Checked)
                {
                    foreach (UserModel ex in users)
                    {
                        if (ex.Name.Equals(textBox1.Text))
                        {
                            System.Windows.Forms.MessageBox.Show("User already exists.");
                            valid = true;
                            exist = true;
                            break;
                        }
                        else
                        {
                            exist = false;
                        }
                    }

                    if (exist == false)
                    {
                       
                        if ((textBox1.Text == "") || (textBox2.Text == "") || (textBox3.Text == ""))
                        {
                        	System.Windows.Forms.MessageBox.Show("Invalid data.");
                       	}
                       	else
                        {
							if (textBox2.Text == textBox3.Text)
							{
							    UserModel user = new UserModel();
							
							    user.Name = textBox1.Text;
							    user.Pswd = textBox2.Text;
							    user.Privileges = UserModel.UserPrivileges.USER;
							    user.Id = user.Persist();
							    System.Windows.Forms.MessageBox.Show("User correctly created.");
							
							    textBox1.Clear();
							    textBox2.Clear();
							    textBox3.Clear();
							    checkBox1.Checked = false;
							
							    var ventana = new Form1(user);
							    ventana.Show();
							}
                            else
                            {
                            	System.Windows.Forms.MessageBox.Show("Password does not mach.");
                                 }
                         	}
                         	valid = true;
                    	}
                }
                else
                {
                	foreach (UserModel us in users)
                	{              
                		if( us.Name.Equals(textBox1.Text))
                        {
                            valid = true;
                            if (us.Pswd.Equals(textBox2.Text))
                            {
                            	var ventana = new Form1(us);
                            	ventana.Show();
								break;
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("incorrect password");
                                break;
                            }
                        }
                    }
                }
                if (!valid)
                {
                    System.Windows.Forms.MessageBox.Show("User does not exist");
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                label3.Enabled = true;
                label3.Visible = true;
                textBox3.Enabled = true;
                textBox3.Visible = true;
                textBox2.Text = "";
                textBox2.PasswordChar = '\0';
            }
            else
            {
                label3.Enabled = false;
                label3.Visible = false;
                textBox3.Enabled = false;
                textBox3.Visible = false;
                textBox2.PasswordChar = '*';
            }
        }
    }
}
