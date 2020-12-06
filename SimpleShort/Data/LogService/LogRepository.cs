﻿/*
 * LogRepository:
 * Responsible for preforming basic CRUD operations on the database except the User can not delete or update logs once pushed to the database.
 * From here, a user can get a specific log, all logs, and create a log.
 */

using Microsoft.EntityFrameworkCore;
using SimpleShort.Entities.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleShort.Data.LogService
{
    public class LogRepository : ILogRepository
    {
        private readonly ShortenedUrlContext _context;

        public LogRepository(ShortenedUrlContext context)
            => _context = context;

        public async Task<LogEntity> GetLog(Guid id)
            => await _context.Logs.Where(log => log.Id == id).FirstOrDefaultAsync();

        public async Task<List<LogEntity>> GetLogs(DateTime occurred)
            => await _context.Logs.Where(log => log.Occurred >= occurred).ToListAsync();

        public async Task<List<LogEntity>> GetLogs(string value)
            => await _context.Logs.Where(log => (
                log.Information.Contains(value)
                || log.Location.Contains(value)
                || log.Request.Contains(value)
            )).ToListAsync();

        public async Task<bool> CreateLog(LogEntity log)
        {
            await _context.Logs.AddAsync(log);
            return await SaveChanges();
        }

        private async Task<bool> SaveChanges()
            => await _context.SaveChangesAsync() > 0;
    }
}