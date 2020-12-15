/*
 * LogRepository:
 * Responsible for preforming basic CRUD operations on the database except the User can not delete or update logs once pushed to the database.
 * From here, a user can get a specific log, all logs, and create a log.
 */

// Included Libraries
using Microsoft.EntityFrameworkCore;
using SimpleShort.Entities.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleShort.Interfaces;

namespace SimpleShort.Data.LogService
{
    public class LogRepository : ILogRepository
    {
        // Context to talk to the database
        private readonly ShortenedUrlContext _context;

        // Default constructor gets context via dependency injection
        public LogRepository(ShortenedUrlContext context)
            => _context = context;
        
        // Get a single log based on id
        public async Task<LogEntity> GetLog(Guid id)
            => await _context.Logs.Where(log => log.Id == id).FirstOrDefaultAsync();

        // Get logs starting from a date
        public async Task<List<LogEntity>> GetLogs(DateTime occurred)
            => await _context.Logs.Where(log => log.Occurred >= occurred).ToListAsync();

        // Get all longs containing a given value
        public async Task<List<LogEntity>> GetLogs(string value)
            => await _context.Logs.Where(log => (
                log.Information.Contains(value)
                || log.Location.Contains(value)
                || log.Request.Contains(value)
            )).ToListAsync();

        // Create a log from a LogEntity object
        public async Task<bool> CreateLog(LogEntity log)
        {
            await _context.Logs.AddAsync(log);
            return await SaveChanges();
        }

        // Save changes in the database
        private async Task<bool> SaveChanges()
            => await _context.SaveChangesAsync() > 0;
    }
}