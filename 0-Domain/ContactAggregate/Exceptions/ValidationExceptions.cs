namespace MedGrupo.Domain.ContactAggregate.Exceptions
{
    [System.Serializable]
    public class ValidationException : AggregateException
    {
        public ValidationException() { }
        public ValidationException(string message) : base(message) { }
        public ValidationException(string message, AggregateException inner) : base(message, inner) { }
        protected ValidationException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        public ValidationException(IEnumerable<ContactException> innerExceptions) : base (innerExceptions) { }
    }
}