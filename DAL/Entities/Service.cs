namespace DAL.Entities;

public partial class Service
{
    public int ServiceId { get; set; }

    public string ServiceName { get; set; } = null!;

    public int Duration { get; set; }

    public decimal Price { get; set; }

    public string Status { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<AppointmentDetail> AppointmentDetails { get; set; } = new List<AppointmentDetail>();
}
