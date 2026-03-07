using System.Net;

namespace MyRecipeBook.Excpitons.ExceptionsBase
{
    public class NotFoundException : MyRecipeBookException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
    }
}
