using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizAPI.Models
{
    public class Question
    {
        //Marks QId as the Primary Key for the table.
        [Key]
        public int QId { get; set; }

        //Maps QInWords property to a database column of type nvarchar(250
        [Column(TypeName = "nvarchar(250)")]
        public string QInWords { get; set; }

        //auto-implemented property with getter and setter accessors.
        [Column(TypeName = "nvarchar(50)")]
        public string? ImageName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Option1 { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Option2 { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Option3 { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Option4 { get; set; }

        public int Answer { get; set; }
    }
}
