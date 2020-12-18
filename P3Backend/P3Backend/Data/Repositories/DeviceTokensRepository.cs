using System;
using Microsoft.EntityFrameworkCore;
using P3Backend.Model.RepoInterfaces;
using P3Backend.Model.Users;
using System.Collections.Generic;
using System.Linq;


namespace P3Backend.Data.Repositories
{
    public class DeviceTokensRepository : IDeviceTokensRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<DeviceTokens> _deviceTokens;
        
        public DeviceTokensRepository(ApplicationDbContext context) {
            _context = context;
            _deviceTokens = _context.DeviceTokens;
        }
        
        public DeviceTokens Get()
        { 
            return _deviceTokens.First();
        }
        
        public void Update(DeviceTokens dt) {
            _deviceTokens.Update(dt);
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }
    }
}