namespace PharmaRnd.API.Response
{
    public record ResponseError(string? ErrorCode, string? ErrorMessage, string? InvalidField);

    public record Envelope
    {
        private Envelope(object? result, IEnumerable<ResponseError> errors)
        {
            Result = result;
            Errors = errors.ToList();
            TimeCreated = DateTime.Now;
        }
        
        public object? Result { get; }
        public List<ResponseError> Errors { get; }
        public DateTime TimeCreated { get; }

        //ok
        public static Envelope Ok(object? result = null) =>
            new Envelope(result, []);

        //no ok
        public static Envelope Error(IEnumerable<ResponseError> errors) =>
            new Envelope(null, errors);
    }
}
