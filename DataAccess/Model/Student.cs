using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Model
{
    [Table("Student")]
    public class Student
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int Age { get; set; }

        [Required]
        public string Career { get; set; }
    }
}
