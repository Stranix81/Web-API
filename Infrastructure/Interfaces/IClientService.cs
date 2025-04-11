using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs.Filters;

namespace Infrastructure.Interfaces
{
    public interface IClientService
    {
        /// <summary>
        /// Get all clients
        /// </summary>
        /// <returns><see cref="List{T}"/> list of clients</returns>
        Task<IEnumerable<GetClientDTO>> GetClientsAsync();

        /// <summary>
        /// Get a client by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Client ID</param>
        /// <returns><see cref="GetClientDTO"/> client, or <see langword="null"/></returns>
        Task<GetClientDTO> GetClientAsync(int id);

        /// <summary>
        /// Get clients by <paramref name="filter"/> and <paramref name="pagination"/>
        /// </summary>
        /// <param name="filter">Filter info</param>
        /// <param name="pagination">Pagination info</param>
        /// <returns><see cref="List{T}"/> list of filtered clients</returns>
        Task<IEnumerable<GetClientDTO>> GetClientsFilteredAsync(ClientFilterDTO filter, PaginationDTO pagination);

        /// <summary>
        /// Create a new client
        /// </summary>
        /// <param name="dto">Client info</param>
        /// <returns><see cref="GetClientDTO"/> client</returns>
        Task<GetClientDTO> PostClientAsync(PostPutClientDTO dto);

        /// <summary>
        /// Edit a client by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Client ID</param>
        /// <param name="dto">Client info</param>
        /// <returns><see cref="GetClientDTO"/> client, or <see langword="null"/></returns>
        Task<GetClientDTO> PutClientAsync(int id, PostPutClientDTO dto);

        /// <summary>
        /// Delete a client by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Client ID</param>
        /// <returns><see langword="true"/> for success, <see langword="false"/> for not found error</returns>
        Task<bool> DeleteClientAsync(int id);

        bool ClientExists(int id);
    }
}
