namespace PharmaRnd.Domain.Shared
{
    public abstract class MyEntity<TId> where TId : notnull
    {
        protected MyEntity(TId id)
        {
            Id = id;
        }

        public TId Id { get; private set; }
    }
}
