using LeaveApp_api.Models;
using System.Threading.Tasks;

namespace LeaveApp_api.Repositories
{
    public interface ILeaveFormRepository
    {
        Task SubmitLeaveAsync(LeaveForm leave);
    }
}
