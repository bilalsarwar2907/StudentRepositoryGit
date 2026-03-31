using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StudentRepositoryGit.Models;
using System;
namespace StudentRepositoryGit.Data
{
    public class StudentDbContext : DbContext
    {
     
            public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) 
        { }

            public DbSet<Student> Students { get; set; }
        
    }
}
