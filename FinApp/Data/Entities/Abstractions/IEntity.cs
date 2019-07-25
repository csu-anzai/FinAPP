using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities.Abstractions
{
    public interface IEntity
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
