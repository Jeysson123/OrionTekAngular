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
    public class ClientService : IClientService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<ClientService>? _logger;
        private readonly string logsPath;

        public ClientService(IConfiguration configuration, ApplicationContext wstContext, ILogger<ClientService> logger)
        {
            _context = wstContext;
            _logger = logger;
            logsPath = configuration.GetConnectionString("LogsPath");
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            try
            {
                HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Clients Found!", CurrentStatus = true, CurrentCode = HttpStatusUtils.OK });

                return await Task.FromResult(_context.Clients.ToListAsync()).Result;
            }
            catch (Exception ex)
            {
                _logger?.LogError($"¡Error trying to get Clients! : {ex.Message}");
                LogUtils.Log(logsPath, ex.Message); // --> STORE LOG
                HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Clients Found!", CurrentStatus = false, CurrentCode = HttpStatusUtils.INTERNAL_ERROR });
                return await Task.FromResult(new List<Client>());
            }
        }

        public async Task<Client> GetUniqueClient(int id)
        {
            try
            {
                var clientFound = await _context.Clients.FirstAsync(cl => cl.Id == id);

                if(clientFound != null) HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Client Found!", CurrentStatus = true, CurrentCode = HttpStatusUtils.OK });

                return await Task.FromResult(clientFound);
            }
            catch (Exception ex)
            {
                _logger?.LogError($"¡Error trying to get Client! : {ex.Message}");
                LogUtils.Log(logsPath, ex.Message); // --> STORE LOG
                HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Client not Found!", CurrentStatus = false, CurrentCode = HttpStatusUtils.INTERNAL_ERROR });
                return await Task.FromResult(new Client());
            }
        }

        public async Task<bool> AddClient(Client client)
        {
            try
            {
                await _context.Clients.AddAsync(client);

                await _context.SaveChangesAsync();

                HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Client Inserted!", CurrentStatus = true, CurrentCode = HttpStatusUtils.OK });

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger?.LogError($"¡Error trying to add Client! : {ex.Message}");
                LogUtils.Log(logsPath, ex.Message); // --> STORE LOG
                HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Client not inserted!", CurrentStatus = false, CurrentCode = HttpStatusUtils.INTERNAL_ERROR });
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> UpdateClient(Client client)
        {
            try
            {
                _context.Clients.Update(client);

                await _context.SaveChangesAsync();

                HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Client Updated!", CurrentStatus = true, CurrentCode = HttpStatusUtils.OK });

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger?.LogError($"¡Error trying to update Client! : {ex.Message}");
                LogUtils.Log(logsPath, ex.Message); // --> STORE LOG
                HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Client not updated!", CurrentStatus = false, CurrentCode = HttpStatusUtils.INTERNAL_ERROR });
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> DeleteClient(int id)
        {
            try
            {
                var client = await _context.Clients.FindAsync((long)id);

                if (client == null) return await Task.FromResult(false);

                _context.Clients.Remove(client);

                await _context.SaveChangesAsync();

                HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Client Deleted!", CurrentStatus = true, CurrentCode = HttpStatusUtils.OK });

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger?.LogError($"¡Error trying to add Client! : {ex.Message}");
                LogUtils.Log(logsPath, ex.Message); // --> STORE LOG
                HttpStatusUtils.UpdateStatus(new HttpStatusDto { CurrentMsg = "¡Client not deleted!", CurrentStatus = false, CurrentCode = HttpStatusUtils.INTERNAL_ERROR });
                return await Task.FromResult(false);
            }
        }    
    }
}
