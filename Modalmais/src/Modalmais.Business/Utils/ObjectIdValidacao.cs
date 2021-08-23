using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modalmais.Business.Utils
{
    public class ObjectIdValidacao
    {
        public static bool Validar(string id)
        {
            return ObjectId.TryParse(id, out ObjectId resposta);
        }
    }
}
