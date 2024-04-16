using AdmissionsCommissionOnline.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdmissionsCommissionOnline
{
    public partial class Form1 : Form
    {
        BDcontext bDcontext = new BDcontext();
        public Form1()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form2().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (bDcontext.AuthenticateUser(textBox1.Text, textBox2.Text))
            {
               User user = bDcontext.GetUserByEmail(textBox1.Text);
                this.Hide();
                new Form3(user).ShowDialog();
            }

        }
    }
}
