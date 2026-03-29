namespace StudentRepositoryGit.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }= string.Empty;
        public int BirthYear { get; set; }

        public override string ToString()
        {
            return $"Student(Id={Id}, Name={Name}, BirthYear={BirthYear})";
        }
    }
}
