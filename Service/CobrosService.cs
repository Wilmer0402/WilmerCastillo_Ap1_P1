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
            return await _context.Cobros.AnyAsync(a => a.CobrosId == id);
        }

        private async Task<bool> Insertar(Cobros cobros)
        {
            _context.Cobros.Add(cobros);
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Cobros cobros)
        {
            var existingCobros = await _context.Cobros
                .Include(c => c.CobrosDetalles)
                .FirstOrDefaultAsync(c => c.CobrosId == cobros.CobrosId);

            if (existingCobros != null)
            {

                _context.Entry(existingCobros).CurrentValues.SetValues(cobros);


                var detallesToRemove = existingCobros.CobrosDetalles
                    .Where(d => !cobros.CobrosDetalles.Any(cd => cd.DetallesId == d.DetallesId))
                    .ToList();

                foreach (var detalle in detallesToRemove)
                {
                    _context.CobrosDetalles.Remove(detalle);
                }


                foreach (var nuevoDetalle in cobros.CobrosDetalles)
                {
                    if (nuevoDetalle.DetallesId > 0)
                    {
                        var existingDetalle = existingCobros.CobrosDetalles
                            .FirstOrDefault(d => d.DetallesId == nuevoDetalle.DetallesId);

                        if (existingDetalle != null)
                        {

                            _context.Entry(existingDetalle).CurrentValues.SetValues(nuevoDetalle);
                        }
                    }
                    else
                    {
                        existingCobros.CobrosDetalles.Add(nuevoDetalle);
                    }
                }

                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }


        public async Task<bool> Guardar(Cobros cobros)
        {
            if (!await Existe(cobros.CobrosId))
                return await Insertar(cobros);
            else
                return await Modificar(cobros);
        }

        public async Task<bool> Eliminar(int id)
        {
            var Cobros = await _context.Cobros.FindAsync(id);
            if (Cobros != null)
            {
                _context.Cobros.Remove(Cobros);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Cobros> Buscar(int id)
        {
            return await _context.Cobros.Include(d => d.Deudores).Include(d => d.CobrosDetalles).AsNoTracking().FirstOrDefaultAsync(a => a.CobrosId == id);
        }

        public async Task<List<Cobros>> Listar(Expression<Func<Cobros, bool>> criterio)
        {
            return await _context.Cobros.Include(d => d.Deudores).Include(d => d.CobrosDetalles).AsNoTracking().Where(criterio).ToListAsync();
        }

        public async Task<Cobros> ObtenerPorId(int id)
        {
            return await _context.Cobros
                .Include(c => c.CobrosDetalles)
                .FirstOrDefaultAsync(c => c.CobrosId == id);
        }

    }
}
