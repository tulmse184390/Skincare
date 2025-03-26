using DAL.Datas;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implements
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly SkinCareContext dbContext;

        public AppointmentRepository()
        {
            dbContext = new();
        }

        public async Task<Appointment> AddAppointmentAsync(Appointment appointment)
        {
            await dbContext.Appointments.AddAsync(appointment);
            await dbContext.SaveChangesAsync();
            return appointment;
        }

        public async Task<List<Appointment>> GetAppointmentsAsync(int userId)
        {
            return await dbContext.Appointments.Where(x =>x.UserId == userId)
                                               .Include(x => x.AppointmentDetails)
                                               .ThenInclude(t => t.Service)
                                               .ToListAsync();
        }
    }
}
