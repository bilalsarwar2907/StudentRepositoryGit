using StudentRepositoryGit.Models;
namespace StudentRepositoryGit.Repositories
{
    public class StudentsRepository
    {
        private readonly List<Student> _students = new List<Student>();

        private int _nextId = 1;

        public StudentsRepository()
        {

        }
        public IEnumerable<Student> Get()

        {
            return _students;

        }
        public Student? GetById(int id)
        {
            return _students.FirstOrDefault(student => student.Id == id);
        }
        public Student? Add(Student student)
        {
            if (student == null)

                return null;
            student.Id = _nextId++;
            _students.Add(student);
            return student;

        }
        public Student? Update(int id, Student data)
        {
           var student = GetById(id);
            if (student == null)
                return null;
            student.Name = data.Name;
            student.BirthYear = data.BirthYear;

            return student;

        }


        public Student? Delete(int id)
        {
           var student = GetById(id);
            if (student == null)
                return null;
            _students.Remove(student);
            return student;
        }
    }
}
