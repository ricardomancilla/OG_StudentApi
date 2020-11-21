using System.ComponentModel.DataAnnotations;

namespace Student.API.DTO
{
    public class StudentDto
    {
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
