using iocPubApi.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace iocPubApi.Repositories
{
    public class CoreSmsRepository: ICoreSmsRepository
    {
        private readonly io_onlineContext db;

        public CoreSmsRepository(io_onlineContext context)
        {
            db = context;
        }

        public void Add(CoreSms sms)
        {
            db.CoreSms.Add(sms);
            db.SaveChanges();
            
        }
    }
}