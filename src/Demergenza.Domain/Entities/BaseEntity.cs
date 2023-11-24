using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demergenza.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
    }
}