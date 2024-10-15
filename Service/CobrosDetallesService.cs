using System.Linq.Expressions;
using WilmerCastillo_Ap1_P1.DAL;
using WilmerCastillo_Ap1_P1.Models;
using Microsoft.EntityFrameworkCore;

namespace WilmerCastillo_Ap1_P1.Service
{
    public class CobrosDetallesService
    {
        public class CobrosDetallesServices(Context context)
        {
            private readonly Context _context = context;

            public async Task<List<Prestamos>> Listar(Expression<Func<Prestamos, bool>> criterio)
            {
                return await _context.Prestamos
                    .AsNoTracking().Where(criterio).ToListAsync();
            }
        }
    }
}
