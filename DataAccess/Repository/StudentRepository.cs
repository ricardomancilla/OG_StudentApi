using Dapper.Contrib.Extensions;
using DataAccess.Contract;
using DataAccess.Model;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private const string ConnectionStringName = "ConnectionString";

        private readonly string connectionString;

        public StudentRepository(IConfiguration config)
        {
            connectionString = config[ConnectionStringName];
        }

        public Task<IEnumerable<Student>> GetAllAsync()
        {
            using var dbConnection = new SqliteConnection(connectionString);
            dbConnection.Open();
            return dbConnection.GetAllAsync<Student>();
        }

        public Task<Student> GetAsync(int Id)
        {
            using var dbConnection = new SqliteConnection(connectionString);
            dbConnection.Open();
            return dbConnection.GetAsync<Student>(Id);
        }

        public Task<int> CreateAsync(Student student)
        {
            using var dbConnection = new SqliteConnection(connectionString);
            dbConnection.Open();
            return dbConnection.InsertAsync(student);
        }

        public Task<bool> UpdateAsync(Student student)
        {
            using var dbConnection = new SqliteConnection(connectionString);
            dbConnection.Open();
            return dbConnection.UpdateAsync(student);
        }

        public Task<bool> DeleteAsync(int Id)
        {
            using var dbConnection = new SqliteConnection(connectionString);
            dbConnection.Open();
            return dbConnection.DeleteAsync(new Student { Id = Id });
        }
    }
}
