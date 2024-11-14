using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDirectionService
    {
        Task<IEnumerable<Direction>> GetAllDirections();
        Task<IEnumerable<Direction>> GetAllDirectionsByClientId(int id);
        Task<Direction> GetUniqueDirection(int id);
        Task<bool> AddDirection(Direction direction);
        Task<bool> UpdateDirection(Direction direction);
        Task<bool> DeleteDirection(int id);
    }
}
