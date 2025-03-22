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

        public async Task<List<Appointment>> GetAppointmentsAsync(int userId, DateTime startTime, DateTime toTime)
        {
            return await dbContext.Appointments.Where(x =>x.UserId == userId && x.StartTime >= startTime && x.StartTime <= toTime)
                                               .Include(x => x.AppointmentDetails)
                                               .ToListAsync();
        }
    }
}
