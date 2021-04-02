using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using API.Data;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.API.DTOs;
using DatingApp.API.Entities;
using DatingApp.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<MemberDTO> GetMemberAsync(string username)
        {
            return await _context.Users
                         .Where(x => x.UserName == username)
                         .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                         .SingleOrDefaultAsync();
        }

        public async Task<PageList<MemberDTO>> GetMembersAsync(UserParams userParams)
        {
            var query = _context.Users
                        .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                        .AsNoTracking()
                        .AsQueryable();

            query = query.Where(u => u.Username != userParams.CurrentUsername); 
            query = query.Where(u => u.Gender != userParams.Gender); 

            var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1); 
            var maxDob = DateTime.Today.AddYears(-userParams.MinAge); 

             query = query.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob); 
                        
            return await PageList<MemberDTO>.CreateAsync(query.ProjectTo<MemberDTO>(_mapper.ConfigurationProvider).AsNoTracking(),
                                                         userParams.PageNumber,
                                                         userParams.PageSize); 
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                         .Include(p => p.Photos)
                         .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
                         .Include(p => p.Photos)
                         .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

       
    }
}