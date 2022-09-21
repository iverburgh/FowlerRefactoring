namespace VideoStore.Console
{
    public class Play
    {
        public string Name { get; }
        public PayType PayType { get; }

        public Play(string name, PayType payType)
        {
            Name = name;
            PayType = payType;
        }
    }
}
