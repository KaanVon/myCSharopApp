using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BS.Data.Infrastructure
{
    public class DbFactory
    {
        public static DbContext GetDbContext()
        {
            DbContext dbContext = CallContext.GetData("DbContext_BS") as DbContext;

            if (dbContext == null)
            {
                dbContext = new ModelEntity();
                CallContext.SetData("DbContext_BS", dbContext);
            }

            return dbContext;
        }

        public static DbContext GetNewDbContext()
        {
            return new ModelEntity();
        }
    }
}
