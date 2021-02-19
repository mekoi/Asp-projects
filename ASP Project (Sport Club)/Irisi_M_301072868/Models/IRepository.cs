using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irisi_M_301072868.Models
{
    public interface IRepository
    {
        IQueryable<Club> Clubs { get;}

        IQueryable<Player> Players { get; }
    }
}
