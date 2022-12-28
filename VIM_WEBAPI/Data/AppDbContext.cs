using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;



namespace VIM_WEBAPI.Data
{
    public class AppDbContext :DbContext
    {
     

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

       
    }
}
