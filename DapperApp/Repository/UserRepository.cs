using Dapper;
using DapperApp.Models;
using Microsoft.Data.Sqlite;
using System.Data;

namespace DapperApp.Repository
{
    public interface IUserRepository
    {
        int MaxId { get; }
        void Create(User user);
        void Delete(int id);
        User Get(int id);
        List<User> GetAll();
        void Update(User user);
        int Count { get; }
    }
    public class UserRepository : IUserRepository
    {
        private string? _connectionString;
        public UserRepository(string? connectionString)
        {
            _connectionString = connectionString;
        }

        public int Count
        {
            get
            {
                IDbConnection db = new SqliteConnection(_connectionString);
                db.Open();
                int count = db.QuerySingleOrDefault<int>("SELECT COUNT(Id) FROM People");
                db.Close();
                db.Dispose();
                return count;
            }
        }
        public List<User> GetAll()
        {
            using (IDbConnection db = new SqliteConnection(_connectionString))
            {
                return db.Query<User>("SELECT * FROM People").ToList();
            }
        }
        public User Get(int id)
        {
            using (IDbConnection db = new SqliteConnection(_connectionString))
            {
                return db.Query<User>("SELECT * FROM People WHERE Id = @id", new { id }).FirstOrDefault() ?? new User { };
            }
        }
        public void Create(User user)
        {
            using (IDbConnection db = new SqliteConnection(_connectionString))
            {
                var sqlQuery = "INSERT INTO People VALUES(@Id, @FullName, @Age, @Email)";
                db.Execute(sqlQuery, user);
            }
        }
        public int MaxId
        {
            get
            {
                using (IDbConnection db = new SqliteConnection(_connectionString))
                {
                    var sqlQuery = "SELECT MAX(Id) + 1 FROM People";
                    return db.QuerySingleOrDefault<int>(sqlQuery);
                }
            }
        }
        public void Update(User user)
        {
            using (IDbConnection db = new SqliteConnection(_connectionString))
            {
                var sqlQuery = "UPDATE People SET FullName = @FullName, Age = @Age, Email = @Email WHERE Id = @Id";
                db.Execute(sqlQuery, user);
            }
        }
        public void Delete(int id)
        {
            using (IDbConnection db = new SqliteConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM People WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}