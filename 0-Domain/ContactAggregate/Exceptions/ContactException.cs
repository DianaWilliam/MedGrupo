namespace MedGrupo.Domain.ContactAggregate.Exceptions
{
    [System.Serializable]
    public class ContactException : System.Exception
    {
        public ContactException() { }
        public ContactException(string message) : base(message) { }
        public ContactException(string message, System.Exception inner) : base(message, inner) { }
        protected ContactException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}