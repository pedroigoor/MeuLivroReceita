namespace MyRecipeBook.Excpitons.ExceptionsBase
{
    public class NotFoundException : MyRecipeBookException
    {
        public NotFoundException(string mensage) : base(mensage) { }   
    
    }
}
