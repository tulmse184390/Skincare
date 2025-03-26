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

        public async Task<Appointment> AddAppointmentAsync(Appointment appointment)
        {
            return await appointmentRepository.AddAppointmentAsync(appointment);
        }

        public async Task<List<Appointment>> GetAppointmentsAsync(int userId)
        {
            return await appointmentRepository.GetAppointmentsAsync(userId);
        }

        public async Task<List<Appointment>> GetAppointmentsAsync(int userId, string status)
        {
            var list = await appointmentRepository.GetAppointmentsAsync(userId);
            return list.Where(x => x.Status == status).ToList();
        }

        public async Task<bool> IsValidBookingAsync(int userId, DateTime dateTime)
        {
            var list = await appointmentRepository.GetAppointmentsAsync(userId);

            var appointment = list.FirstOrDefault(x => x.StartTime <= dateTime && x.EndTime > dateTime);
            return appointment == null;
        }
    }
}
