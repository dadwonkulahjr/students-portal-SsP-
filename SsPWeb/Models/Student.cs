namespace SsPWeb.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Record Record { get; set; }

    }
}
