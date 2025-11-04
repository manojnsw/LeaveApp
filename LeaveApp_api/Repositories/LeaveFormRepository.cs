using LeaveApp_api.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace LeaveApp_api.Repositories
{
    public class LeaveFormRepository : ILeaveFormRepository
    {
        private readonly IConfiguration _config;

        public LeaveFormRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task SubmitLeaveAsync(LeaveForm leave)
        {
            using (var con = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await con.OpenAsync();
                using (var cmd = new SqlCommand("sp_InsertLeaveApplication", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ApplicantId", leave.ApplicantId);
                    cmd.Parameters.AddWithValue("@ManagerId", leave.ManagerId);
                    cmd.Parameters.AddWithValue("@StartDate", leave.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", leave.EndDate);
                    cmd.Parameters.AddWithValue("@ReturnDate", leave.ReturnDate);
                    cmd.Parameters.AddWithValue("@NumberOfDays", leave.NumberOfDays);
                    cmd.Parameters.AddWithValue("@GeneralComments", leave.GeneralComments ?? string.Empty);
                    cmd.Parameters.AddWithValue("@LeaveType", leave.LeaveType);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
