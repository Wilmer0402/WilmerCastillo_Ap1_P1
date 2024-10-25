using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WilmerCastillo_Ap1_P1.DAL;
using WilmerCastillo_Ap1_P1.Models;

namespace WilmerCastillo_Ap1_P1.Service
{
    public class PrestamosService
    {
        private readonly Context _context;

        public PrestamosService(Context context)
        {
            _context = context;
        }

        public async Task<bool> Existe(int id)
        {
            return await _context.Prestamos
                 .AnyAsync(a => a.PrestamosId == id);
        }

        private async Task<bool> Insertar(Prestamos prestamos)
        {
            _context.Prestamos.Add(prestamos);
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Prestamos prestamos)
        {
            _context.Update(prestamos);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Prestamos prestamos)
        {
            prestamos.Balance = prestamos.Monto;
            if (!await Existe(prestamos.PrestamosId))
                return await Insertar(prestamos);
            else
                return await Modificar(prestamos);
        }

        public async Task<Prestamos> Buscar(int prestamoId)
        {
            return await _context.Prestamos.Include(d => d.Deudor)
                .FirstOrDefaultAsync(p => p.PrestamosId == prestamoId);
        }

        public async Task<bool> Eliminar(int prestamoId)
        {
            return await _context.Prestamos
                .Where(p => p.PrestamosId == prestamoId)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Prestamos>> Listar(Expression<Func<Prestamos, bool>> criterio)
        {
            return await _context.Prestamos
                .Include(d => d.Deudor)
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }

        public async Task<Prestamos?> BuscarPrestamo(int id)
        {
            return await _context.Prestamos
                .Include(p => p.Deudor)
                .FirstOrDefaultAsync(p => p.PrestamosId == id);
        }

        public async Task<List<Prestamos>> GetList(Expression<Func<Prestamos, bool>> criterio)
        {
            return await _context.Prestamos
                .Include(d => d.Deudor)
                .Where(criterio)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Prestamos>> ObtenerPrestamosPorDeudor(int deudorId)
        {

            return await _context.Prestamos
                                 .Where(p => p.DeudorId == deudorId)
                                 .ToListAsync();
        }

        public async Task<Prestamos> GetCliente(int deudorId)
        {
            return await _context.Prestamos
                .FirstOrDefaultAsync(p => p.DeudorId == deudorId);
        }

        public async Task<Prestamos> ObtenerPorId(int id)
        {
            return await _context.Prestamos
                .FirstOrDefaultAsync(p => p.PrestamosId == id);
        }

        public async Task<List<Prestamos>> GetPrestamosPendientes(int deudorId)
        {
            return await _context.Prestamos
                .Where(p => p.DeudorId == deudorId && p.Balance > 0)
                .OrderBy(p => p.PrestamosId)
                .AsNoTracking()
                .ToListAsync();
        }

      
    }

}
    

