using Dapper;
using DapperApp.Models;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;
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
        string GetFullDateTime();
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
                IDbConnection db = new SqlConnection(_connectionString);
                db.Open();
                int count = db.QuerySingleOrDefault<int>("SELECT COUNT(Id) FROM People");
                db.Close();
                db.Dispose();
                return count;
            }
        }
        public List<User> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<User>("SELECT * FROM People").ToList();
            }
        }
        public User Get(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.QuerySingleOrDefault<User>("SELECT * FROM People WHERE Id = @id", new { id }) ?? new User { };
            }
        }
        public void Create(User user)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "INSERT INTO People VALUES(@Id, @FullName, @Age, @Email)";
                db.Execute(sqlQuery, user);
            }
        }
        public int MaxId
        {
            get
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var sqlQuery = "SELECT MAX(Id) + 1 FROM People";
                    return db.QuerySingleOrDefault<int>(sqlQuery);
                }
            }
        }
        public void Update(User user)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "UPDATE People SET FullName = @FullName, Age = @Age, Email = @Email WHERE Id = @Id";
                db.Execute(sqlQuery, user);
            }
        }
        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM People WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
        public string GetFullDateTime()
        {
            //Первый CommandType.Text
            // using (IDbConnection db = new SqlConnection(_connectionString))
            // {
            //     var sqlQuery = "EXEC ToFullDateTime @dateTime";
            //     FullTime result = db.QuerySingleOrDefault<FullTime>(sqlQuery, new { dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
            //     return $"Год - {result.Year}, Четверть - {result.Quarter}, Месяц название - {result.MonthName}, День года - {result.DayOfYear}, День - {result.Day}, День недели - {result.DayOfTheWeek}, Час - {result.Hour}, Минута - {result.Minute}, Секунд - {result.Second}"; 
            // }

            //Второй CommandType.StoredProcedure
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var prodsedure = "[ToFullDateTime]";
                var values = new { dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
                FullTime result = db.QuerySingleOrDefault<FullTime>(prodsedure, values,commandType: CommandType.StoredProcedure);
                return $"Год - {result.Year}, Четверть - {result.Quarter}, Месяц название - {result.MonthName}, День года - {result.DayOfYear}, День - {result.Day}, День недели - {result.DayOfTheWeek}, Час - {result.Hour}, Минута - {result.Minute}, Секунд - {result.Second}"; 
            }
        }
    }
}