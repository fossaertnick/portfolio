using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Core.Services.Interfaces
{
    public interface ILocalMemoriaCache
    {
        // methoden
        Task<ResultModel<Memoria>> GetByIdAsync(Guid id);
        Task<ResultModel<bool>> SaveAsync(Memoria memoria);
        Task<ResultModel<bool>> DeleteAsync(Guid id);
        Task<string> CacheFileAsync(byte[] bytes, string extension);
    }
}
