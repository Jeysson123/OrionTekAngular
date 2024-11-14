using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models;
using Models.Dtos;
using Models.Entities;
using Services.Interfaces;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services
{
    public class DirectionService : IDirectionService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<DirectionService>? _logger;
        private readonly string logsPath;

        public DirectionService(IConfiguration configuration, ApplicationContext context, ILogger<DirectionService> logger)
        {
            _context = context;
            _logger = logger;
            logsPath = configuration.GetConnectionString("LogsPath");
        }

        public async Task<IEnumerable<Direction>> GetAllDirections()
        {
            try
            {
                HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Directions Found!", CurrentStatus = true, CurrentCode = HttpStatusUtils.OK });

                return await Task.FromResult(await _context.Directions.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger?.LogError($"¡Error trying to get Directions! : {ex.Message}");
                LogUtils.Log(logsPath, ex.Message);
                HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Error getting Directions!", CurrentStatus = false, CurrentCode = HttpStatusUtils.INTERNAL_ERROR });
                return await Task.FromResult(new List<Direction>());
            }
        }

        public async Task<IEnumerable<Direction>> GetAllDirectionsByClientId(int id)
        {
            try
            {
                HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Directions Found!", CurrentStatus = true, CurrentCode = HttpStatusUtils.OK });

                return await Task.FromResult((await _context.Directions.ToListAsync()).FindAll(dr => dr.ClientId == id));

            }
            catch (Exception ex)
            {
                _logger?.LogError($"¡Error trying to get Directions! : {ex.Message}");
                LogUtils.Log(logsPath, ex.Message);
                HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Error getting Directions!", CurrentStatus = false, CurrentCode = HttpStatusUtils.INTERNAL_ERROR });
                return await Task.FromResult(new List<Direction>());
            }
        }

        public async Task<Direction> GetUniqueDirection(int id)
        {
            try
            {
                var direction = await _context.Directions.FirstOrDefaultAsync(d => d.Id == id);

                if (direction != null)
                {
                    HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Direction Found!", CurrentStatus = true, CurrentCode = HttpStatusUtils.OK });
                }
                else
                {
                    HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Direction not found!", CurrentStatus = false, CurrentCode = HttpStatusUtils.NOT_FOUND });
                }

                return await Task.FromResult(direction);
            }
            catch (Exception ex)
            {
                _logger?.LogError($"¡Error trying to get Direction! : {ex.Message}");
                LogUtils.Log(logsPath, ex.Message);
                HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Error getting Direction!", CurrentStatus = false, CurrentCode = HttpStatusUtils.INTERNAL_ERROR });
                return await Task.FromResult(new Direction());
            }
        }

        public async Task<bool> AddDirection(Direction direction)
        {
            try
            {
                await _context.Directions.AddAsync(direction);

                await _context.SaveChangesAsync();

                HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Direction Added!", CurrentStatus = true, CurrentCode = HttpStatusUtils.OK });
                
                return await Task.FromResult(true);
            }

            catch (Exception ex)
            {
                _logger?.LogError($"¡Error trying to add Direction! : {ex.Message}");
                LogUtils.Log(logsPath, ex.Message);
                HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Error adding Direction!", CurrentStatus = false, CurrentCode = HttpStatusUtils.INTERNAL_ERROR });
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> UpdateDirection(Direction direction)
        {
            try
            {
                _context.Directions.Update(direction);

                await _context.SaveChangesAsync();

                HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Direction Updated!", CurrentStatus = true, CurrentCode = HttpStatusUtils.OK });
               
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger?.LogError($"¡Error trying to update Direction! : {ex.Message}");
                LogUtils.Log(logsPath, ex.Message);
                HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Error updating Direction!", CurrentStatus = false, CurrentCode = HttpStatusUtils.INTERNAL_ERROR });
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> DeleteDirection(int id)
        {
            try
            {
                var direction = await _context.Directions.FindAsync((long)id);

                if (direction == null)
                {
                    HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Direction not found!", CurrentStatus = false, CurrentCode = HttpStatusUtils.NOT_FOUND });
                    return false;
                }

                _context.Directions.Remove(direction);

                await _context.SaveChangesAsync();

                HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Direction Deleted!", CurrentStatus = true, CurrentCode = HttpStatusUtils.OK });
               
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger?.LogError($"¡Error trying to delete Direction! : {ex.Message}");
                LogUtils.Log(logsPath, ex.Message);
                HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Error deleting Direction!", CurrentStatus = false, CurrentCode = HttpStatusUtils.INTERNAL_ERROR });
                return await Task.FromResult(false);
            }
        }
    }
}
