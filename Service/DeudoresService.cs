using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Linq.Expressions;
using WilmerCastillo_Ap1_P1.DAL;
using WilmerCastillo_Ap1_P1.Models;

namespace WilmerCastillo_Ap1_P1.Service
{
    public class DeudoresService
    {
        public readonly Context _context;
        public DeudoresService(Context context)
        {
            _context = context; 
        }

        public async Task<List<Deudores>> Listar(Expression<Func<Deudores, bool>> criterio)
        {
            return await _context.Deudores.AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}
