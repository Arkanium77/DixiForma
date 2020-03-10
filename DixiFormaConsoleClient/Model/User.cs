using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        [BsonId]
        public String Login{ get; set; }
        public String Password{ get; set; }

        public override string ToString()
        {
            return $"User «{Login}»";
        }
    }
}
