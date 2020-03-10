using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DixiFormaWCFLibrary.Model
{
    public class SessionDF
    {
        private readonly User sessionUser;
        private readonly OperationContext sessionContext;
        public SessionDF(User user,OperationContext context)
        {
            sessionUser = user;
            sessionContext = context;
        }
        public User SessionUser { get; }
        public OperationContext SessionContext { get; }
    }
}
