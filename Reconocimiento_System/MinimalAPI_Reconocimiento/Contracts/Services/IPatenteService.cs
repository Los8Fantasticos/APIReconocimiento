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
    }
}
