using Microsoft.EntityFrameworkCore;
using StudentRepositoryGit.Models;
namespace StudentRepositoryGit.Data
{
    public class StudentDbContext : DbContext
    {
        private readonly StudentDbContext _context;

        public StudentDbContext(StudentDbContext context)
        {
            _context = context;
        }

        public DbSet<Student> Students { get; set; }
    }
}
