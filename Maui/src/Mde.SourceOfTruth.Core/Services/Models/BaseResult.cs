using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Mde.SourceOfTruth.Core.Services.Models
{
    public class BaseResult
    {
        public bool IsSucces => !Errors.Any();
        public List<string> Errors { get; set; } = new List<string>();
    }
}
