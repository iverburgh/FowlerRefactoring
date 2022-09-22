namespace VideoStore.Console.PersistanceModels
{
    public  class Play : BaseEntity
    {
        public Play()
        {
            Performances = new HashSet<PersistanceModels.Performance>();
        }

        public string Name { get; set; } = null!;
        public int PayType { get; set; }
        public string? ShortName { get; set; }

        public virtual ICollection<PersistanceModels.Performance> Performances { get; set; }
    }
}
