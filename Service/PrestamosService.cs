using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WilmerCastillo_Ap1_P1.DAL;
using WilmerCastillo_Ap1_P1.Models;

namespace WilmerCastillo_Ap1_P1.Service

{
    public class PrestamosService(Context context)
    {
        private readonly Context _context = context;


        public async Task<bool> Existe(int id)
        {
            return await _context.Prestamos.AnyAsync(a => a.PrestamosId == id);
        }

        public async Task<bool> Insertar(Prestamos prestamos)
        {
            _context.Prestamos.Add(prestamos);
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Prestamos prestamos)
        {
            var existingPrestamos = await _context.Prestamos.FindAsync(prestamos.PrestamosId);
            if (existingPrestamos != null)
            {
                _context.Entry(existingPrestamos).CurrentValues.SetValues(prestamos);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> Guardar(Prestamos prestamos)
        {
            if (!await Existe(prestamos.PrestamosId))
                return await Insertar(prestamos);
            else
                return await Modificar(prestamos);
        }

        public async Task<bool> Eliminar(int id)
        {
            var Prestamos = await _context.Prestamos.FindAsync(id);
            if (Prestamos != null)
            {
                _context.Prestamos.Remove(Prestamos);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Prestamos> Buscar(int id)
        {
            return await _context.Prestamos.Include(d => d.Deudor).AsNoTracking().FirstOrDefaultAsync(a => a.PrestamosId == id);
        }

        public async Task<List<Prestamos>> Listar(Expression<Func<Prestamos, bool>> criterio)
        {
            return await _context.Prestamos.Include(d => d.Deudor).AsNoTracking().Where(criterio).ToListAsync();
        }

        public async Task<List<Prestamos>> ObtenerPrestamosPorDeudor(int deudorId)
        {

            return await _context.Prestamos
                                 .Where(p => p.DeudorId == deudorId)
                                 .ToListAsync();
        }

        public async Task<Prestamos> GetCliente(int deudorId)
        {
            return await _context.Prestamos.FirstOrDefaultAsync(p => p.DeudorId == deudorId);
        }

        public async Task<Prestamos> ObtenerPorId(int id)
        {
            return await _context.Prestamos.FirstOrDefaultAsync(p => p.PrestamosId == id);
        }

        public async Task<Prestamos?> BuscarPrestamo(int id)
        {
            return await _context.Prestamos.Include(p => p.Deudor).FirstOrDefaultAsync(p => p.DeudorId == id);
        }
    }
}