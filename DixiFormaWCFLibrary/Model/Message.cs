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
    public class Message
    {
        [BsonId]
        public Guid MessageId { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public DateTime SendingTime { get; set; }

        public override string ToString()
        {
            return $"From {User.Login} at {SendingTime.ToString()}\n\t{Text}";
        }
    }
}
