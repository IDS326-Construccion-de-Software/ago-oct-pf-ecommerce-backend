using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Revenge.Infrestructure.Entities;


namespace Revenge.Data.Context
{
    public sealed class RevengeDbContext : DbContext
    {
        public RevengeDbContext(DbContextOptions<RevengeDbContext> options)
            : base(options)
        {
            Users = Set<User>();
        }
        //Relacion con usuario (por terminar)
        public DbSet<User> Users { get; }

    }    
}