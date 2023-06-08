namespace OZ.OrderApi.Services.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        private readonly string _message;

        public EntityNotFoundException(string message)
        {
            _message = message;
        }

        public override string Message => _message;
    }
}
