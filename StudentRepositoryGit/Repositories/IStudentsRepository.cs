using StudentRepositoryGit.Models;

namespace StudentRepositoryGit.Repositories
{
    public interface IStudentsRepository
    {
        Student? Add(Student student);
        Student? Delete(int id);
        IEnumerable<Student> Get();
        IEnumerable<Student> Get(int? birthYearBefore, int? birthYearAfter, string? nameFilter);
        IEnumerable<Student> Get(int? birthYearBefore, int? birthYearAfter, string? nameFilter, string? sortBy, bool descending = false);
        Student? GetById(int id);
        Student? Update(int id, Student data);
    }
}