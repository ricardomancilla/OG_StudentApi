using DataAccess.Contract;
using DataAccess.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public Task<int> CreateAsync(Student student)
        {
            return studentRepository.CreateAsync(student);
        }

        public Task<bool> DeleteAsync(int Id)
        {
            return studentRepository.DeleteAsync(Id);
        }

        public Task<Student> GetAsync(int Id)
        {
            return studentRepository.GetAsync(Id);
        }

        public Task<IEnumerable<Student>> GetAllAsync()
        {
            return studentRepository.GetAllAsync();
        }

        public Task<bool> UpdateAsync(Student student)
        {
            return studentRepository.UpdateAsync(student);
        }
    }
}
