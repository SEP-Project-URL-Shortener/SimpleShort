﻿/*
 * ILogRepository:
 * Responsible for giving the user an interface of the repository instead of giving them the entire repository.
 */

using SimpleShort.Entities.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleShort.Data.LogService
{
    public interface ILogRepository
    {
        Task<LogEntity> GetLog(Guid id);

        Task<List<LogEntity>> GetLogs(DateTime occurred);

        Task<List<LogEntity>> GetLogs(string value);

        Task<bool> CreateLog(LogEntity log);
    }
}