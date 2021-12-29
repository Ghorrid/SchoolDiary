using System.IO;
using System.Xml.Serialization;

namespace SchoolDiary
{
    public class FileHelper <T> where T : new() //klasa generyczna T, nie jest ograniczona tylko do List<student>.
    {
        private string _filePath;
        public FileHelper(string filePath)
        {
            _filePath = filePath;
        }
        public void SerializeToFile(T students)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var streamWriter = new StreamWriter(_filePath))
            {
                serializer.Serialize(streamWriter, students);
                streamWriter.Close();
            }


        }

        public T DeserializeFromFile()
        {
            if (!File.Exists(_filePath))
                return new T();

            var serializer = new XmlSerializer(typeof(T));
            using (var streamReader = new StreamReader(_filePath))
            {
                var students = (T)serializer.Deserialize(streamReader);
                streamReader.Close();
                return students;
            }

        }
    }
}
