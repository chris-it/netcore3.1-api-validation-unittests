using Bunnings.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bunnings.Domain.Services
{
    public class RosterService : IRosterService
    {
        private List<Roster> allRosters = new List<Roster>() {
            new Roster(){ Id = 1, Name = "Roster 1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7) },
            new Roster(){ Id = 2, Name = "Roster 2", StartDate = DateTime.Now.AddDays(8), EndDate = DateTime.Now.AddDays(14) },
            new Roster(){ Id = 3, Name = "Roster 3", StartDate = DateTime.Now.AddDays(15), EndDate = DateTime.Now.AddDays(21) },
            new Roster(){ Id = 4, Name = "Roster 4", StartDate = DateTime.Now.AddDays(22), EndDate = DateTime.Now.AddDays(28) },
            new Roster(){ Id = 5, Name = "Roster 5", StartDate = DateTime.Now.AddDays(39), EndDate = DateTime.Now.AddDays(35) },
        };

        public async Task<Roster[]> GetRosters(DateTime from, DateTime to)
        {
            return allRosters.Where(r => r.StartDate >= from && r.EndDate <= to).ToArray();
        }

        public async Task<bool> SaveRoster(Roster roster)
        {
            allRosters.Add(roster);
            return true;
        }
    }
}
