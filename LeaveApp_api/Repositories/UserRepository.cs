using LeaveApp_api.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace LeaveApp_api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _config;

        public UserRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<User>> GetActiveUsersAsync()
        {
            var users = new List<User>();
            using (var con = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await con.OpenAsync();
                using (var cmd = new SqlCommand("sp_GetActiveUsers", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            users.Add(new User
                            {
                                UserId = (int)reader["UserId"],
                                FullName = reader["FullName"].ToString(),
                                IsActive = (bool)reader["IsActive"]
                            });
                        }
                    }
                }
            }
            return users;
        }
    }
}
