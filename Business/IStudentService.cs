﻿using DataAccess.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetAsync(int Id);
        Task<int> CreateAsync(Student student);
        Task<bool> UpdateAsync(Student student);
        Task<bool> DeleteAsync(int Id);
    }
}
