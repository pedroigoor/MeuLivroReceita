namespace MyRecipeBook.Excpitons.ExceptionsBase
{
    public class InvalidLoginException : MyRecipeBookException
    {
            public InvalidLoginException() : base(ResourceMenssagesException.EMAIL_OR_PASSWORD_INVALID) { }         
        

    }
}
