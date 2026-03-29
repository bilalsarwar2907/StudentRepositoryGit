using Microsoft.EntityFrameworkCore;
using StudentRepositoryGit.Data;
using StudentRepositoryGit.Models;
using System;
using System.Linq;

namespace StudentRepositoryGit.Repositories
{
    public class StudentRepositoryDb : IStudentsRepository
    {
        private readonly StudentDbContext _context;

        public StudentRepositoryDb(StudentDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Student> Get()
        {
            return _context.Students.AsEnumerable();
        }

        public Student? GetById(int id)
        {
            return _context.Students.Find(id);
        }
        public Student? Add(Student student)
        {

            if (student == null)
            {
                throw new ArgumentNullException(nameof(student)); 
            }
                _context.Add(student);
                _context.SaveChanges();
                 return student;
                  
        }
        public Student? Delete(int id)
        {
            var studentToDelete = GetById(id);
            if (studentToDelete != null)

            {
                _context.Remove(studentToDelete);
                _context.SaveChanges();
                return studentToDelete;
            } 
            return null;
        }
        public Student? Update(int id, Student student)
        {
             var studentToUpdate = GetById(id);
            if (studentToUpdate != null)
            {
                studentToUpdate.Name = student.Name;
                studentToUpdate.BirthYear = student.BirthYear;
                studentToUpdate.Grade = student.Grade;
                _context.SaveChanges();

                return studentToUpdate;
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

            IQueryable<Student> result = _context.Students;

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

            IQueryable<Student> result = _context.Students;

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

