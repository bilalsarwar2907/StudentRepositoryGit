using Microsoft.EntityFrameworkCore;
using StudentRepositoryGit.Models;
using System.Globalization;
namespace StudentRepositoryGit.Repositories
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly List<Student> _students = new List<Student>();

        private int _nextId = 1;

        public StudentsRepository(bool includeData = false)
        {
            if (includeData)
            {
                Add(new Student { Name = "Alice", BirthYear = 2000,Grade="A" });
                Add(new Student { Name = "Bob", BirthYear = 1995, Grade = "B" });
                Add(new Student { Name = "Charlie", BirthYear = 2002, Grade ="C" });
                Add(new Student { Name = "Diana", BirthYear = 1998, Grade = "D" });
            }


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
            {
                throw new ArgumentNullException(nameof(student));
            }

            student.Id = _nextId++;
            _students.Add(student);
            return student;

        }
        public Student? Update(int id, Student data)
        {
            var student = GetById(id);
            if (student != null)
            {
                student.Name = data.Name;
                student.BirthYear = data.BirthYear;
                student.Grade = data.Grade;

                return student;
            }
            return null;

        }

        public Student? Delete(int id)
        {
            var student = GetById(id);
            if (student != null)
            {
                _students.Remove(student);
                return student;
            }
            return null;
        }

        public IEnumerable<Student> Get(int? birthYearBefore, int? birthYearAfter, string? nameFilter)

        {

            if (birthYearBefore > birthYearAfter && birthYearBefore != null && birthYearAfter != null)
            {
                throw new ArgumentException("birthYearBefore " +
                  "cannot be greater than birthYearAfter.");
            }

            IEnumerable<Student> result = _students.AsReadOnly();

            if (birthYearBefore != null)
            {

                result = result.Where(s => s.BirthYear > birthYearBefore);

            }

            if (birthYearAfter != null)
            {

                result = result.Where(s => s.BirthYear < birthYearAfter);

            }
            if (nameFilter != null)
            {
                result = result.Where(c => c.Name.
                 Contains(nameFilter, StringComparison.OrdinalIgnoreCase));
            }
            return result;

        }

       public IEnumerable<Student> Get(int? birthYearBefore, int? birthYearAfter, string? nameFilter, string? sortBy, bool descending = false)
        {
            if (birthYearBefore > birthYearAfter && birthYearBefore != null && birthYearAfter != null)
            {
                throw new ArgumentException("birthYearBefore " +
                  "cannot be greater than birthYearAfter.");
            }

            IEnumerable<Student> result = _students.AsReadOnly();

            if (birthYearBefore != null)
            {

                result = result.Where(s => s.BirthYear >= birthYearBefore);

            }

            if (birthYearAfter != null)
            {

                result = result.Where(s => s.BirthYear <= birthYearAfter);

            }
            if (nameFilter != null)
            {
                result = result.Where(c => c.Name.
                 Contains(nameFilter, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {

                sortBy = sortBy.ToLower();

                if (sortBy == "id")
                {
                    result = descending
                        ? result.OrderByDescending(s => s.Id)
                        : result.OrderBy(s => s.Id);
                }
                if (sortBy == "name")
                {

                    result = descending
                             ? result.OrderByDescending(s => s.Name)
                             : result.OrderBy(s => s.Name);

                }
                if (sortBy == "birthyear")
                {
                    result = descending
                   ? result.OrderByDescending(s => s.BirthYear)
                     : result.OrderBy(s => s.BirthYear);
                }
                if (sortBy == "grade")
                {
                    result = descending
                   ? result.OrderByDescending(s => s.Grade)
                     : result.OrderBy(s => s.Grade);
                }

            }
            return result;
        }

    }
}
