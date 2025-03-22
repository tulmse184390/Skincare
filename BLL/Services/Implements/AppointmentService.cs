using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Implements;
using DAL.Repositories.Interfaces;

namespace BLL.Services.Implements
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository appointmentRepository;

        public AppointmentService()
        {
            appointmentRepository = new AppointmentRepository();
        }

        public async Task<List<Appointment>> GetAppointmentsAsync(int userId, DateTime fromTime, DateTime toTime)
        {
            return await appointmentRepository.GetAppointmentsAsync(userId, fromTime, toTime);
        }
    }
}
