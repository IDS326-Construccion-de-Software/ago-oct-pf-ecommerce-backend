using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Revenge.Infrestructure.Entities;


namespace Revenge.Data.Context
{
    public partial class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
        //Relacion con usuario (por terminar)
        public virtual DbSet<User> Users { get; set; }

    }    
}