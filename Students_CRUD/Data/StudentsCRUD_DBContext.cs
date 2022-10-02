using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Students_CRUD.Models;

    public class StudentsCRUD_DBContext : DbContext
    {
        public StudentsCRUD_DBContext (DbContextOptions<StudentsCRUD_DBContext> options)
            : base(options)
        {
        }

        public DbSet<Students_CRUD.Models.StudentResults> StudentResults { get; set; } = default!;
    }
