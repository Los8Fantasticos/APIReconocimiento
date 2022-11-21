using MinimalAPI_Reconocimiento.Contracts.Repositories;
using MinimalAPI_Reconocimiento.Contracts.Services;
using RabbitMqService.Abstractions;
using RabbitMqService.Queues;

namespace MinimalAPI_Reconocimiento.Services
{
    public class PatenteService : IPatenteService, IMessageReceiver<string>
    {
        private readonly IPatenteRepository _patenteRepository;
        private readonly IMessageSender _messageSender;
        private readonly ILogger logger;
        public PatenteService(IPatenteRepository patenteRepository, ILogger<PatenteService> logger, IMessageSender messageSender)
        {
            _patenteRepository = patenteRepository;
            _messageSender = messageSender;
            this.logger = logger;
        }

        public async Task ReceiveAsync(string message, CancellationToken cancellationToken)
        {
            logger.LogInformation("Mensaje recibido para validación de patente");

            var exists = await ValidatePatente(message);
            if(!exists)
                await _messageSender.PublishAsync<Multas,string>(message);
            else
                await _messageSender.PublishAsync<Pagos, string>(message);
            
            logger.LogInformation($"Patente {message} validada.");
        }

        //validar si existe la patente en nuestra base de datos y si está activa o no.
        public async Task<bool> ValidatePatente(string PatenteDTO)
        {
            try
            {
                var PatenteModel = await _patenteRepository.GetPatente(PatenteDTO);
                bool exists = PatenteModel != null && (PatenteModel.Active);

                var LastTrafic = await _patenteRepository.GetLastTrafic();
                //Validar si la fecha de LastTrafic es mayor que hoy
                if (LastTrafic != null && LastTrafic.Fecha.Day != DateTime.Now.Day)
                    await _patenteRepository.CountPatente(exists);
                else if (LastTrafic == null)
                    await _patenteRepository.CountPatente(exists);
                else
                    await _patenteRepository.CountPatente(exists, LastTrafic);
                return exists;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al validar patente");
                throw;
            }

        }

        
    }
}
