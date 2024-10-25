using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WilmerCastillo_Ap1_P1.DAL;
using WilmerCastillo_Ap1_P1.Models;

namespace WilmerCastillo_Ap1_P1.Service
{
    public class CobrosService(Context context)
    {
        private readonly Context _context = context;


        public async Task<bool> Existe(int id)
        {
            return await _context.Cobros
                .AnyAsync(a => a.CobrosId == id);
        }

        private async Task<bool> Insertar(Cobros cobros)
        {
            _context.Cobros.Add(cobros);
            await AfectarPrestamos(cobros.CobrosDetalles.ToArray(), TipoOperacion.Resta);
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Cobros cobros)
        {
            var cobroOriginal = await _context.Cobros
                .Include(c => c.CobrosDetalles)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CobrosId == cobros.CobrosId);

            if (cobroOriginal == null)
                return false;

            await AfectarPrestamos(cobroOriginal.CobrosDetalles.ToArray(), TipoOperacion.Suma);
            await AfectarPrestamos(cobros.CobrosDetalles.ToArray(), TipoOperacion.Resta);
            _context.Update(cobros);
            return await _context.SaveChangesAsync() > 0;
        }


        private async Task AfectarPrestamos(CobrosDetalles[] detalle, TipoOperacion tipoOperacion)
        {
            foreach (var item in detalle)
            {
                var prestamo = await _context.Prestamos.SingleAsync(p => p.PrestamosId == item.PrestamosId);
                if (tipoOperacion == TipoOperacion.Resta)
                    prestamo.Balance -= item.ValorCobrado;
                else
                    prestamo.Balance += item.ValorCobrado;

            }
        }

        public async Task<bool> Guardar(Cobros cobros)
        {
            if (!await Existe(cobros.CobrosId))
                return await Insertar(cobros);
            else
                return await Modificar(cobros);
        }

        public async Task<bool> Eliminar(int cobroId)
        {

            var cobro = await _context.Cobros
             .Include(c => c.CobrosDetalles)
             .FirstOrDefaultAsync(c => c.CobrosId == cobroId);

            if (cobro == null) return false;

            await AfectarPrestamos(cobro.CobrosDetalles.ToArray(), TipoOperacion.Suma);

            _context.CobrosDetalles.RemoveRange(cobro.CobrosDetalles);
            _context.Cobros.Remove(cobro);
            var cantidad = await _context.SaveChangesAsync();
            return cantidad > 0;
        }

        public async Task<Cobros> Buscar(int id)
        {
            return await _context.Cobros
                .Include(d => d.Deudores)
                .Include(d => d.CobrosDetalles)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.CobrosId == id);
        }

        public async Task<List<Cobros>> Listar(Expression<Func<Cobros, bool>> criterio)
        {
            return await _context.Cobros
                .Include(d => d.Deudores)
                .Include(d => d.CobrosDetalles)
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }

        public async Task<Cobros> ObtenerPorId(int id)
        {
            return await _context.Cobros
                .Include(c => c.CobrosDetalles)
                .FirstOrDefaultAsync(c => c.CobrosId == id);
        }

    }

    public enum TipoOperacion
    {
        Suma = 1,
        Resta = 2
    }
}
