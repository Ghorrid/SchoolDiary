using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace SchoolDiary
{
    public partial class AddEditStudent : Form
    {

        private int _studentId;
        private Student _student;
        //private List<int> group = new List<int> { 0, 1, 2, 3 };

        private FileHelper<List<Student>> _fileHelper =
            new FileHelper<List<Student>>(Program.FilePath);
        public AddEditStudent(int id =0)
        {
            InitializeComponent();
            _studentId = id;
            GetStudentData();
            tbFirstName.Select();
            cbGroupName.DataSource =  Form1.group;
        }
      
         private void GetStudentData()
        {
            if (_studentId != 0)
            {
                Text = "Edycja danych ucznia";
                var students = _fileHelper.DeserializeFromFile();
                _student = students.FirstOrDefault(x => x.Id == _studentId);
                if (_student == null)
                    throw new Exception("Brak studenta o takim Id");
                FillTextBoxes();
            }
        }

        private void FillTextBoxes() 
        {
            tbId.Text = _student.Id.ToString();
            tbFirstName.Text = _student.FirstName;
            tbLastName.Text = _student.LastName;
            tbMath.Text = _student.Math;
            tbPhysics.Text = _student.Physics;
            tbPolishLang.Text = _student.PolishLang;
            tbTechnology.Text = _student.Technology;
            tbForeignLang.Text = _student.ForeignLang;
            tbPhysics.Text = _student.Physics;
            rtbComments.Text = _student.Comments;
            cbAdditionalActivities.Checked= _student.AdditionalActivities;
            cbGroupName.Text = _student.Group;
        }

        private void btnAddEditAccept_Click(object sender, EventArgs e)
        {
            var students = _fileHelper.DeserializeFromFile();

            if (_studentId != 0)           
                students.RemoveAll(x => x.Id == _studentId);
            else
            AssignIdToNewStudent(students);

            AddNewUserToList(students);
         
            _fileHelper.SerializeToFile(students);
            Close();

        }

        private void AddNewUserToList(List<Student> students) 
        {
            var student = new Student
            {
                Id = _studentId,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                Comments = rtbComments.Text,
                Math = tbMath.Text,
                Technology = tbTechnology.Text,
                PolishLang = tbPolishLang.Text,
                ForeignLang = tbForeignLang.Text,
                Physics = tbPhysics.Text,
                AdditionalActivities = cbAdditionalActivities.Checked,
                Group =cbGroupName.Text
            };
            students.Add(student);
        }

        private void AssignIdToNewStudent(List<Student> students)
        {
            var studentMaxId = students.OrderByDescending(x => x.Id).FirstOrDefault();
            _studentId = studentMaxId != null ? studentMaxId.Id + 1 : 1;
            //if (studentMaxId != null)
            //{
            //    studentId = studentMaxId.Id + 1;
            //} else studentId = 1;
            // to samo co wyzej w innej formie
        }

        private void btnAddEditCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
