namespace VideoStore.Console.PersistanceModels
{
    public class Performance : BaseEntity
    {
        public Guid PlayId { get; set; }
        public int Audience { get; set; }
        public Guid CustomerId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Play Play { get; set; } = null!;
    }
}
