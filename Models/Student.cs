namespace Orient.Models
{
    public class Student
    {
        public Student() {
            TotalPoints= 0;
            Quiz1Tries = 0;
            
        }
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Quiz1Tries { get; set; }
        public int TotalPoints { get; set; }

    }
}
