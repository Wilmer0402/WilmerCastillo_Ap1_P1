using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WilmerCastillo_Ap1_P1.DAL;
using WilmerCastillo_Ap1_P1.Models;

namespace WilmerCastillo_Ap1_P1.Service
{
    public class CobrosService
    {
        private readonly Context _context;

        public CobrosService(Context context)
        {
            _context = context;
        }

        public async Task<bool> Existe(int id)
        {
            return await _context.Cobros.AnyAsync(w => w.CobrosId == id);
        }

        public async Task<bool> Modificar(Cobros cobro)

        {
            try
            {
                // Asegúrate de que los detalles también se estén actualizando.
                // También puedes ajustar el monto aquí si es necesario.
                _context.Cobros.Attach(cobro);
                _context.Entry(cobro).State = EntityState.Modified;

                foreach (var detalle in cobro.CobrosDetalles)
                {
                    _context.Entry(detalle).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return false;
            }
        }



        public async Task<bool> Insertar(Cobros cobros)
        {
            _context.Cobros.Add(cobros);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Cobros cobros)
        {
            if (!await Existe(cobros.CobrosId))
                return await Insertar(cobros);
            return await Modificar(cobros);
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var Cobros = await _context.Cobros.FindAsync(id);
                if (Cobros != null)
                {
                    _context.Cobros.Remove(Cobros);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    Console.WriteLine($"No se encontró el cobro con ID: {id}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el cobro en la base de datos: {ex.Message}");
            }
            return false;
        }

        public async Task<List<Cobros>> Listar(Expression<Func<Cobros, bool>> criterio)
        {
            return await _context.Cobros
                .Include(w => w.Deudores)
                .Include(w => w.CobrosDetalles)
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }

        public async Task<Cobros> Buscar(int id)
        {
            return await _context.Cobros
                .AsNoTracking()
                .Include(w => w.Deudores)
                .Include(w => w.CobrosDetalles)
                .FirstOrDefaultAsync(w => w.CobrosId == id);
        }

        public async Task<Cobros> BuscarPorId(int cobrosId)
        {
            return await _context.Cobros
                .Include(c => c.CobrosDetalles) // Include the details here
                .FirstOrDefaultAsync(c => c.CobrosId == cobrosId);
        }

    }


}
