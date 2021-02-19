using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irisi_M_301072868.Models
{
    public class EFRepository : IRepository
    {
        private readonly ApplicationDbContext context;

        public EFRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Club> Clubs => context.Clubs;

        public IQueryable<Player> Players => context.Players;
    }
}
