using Mde.SourceOfTruth.Core.Entities;
using Mde.SourceOfTruth.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.SourceOfTruth.Core.Services.Interfaces
{
    public interface IMemoriaService
    {
        // methoden
        Task<ResultModel<Memoria>> GetMemoriaByIdAsync(Guid id, Guid deviceId);
        Task<ResultModel<IEnumerable<Memoria>>> GetAllMemoriasAsync(Guid deviceId);
        Task<ResultModel<Memoria>> CreateMemoriaAsync(Memoria newMemoria);
        Task<ResultModel<Memoria>> UpdateMemoriaAsync(Memoria updatingMemoria, Guid deviceId);
        Task<ResultModel<Memoria>> DeleteMemoriaAsync(Guid id, Guid deviceId);
    }
}
