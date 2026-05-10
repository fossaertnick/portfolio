using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Core.Entities
{
    public class Synced
    {
        // properties
        public DateTime SynchedOn { get; set; }
        public bool IsSynced { get; set; }
        public bool IsDeleted { get; set; }
    }
}
