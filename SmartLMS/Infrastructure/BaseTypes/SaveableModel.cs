using SmartLMS.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartLMS.Infrastructure.BaseTypes
{
    public abstract class SaveableModel
    {
        [NotMapped]
        public string SaveMessage { get; set; }
        [NotMapped]
        public AlertType MessageType { get; set; }
    }
}