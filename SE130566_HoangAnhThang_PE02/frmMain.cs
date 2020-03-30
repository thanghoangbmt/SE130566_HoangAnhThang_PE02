using StudentAssemblies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE130566_HoangAnhThang_PE02
{
    /*
     Họ tên: Hoàng Anh Thắng
     StudentID: SE130566
     Mã đề: PE02
     Ngày thi: 30/03/2020, giờ thi: 12:30:00
     Thời gian 85 phút.
    */
    public partial class frmMain : Form
    {
        StudentData studentData = new StudentData();
        List<Student> list = null;

        public frmMain()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            list = studentData.GetListStudent();
            dgvStudent.DataSource = list;

            if (list != null || list.Count > 0)
                dgvStudent.Rows[0].Selected = true;

            loadDataForTextBox();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string StudentName = txtStudentName.Text.Trim();

            if (string.IsNullOrEmpty(StudentName))
            {
                MessageBox.Show("Please input Student Name");
            }

            DateTime Dob;
            try
            {
                Dob = DateTime.Parse(txtDob.Text);
            }
            catch
            {
                MessageBox.Show("You must type Dob that follow dd/mm/yyyy format !!!");
                return;
            }

            string Email = txtEmail.Text;

            bool validMail = IsValidMail(Email);
            if (!validMail)
            {
                MessageBox.Show("Please type Email correct format !!!");
                return;
            }

            Student student = new Student { Name = StudentName, Dob = Dob, Email = Email };
            bool result = studentData.AddNewProduct(student);
            if (result)
            {
                MessageBox.Show("Add successful");
                LoadData();
            }
            else
            {
                MessageBox.Show("Add failed");
            }
        }



        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvStudent_SelectionChanged(object sender, EventArgs e)
        {
            loadDataForTextBox();
        }

        private void loadDataForTextBox()
        {
            txtStudentID.Clear();
            txtStudentName.Clear();
            txtDob.Clear();
            txtEmail.Clear();

            txtStudentID.Text = dgvStudent.CurrentRow.Cells[0].Value.ToString();
            txtStudentName.Text = dgvStudent.CurrentRow.Cells[1].Value.ToString();
            txtDob.Text = dgvStudent.CurrentRow.Cells[2].Value.ToString();
            txtEmail.Text = dgvStudent.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(txtStudentID.Text);
            string StudentName = txtStudentName.Text.Trim();


            DateTime Dob;

            if (string.IsNullOrEmpty(StudentName))
            {
                MessageBox.Show("Please input Student Name");
            }

            try
            {
                Dob = DateTime.Parse(txtDob.Text);
            }
            catch
            {
                MessageBox.Show("You must type Dob that follow dd/mm/yyyy format !!!");
                return;
            }

            string Email = txtEmail.Text;

            bool validMail = IsValidMail(Email);
            if (!validMail)
            {
                MessageBox.Show("Please type Email correct format !!!");
                return;
            }

            Student student = new Student { ID = ID, Name = StudentName, Dob = Dob, Email = Email };
            bool result = studentData.UpdateProduct(student);
            if (result)
            {
                MessageBox.Show("Update successful");
                LoadData();
            }
            else
            {
                MessageBox.Show("Update failed");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(txtStudentID.Text);
            Student student = new Student { ID = ID };
            bool result = studentData.DeleteStudent(student);
            if (result)
            {
                MessageBox.Show("Delete successful");
                LoadData();
            }
            else
            {
                MessageBox.Show("Delete failed");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim();

            list = studentData.FindStudent(search);

            dgvStudent.DataSource = list;

            if (list != null || list.Count > 0)
                dgvStudent.Rows[0].Selected = true;

            loadDataForTextBox();
        }

        private bool IsValidData()
        {

            return true;
        }

        private bool IsValidMail(string Email)
        {
            try
            {
                MailAddress m = new MailAddress(Email);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
