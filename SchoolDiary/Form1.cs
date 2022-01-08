using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SchoolDiary
{
    public partial class Form1 : Form
    {

        private FileHelper<List<Student>>  _fileHelper = 
            new FileHelper<List<Student>>(Program.FilePath);

        public static List<string> group = new List<string> { "Wszyscy", "Grupa A", "Grupa B", "Grupa C" };

        public Form1()
        {
            InitializeComponent();

            #region LEKCJA 8: Praca z Plikami System.IO
            // LEKCJA 8: Praca z Plikami System.IO
            //var path = $@"{Path.GetDirectoryName(Application.ExecutablePath)}\..\new_file.txt";
            //if (!File.Exists(path))
            //{
            //    File.Create(path); 
            //}
            // usuwanie pliku
            //File.Delete(path);
            //writealltext zawsze nadpisuje caly plik 
            //tworzy plik jezeli go nie ma
            //File.WriteAllText(path, "Przykładowy tekst");
            //AppendAllText tworzy plik jezeli go nie ma
            //File.AppendAllText(path, "Przykładowy tekst \n");
            //var text = File.ReadAllText(path);
            //mbox
            //MessageBox.Show(text);
            //MessageBox.Show("Test","Tytuł",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Stop);
            #endregion File

            #region LEKCJA 9: Serializacja, Deserializacja
            //var serializer = new XmlSerializer (typeof(List<Student>));            
            //var streamWriter = new StreamWriter (_filePath);

            //serializer.Serialize(streamWriter, students);
            ////zamykamy
            //streamWriter.Close();
            //// zwalniamy pamiec
            //streamWriter.Dispose();


            // rozwiazanie z try , finaly
            //var serializer = new XmlSerializer(typeof(List<Student>));
            //StreamWriter streamWriter = null;

            //try
            //{
            //    streamWriter = new StreamWriter(_filePath);
            //    serializer.Serialize(streamWriter, students);
            //    //zamykamy
            //    streamWriter.Close();
            //}
            //finally 
            //{
            //    streamWriter.Dispose();
            //}

            //sprawdzenie czy dziala kod 

            //var students = new List<Student>();
            //students.Add(new Student { Id = 1, FirstName = "Adam" });
            //students.Add(new Student { Id = 2, FirstName = "Jan" });
            //students.Add(new Student { Id = 3, FirstName = "Dawid" });
            //SerializeToFile(students);

            //var students = DeserializeFromFile();
            //foreach (var item in students)
            //{
            //    MessageBox.Show(item.FirstName);
            //}
            //rozwiazanie z using lepsze !!! jezeli w using jest deklaracja obiektu to na koncu  
            // wywolywana jest metoda dispose automatycznie.
            //var serializer = new XmlSerializer(typeof(List<Student>));

            //using (var streamWriter = new StreamWriter(_filePath))
            //{
            //    serializer.Serialize(streamWriter, students);
            //    streamWriter.Close();
            //}
            #endregion

            #region LEKCJA 12: LINQ

            //List<int> list1 = new List<int> { 2,5,8,20,22,105};
            //// skladnia query syntax Linq - skladnia zapytan podobna do sqla
            //var list2 = (from x in list1  where x>10 select x).ToList();

            //// skladnia lambda expression - skladnia metody method syntax 

            //var list3 = list1.Where(x => x > 10).ToList();

            //var list4 = (from x in list1 where x > 10 orderby x descending select x).ToList();

            //var list5 = list1.Where(x =>x>10).OrderByDescending(x => x).ToList();

            //var studentsList = new List<Student>();

            //// pobieramy samo pole id
            //var student1 = from x in studentsList select x.Id; 
            //var student2 = studentsList.Select(x => x.Id);

            //var allPosivites = list1.All(x => x > 0); // zwraca true jezeli wszytkie elementy listy sa dodanie wszystkie spelniaja warunek

            //var anyNumberBiggerThen100 = list1.Any(x => x > 100); //zwraca true jezeli jakis jeden dowolny element listy spelnia warunek >100

            //var contains10 = list1.Contains(10);
            //var max = list1.Max();
            //var min = list1.Min();
            //var avg = list1.Average();
            //var sum = list1.Sum();
            //var count = list1.Count();

            //var firstElement = list1.First(x => x>10); // 1 element wiekszy od 10

            #endregion

            cbGroupFilter.DataSource = group;

            RefreshDiary();

           // SetColumnsHeader();

            

        }


        
            //if (cbGroupFilter.Text != null)
            //{
            //    dgvStudents.DataSource = students.Any(x => x.groupId == cbGroupFilter.Text);
            //}

    private void RefreshDiary()
        {
            var students =  _fileHelper.DeserializeFromFile();

        //    var filtredStudents = (from x in students where x.groupId =="1" select x).ToList();

            if (cbGroupFilter.Text == "Wszyscy")
                dgvStudents.DataSource = students;
            else
                dgvStudents.DataSource = students.Where(x=>x.Group==cbGroupFilter.Text).ToList();
        }

        private void SetColumnsHeader()
        {
            dgvStudents.Columns[0].HeaderText = "Numer";
            dgvStudents.Columns[1].HeaderText = "Imię";
            dgvStudents.Columns[2].HeaderText = "Nazwisko";
            dgvStudents.Columns[3].HeaderText = "Matematyka";
            dgvStudents.Columns[4].HeaderText = "Technologia";
            dgvStudents.Columns[5].HeaderText = "Fizyka";
            dgvStudents.Columns[6].HeaderText = "Język Polski";
            dgvStudents.Columns[7].HeaderText = "Język obcy";
            dgvStudents.Columns[8].HeaderText = "Komenatrz";
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addEditStudent = new AddEditStudent();
            addEditStudent.FormClosing += AddEditStudent_FormClosing;
            addEditStudent.ShowDialog();
            
        }

        private void AddEditStudent_FormClosing(object sender, FormClosingEventArgs e)
        {
            RefreshDiary();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count ==0)
            {
                MessageBox.Show("Proszę wybierz pozycję, którą chcesz edytować");
                return;
            }
            var addEditStudent = new AddEditStudent( Convert.ToInt32(dgvStudents.SelectedRows[0].Cells[0].Value));
            
            addEditStudent.FormClosing += AddEditStudent_FormClosing;
            
            addEditStudent.ShowDialog();
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Proszę wybierz pozycję, którą chcesz usunąć");
                return;
            }
            var selectedStudent =dgvStudents.SelectedRows[0];

            // _studentid = studentMaxId != null ? studentMaxId.Id + 1 : 1;

            var studentLastName =
                 selectedStudent.Cells[2].Value != null ? selectedStudent.Cells[2].Value.ToString().Trim() : " ";

            var confirmDelete = MessageBox.Show($"Czy na pewno chcesz usunąć ucznia" +
                $" {selectedStudent.Cells[1].Value}" + $"{ studentLastName}" ,"Usuń ucznia",
                MessageBoxButtons.YesNo,MessageBoxIcon.Question);  

            if (confirmDelete == DialogResult.Yes)
            {
                DeleteStudent(Convert.ToInt32(selectedStudent.Cells[0].Value));
                RefreshDiary();
            }    

        }

        private void DeleteStudent(int id)
        {
            var students = _fileHelper.DeserializeFromFile();
            students.RemoveAll(x => x.Id == id);
            _fileHelper.SerializeToFile(students);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDiary();
        }
    }
}
