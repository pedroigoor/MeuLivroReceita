namespace MyRecipeBook.Communication.Resopnses
{
    public class ResponseErrorJson
    {
        public IList<string> Errors {  get; }
      
        public ResponseErrorJson(IList<string> errors) {
            Errors = errors; ;
        }

        public ResponseErrorJson(string errors)
        {
            Errors = [errors];
        }
    }
}
