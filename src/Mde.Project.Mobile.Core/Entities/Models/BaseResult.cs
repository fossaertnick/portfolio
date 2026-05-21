using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Mde.Project.Mobile.Core.Entities.Models
{
    public class BaseResult
    {
        // properties
        public bool IsSucces => !Errors.Any();
        public List<string> Errors { get; set; } = new List<string>();

        public string? UserMessage { get; set; }
        public int? StatusCode { get; set; }
    }
}
