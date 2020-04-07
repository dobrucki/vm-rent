namespace Core.Application.SharedKernel
{
    public class Error
    {
       public string Name { get; }
       public string Description { get; }

       protected Error(string name, string description)
       {
           Name = name;
           Description = description;
       }
    }

    public class ValidationError : Error
    {
        public ValidationError(string name, string description) : base(name, description)
        {
        }
    }

    public class PersistenceError : Error
    {
        public PersistenceError(string name, string description) : base(name, description)
        {
        }
    }
}