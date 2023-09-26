using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using testTH4.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace testTH4
{
    public partial class Form1 : Form
    {
        //StudentDBContext db = new StudentDBContext();
        Model1 context = new Model1();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //Model1 context = new Model1();
                List<Student> dssv = context.Students.ToList();
                List<Faculty> dskhoa = context.Faculties.ToList();
                FillFacultyCombobox(dskhoa);
                BindGrid(dssv);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BindGrid(List<Student> liststudent)
        {
            dataGridView1.Rows.Clear();
            foreach(var item in liststudent)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = item.StudentID;
                dataGridView1.Rows[index].Cells[1].Value = item.FullName;
                dataGridView1.Rows[index].Cells[2].Value = item.Faculty.FacultyName;
                dataGridView1.Rows[index].Cells[3].Value = item.AverageScore;
            }
        }

        private void FillFacultyCombobox(List<Faculty> listfaculty)
        {
            this.cmbKhoa.DataSource = listfaculty;
            this.cmbKhoa.DisplayMember = "FacultyName";
            this.cmbKhoa.ValueMember = "FacultyID";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMSSV.Text == "" || txtTen.Text == "" || txtDTB.Text == "")
            {
                MessageBox.Show("Nhap du thong tin");
            }
            else
            {
                string ms = txtMSSV.Text;
                int res = ms.ToCharArray().Length;
                if (res == 10)
                {
                    Student s = new Student() { StudentID = txtMSSV.Text, FullName = txtTen.Text, FacultyID = cmbKhoa.SelectedIndex + 1, AverageScore = double.Parse(txtDTB.Text) };
                    context.Students.Add(s);
                    context.SaveChanges();
                    MessageBox.Show("Them thanh cong");
                }
                else
                {
                    MessageBox.Show("chi nhap 10 ki tu");
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var rowData = dataGridView1.SelectedRows[0].Cells["Column1"].Value.ToString();
            Student s = context.Students.Find(rowData);
            context.Students.Remove(s);
            context.SaveChanges();
            MessageBox.Show("Xoa thanh cong");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            String id = (dataGridView1.SelectedRows[0].Cells["Column1"].Value.ToString());
            Student st = context.Students.Find(id);
            if (st != null)
            {
                st.FullName = txtTen.Text;
                string facultyName = cmbKhoa.Text;
                Faculty faculty = context.Faculties.FirstOrDefault(f => f.FacultyName == facultyName);
                if (faculty != null)
                {
                    st.Faculty = faculty;
                }
                else
                {
                    MessageBox.Show("Selected faculty does not exist in the database.");
                }
                st.AverageScore = Convert.ToInt32(txtDTB.Text);
                context.SaveChanges();
                MessageBox.Show("Sua thanh cong");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //gets a collection that contains all the rows
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                //populate the textbox from specific value of the coordinates of column and row.
                txtMSSV.Text = row.Cells[0].Value.ToString();
                txtTen.Text = row.Cells[1].Value.ToString();
                cmbKhoa.Text = row.Cells[2].Value.ToString();
                txtDTB.Text = row.Cells[3].Value.ToString();
            }
        }
    }
}
