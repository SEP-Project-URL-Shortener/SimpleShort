﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleShort.Entities.Logging;

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
