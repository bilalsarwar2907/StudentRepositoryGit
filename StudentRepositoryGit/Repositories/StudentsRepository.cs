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
      
    }
}
