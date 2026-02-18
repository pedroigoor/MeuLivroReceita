namespace MyRecipeBook.Excpitons.ExceptionsBase
{
    public class ErrorOnValidationException : MyRecipeBookException
    {
        public List<string> Errors { get; }
        public ErrorOnValidationException(List<string> errors)
        {
            Errors = errors;
        }
    }
}
