namespace MyFinance.Domain.Common
{
    public abstract class BaseEntidade
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
    }
}