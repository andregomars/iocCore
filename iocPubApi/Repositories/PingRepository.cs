using System;
using System.IO;
using System.Collections.Generic;
using iocPubApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace iocPubApi.Repositories
{
    public class PingRepository : IPingRepository
    {
        private readonly io_onlineContext db;
        public IConfigurationRoot Configuration { get; }

        public PingRepository(io_onlineContext context)
        {
            db = context;
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
        }

        public string PingDB()
        {
            var fleetCount = db.IoFleet.Count();
            return $"Fleet count is {fleetCount}"; 
        }

        // #region IDisposable Support
        // private bool disposedValue = false; 

        // protected virtual void Dispose(bool disposing)
        // {
        //     if (!disposedValue)
        //     {
        //         if (disposing)
        //         {
        //             db.Dispose();
        //         }
        //         disposedValue = true;
        //     }
        // }

        // public void Dispose()
        // {
        //     Dispose(true);
        //     GC.SuppressFinalize(this);
        // }
        // #endregion

    }
}