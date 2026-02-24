namespace MyRecipeBook.Communication.Resopnses
{
    public class ResponseRegisteredUserJson
    {
         public string Name { get; set; } = string.Empty;
         public ResponseTokensJson Tokens { get; set; } = default!;
    }
}
