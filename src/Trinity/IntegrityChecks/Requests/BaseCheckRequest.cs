namespace Trinity.IntegrityChecks
{
    public interface IBaseCheckRequest
    {
        string Name { get; }

        string Description { get; set; }
    }


    public interface IRawCheckRequest : IBaseCheckRequest
    {
        string Cypher { get; set; }
    }

    public abstract class BaseCheckRequest
    {
        public virtual string Name => GetType().Name;

        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}