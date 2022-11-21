using Microsoft.EntityFrameworkCore;

using MinimalAPI_Reconocimiento.Contracts.Repositories;
using MinimalAPI_Reconocimiento.Models.ApplicationModel;

namespace MinimalAPI_Reconocimiento.Infrastructure.Repositories
{
    public class PatenteRepository : IPatenteRepository
    {
        private readonly ApplicationDbContext _context;
        public PatenteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CountPatente(bool IsRecognition, TraficoModel LastTrafic = null)
        {
            if(LastTrafic != null)
            {
                if (IsRecognition)
                    LastTrafic.PatentesReconocidas++;
                else
                    LastTrafic.PatentesNoReconocidas++;

                _context?.Trafico?.Update(LastTrafic);
                await _context?.SaveChangesAsync();
            }
            else
            {
                TraficoModel traficoModel = new();
                if (IsRecognition)
                    traficoModel.PatentesReconocidas++;
                else
                    traficoModel.PatentesNoReconocidas++;

                traficoModel.Fecha = DateTime.Now;

                _context?.Trafico?.Add(traficoModel);
                await _context?.SaveChangesAsync();
            }
        }

        public async Task<int> GetAllTraffic() => await _context?.Trafico?.Where(x => x.Fecha.Day == DateTime.Now.Day).AsNoTracking().SumAsync(x => x.PatentesReconocidas + x.PatentesNoReconocidas);
        public async Task<TraficoModel> GetLastTraffic() => await _context.Trafico.OrderByDescending(x => x.Fecha).FirstOrDefaultAsync();
        public async Task<int> GetNotRecognizedTraffic() => await _context?.Trafico?.Where(x => x.Fecha.Day == DateTime.Now.Day).AsNoTracking().SumAsync(x => x.PatentesNoReconocidas);
        public async Task<PatenteModel> GetPatente(string PatenteDTO) => await _context?.Patente?.Where(x => x.Patente == PatenteDTO).AsNoTracking().FirstOrDefaultAsync();
        public async Task<int> GetRecognizedTraffic() => await _context?.Trafico?.Where(x => x.Fecha.Day == DateTime.Now.Day).AsNoTracking().SumAsync(x => x.PatentesReconocidas);
    }
}
