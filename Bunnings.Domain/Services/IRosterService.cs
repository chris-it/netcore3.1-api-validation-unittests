using Bunnings.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Bunnings.Domain.Services
{
    public interface IRosterService
    {
        Task<Roster[]> GetRosters(DateTime from, DateTime to);
        Task<bool> SaveRoster(Roster roster);
    }
}
