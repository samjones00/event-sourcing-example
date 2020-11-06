namespace EventSourcing.Core.Interfaces
{
    public interface IPaymentHandler
    {
        public bool Process();
        public bool IsMatch(string providerName);
    }
}
