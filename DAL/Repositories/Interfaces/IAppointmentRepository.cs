using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAppointmentsAsync(int userId,DateTime fromTime, DateTime toTime);
    }
}
