namespace VideoStore.Console.PersistanceModels
{
    public class Customer : BaseEntity
    {
        public Customer()
        {
            Performances = new HashSet<Performance>();
        }

        public string Name { get; set; } = null!;

        public virtual ICollection<Performance> Performances { get; set; }
    }
}
