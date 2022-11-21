namespace MinimalAPI_Reconocimiento.Contracts.Services
{
    public interface IPatenteService
    {
        /// <summary>
        /// Valida si está registrada la patente en la base de datos.
        /// </summary>
        /// <param name="PatenteDTO">Propiedad Patente</param>
        /// <returns></returns>
        public Task<bool> ValidatePatente(string PatenteDTO);
        /// <summary>
        /// Obtiene el tráfico del día.
        /// </summary>
        /// <returns></returns>
        public Task<int> GetTrafico();
        public Task<int> GetTraficoReconocido();
        public Task<int> GetTraficoNoReconocido();

    }
}
