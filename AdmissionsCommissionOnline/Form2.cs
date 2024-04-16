using AdmissionsCommissionOnline.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AdmissionsCommissionOnline
{

    public partial class Form2 : Form
    {
        BDcontext bDcontext = new BDcontext();
        Enrollee enrollee = new Enrollee();
        public Form2()
        {
            InitializeComponent();
            comboBox1.Items.Add("СПО");
            comboBox1.Items.Add("БАКАЛАВРИАТ");
            comboBox1.Items.Add("СПЕЦИАЛИТЕТ");
            comboBox1.Items.Add("МАГИСТРАТУРА");
            List<USExam> exams = bDcontext.GetUSExam();
            List<string> examsTitle = exams.Select(x => x.Title).ToList();
            for (int i = 0; i < examsTitle.Count; i++)
            {
                comboBox2.Items.Add(examsTitle[i]);
                comboBox3.Items.Add(examsTitle[i]);
                comboBox4.Items.Add(examsTitle[i]);
                comboBox5.Items.Add(examsTitle[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            string educationType = comboBox1.Text;
            Points(educationType);
            List<Education> educations = bDcontext.GetEducationForTupe(educationType);
            List<string> educationsTitle = educations.Select(x => x.Title).ToList();
            for (int i = 0; i < educationsTitle.Count; i++)
            {
                checkedListBox1.Items.Add(educationsTitle[i]);
            }
        }

        private void Points(string educationType)
        {
            if(educationType == "СПО" || educationType == "МАГИСТРАТУРА")
            {
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";
                textBox10.Hide();
                textBox11.Hide();
                textBox12.Hide();
                comboBox2.Hide();
                comboBox3.Hide();
                comboBox4.Hide();
                comboBox5.Hide();
            }    
            else
            {
                textBox10.Show();
                textBox11.Show();
                textBox12.Show();
                comboBox2.Show();
                comboBox3.Show();
                comboBox4.Show(); 
                comboBox5.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF файлы (*.pdf)|*.pdf"; 

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                enrollee.Document = openFileDialog.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                enrollee.FIO = textBox1.Text;
                enrollee.FIOParent = textBox2.Text;
                enrollee.Email = textBox3.Text;
                enrollee.Phone = Convert.ToInt16(textBox6.Text);
                enrollee.SNILS = Convert.ToInt16(textBox7.Text);
                enrollee.Seria = Convert.ToInt16(textBox4.Text);
                enrollee.Number = Convert.ToInt16(textBox5.Text);
                enrollee.EducationalInstitution = textBox8.Text;
                enrollee.Password = textBox13.Text;
                double PoinSum;
                PoinSum = Convert.ToInt16(textBox9.Text);
                if(textBox10.Text != String.Empty)
                    PoinSum += Convert.ToInt16(textBox10.Text);
                if (textBox11.Text != String.Empty)
                    PoinSum += Convert.ToInt16(textBox11.Text);
                if (textBox12.Text != String.Empty)
                    PoinSum += Convert.ToInt16(textBox12.Text);
                enrollee.Point = PoinSum;
                bDcontext.AddEnrollee(enrollee);
                bDcontext.AddUser(enrollee.Email, enrollee.Password);
                MessageBox.Show("Пользователь успешно зарегестрирован");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }
    }
}
