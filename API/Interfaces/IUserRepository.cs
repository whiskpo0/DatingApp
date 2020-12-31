using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.DTOs;
using DatingApp.API.Entities;

namespace DatingApp.API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user); 
        Task<bool> SaveAllAsync(); 
        Task<IEnumerable<AppUser>> GetUsersAsync(); 
        Task <AppUser> GetUserByIdAsync(int id); 
        Task<AppUser> GetUserByUsernameAsync(string username); 
        Task<IEnumerable<MemberDTO>> GetMembersAsync(); 
        Task<MemberDTO> GetMemberAsync(string username); 

    }
}