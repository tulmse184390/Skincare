using DAL.Entities;

namespace BLL.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetAppointmentsAsync(int userId, DateTime fromTime, DateTime toTime);
    }
}
