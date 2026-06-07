using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Entities.Models;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Platforms.Windows
{
    public class WindowsMapService : IMapService
    {
        public event Action<Guid> OnPinClicked;

        public Task<ResultModel<bool>> ClearPins()
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<bool>> Initialize(object mapControl)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<bool>> MoveTo(Location location)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<bool>> SetPins(IEnumerable<MemoriaList> items)
        {
            throw new NotImplementedException();
        }
    }
}
