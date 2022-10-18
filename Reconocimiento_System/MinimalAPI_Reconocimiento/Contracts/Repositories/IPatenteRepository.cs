using MinimalAPI_Reconocimiento.Models.ApplicationModel;

namespace MinimalAPI_Reconocimiento.Contracts.Repositories
{
    public interface IPatenteRepository
    {
        public Task<PatenteModel> GetPatente(string PatenteDTO);
        public Task CountPatente(bool IsRecognition, TraficoModel LastTrafic = null);
        public Task<TraficoModel> GetLastTrafic();
    }
}
