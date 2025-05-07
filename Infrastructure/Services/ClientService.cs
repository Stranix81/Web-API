using Microsoft.EntityFrameworkCore;
using Infrastructure.Interfaces;
using Core.DTOs;
using Core.DTOs.Filters;
using Core.Models;
using Infrastructure.Data;
     
namespace Infrastructure.Services
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _context;

        public ClientService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetClientDTO>> GetClientsAsync()
        {
            return await _context.clients
                .Select(c => new GetClientDTO(c))
                .ToListAsync();
        }

        public async Task<GetClientDTO> GetClientAsync(int id)
        {
            var client = await _context.clients.FindAsync(id);
            return client != null
                ? new GetClientDTO(client) : null;
        }

        public async Task<IEnumerable<GetClientDTO>> GetClientsFilteredAsync(ClientFilterDTO filter, PaginationDTO pagination)
        {
            var query = _context.clients.AsQueryable();

            if (filter.Name != null)
                query = query.Where(с => с.Name == filter.Name);
            if (filter.Lastname != null)
                query = query.Where(с => с.Lastname == filter.Lastname);
            if (filter.BirthDate != null)
                query = query.Where(с => с.BirthDate == filter.BirthDate);


            var page = pagination.Page;
            var pageSize = pagination.PageSize;

            var clients = await query
                .OrderBy(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new GetClientDTO(c))
                .ToListAsync();

            return clients;
        }

        public async Task<GetClientDTO> PostClientAsync(PostPutClientDTO dto)
        {
            Client client = new Client
            {
                Name = dto.Name,
                Lastname = dto.Lastname,
                BirthDate = dto.BirthDate
            };

            _context.clients.Add(client);
            await _context.SaveChangesAsync();

            return new GetClientDTO(client);
        }

        public async Task<GetClientDTO> PutClientAsync(int id, PostPutClientDTO dto)
        {
            Client newClient = new Client
            {
                Id = id,
                Name = dto.Name,
                Lastname = dto.Lastname,
                BirthDate = dto.BirthDate
            };

            _context.Entry(newClient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return new GetClientDTO(newClient);
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            var client = await _context.clients.FindAsync(id);
            if (client == null)
            {
                return false;
            }

            _context.clients.Remove(client);
            await _context.SaveChangesAsync();

            return true;
        }

        public bool ClientExists(int id)
        {
            return _context.clients.Any(e => e.Id == id);
        }
    }
}
