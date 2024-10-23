using System.ComponentModel.DataAnnotations.Schema;

namespace library.Models
{
    public class AddStudentViewModel
    {
        public Guid ID { get; set; }

        [Column("StudentName", TypeName = "varchar(100)")]
        public string Name { get; set; }

        public int StudentRoll { get; set; }

        [Column("StudentGender", TypeName = "varchar(10)")]
        public string Gender { get; set; }
        public int Std { get; set; }

    }
}
