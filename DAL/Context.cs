using Microsoft.EntityFrameworkCore;
using WilmerCastillo_Ap1_P1.Models;


namespace WilmerCastillo_Ap1_P1.DAL

{
    public class Context(DbContextOptions<Context> options) : DbContext(options)
    {
        public DbSet<Prestamos> Prestamos {  get; set; }

       
    }
}
