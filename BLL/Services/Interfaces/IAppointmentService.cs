using DAL.Entities;

namespace BLL.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetAppointmentsAsync(int userId);
        Task<bool> IsValidBookingAsync(int userId, DateTime dateTime);
        Task<Appointment> AddAppointmentAsync(Appointment appointment);
        Task<List<Appointment>> GetAppointmentsAsync(int userId, string status);
    }
}
