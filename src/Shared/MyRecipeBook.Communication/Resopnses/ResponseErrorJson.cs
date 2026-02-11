using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Communication.Resopnses
{
    public class ResponseErrorJson
    {
        public IList<string> Erros {  get; }
      
        public ResponseErrorJson(IList<string> erros) {
            Erros = erros; ;
        }

        public ResponseErrorJson(string erros)
        {
            Erros = [erros];
        }
    }
}
