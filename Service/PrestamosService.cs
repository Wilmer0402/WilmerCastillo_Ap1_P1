using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WilmerCastillo_Ap1_P1.DAL;
using WilmerCastillo_Ap1_P1.Models;

public class PrestamosService
{
    public readonly Context _context;

    public PrestamosService(Context context) { _context = context; }

    public async Task<bool> Existe(int id)
    {
        return await _context.Prestamos.AnyAsync(w => w.PrestamosId == id);
    }

    public async Task<bool> Modificar(Prestamos prestamos)
    {
        var existente = await _context.Prestamos.FindAsync(prestamos.PrestamosId);

        if (existente != null)
        {
            existente.DeudorId = prestamos.DeudorId;
            existente.Concepto = prestamos.Concepto;
            existente.Monto = prestamos.Monto;
          

            return await _context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<bool> Insertar(Prestamos prestamos)
    {
        _context.Prestamos.Add(prestamos);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Prestamos prestamos)
    {
        if (!await Existe(prestamos.PrestamosId))
            return await Insertar(prestamos);
        return await Modificar(prestamos);
    }

    public async Task<bool> Eliminar(int id)
    {
        var prestamos = await _context.Prestamos.FirstOrDefaultAsync(w => w.PrestamosId == id);
        if (prestamos != null)
        {
            _context.Prestamos.Remove(prestamos);
            return await _context.SaveChangesAsync() >= 0;
        }
        return false;
    }

    public async Task<List<Prestamos>> Listar(Expression<Func<Prestamos, bool>> criterio)
    {
        return await _context.Prestamos
            .Include(w => w.Deudor)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<Prestamos> Buscar(int id)
    {
        return await _context.Prestamos.AsNoTracking()
            .Include(w => w.Deudor)
            .FirstOrDefaultAsync(w => w.PrestamosId == id);
    }
}
