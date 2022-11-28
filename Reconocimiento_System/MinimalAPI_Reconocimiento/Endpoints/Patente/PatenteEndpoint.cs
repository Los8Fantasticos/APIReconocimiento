﻿using MinimalAPI_Reconocimiento.Contracts.Services;
using MinimalAPI_Reconocimiento.Endpoints.Errors;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;

namespace MinimalAPI_Reconocimiento.Endpoints.Patente
{
    [ExcludeFromCodeCoverage]
    public class PatenteEndpoint
    {
        private readonly IPatenteService _patenteService;
        private readonly ILogger _logger;
        public PatenteEndpoint(IPatenteService patenteService, ILoggerFactory logger)
        {
            _patenteService = patenteService;
            _logger = logger.CreateLogger<PatenteEndpoint>();
        }
        public async Task MapPatenteEndpoints(WebApplication app)
        {
            _ = app.MapGet(
               "/api/patentes/transitoGenerado",
               async () =>
               {
                   try
                   {
                       _logger.LogInformation("Obteniendo transito del día...");
                       int result = await _patenteService.GetTrafico();
                       return result;
                   }
                   catch (Exception ex)
                   {
                       _logger.LogError(ex.Message, "Error al obtener transito generado.");
                       throw;
                   }
               })
                
           .WithTags("Patente")
           .WithMetadata(new SwaggerOperationAttribute("..."))
           .Produces<Models.ApplicationModel.PatenteModel>(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);

            _ = app.MapGet(
               "/api/patentes/transitoReconocido",
               async () =>
               {
                   try
                   {
                       _logger.LogInformation("Obteniendo transito reconocido del día...");
                       int result = await _patenteService.GetTraficoReconocido();
                       return result;
                   }
                   catch (Exception ex)
                   {
                       _logger.LogError(ex.Message, "Error al obtener transito reconocido.");
                       throw;
                   }
               })
           .WithTags("Patente")
           .WithMetadata(new SwaggerOperationAttribute("..."))
           .Produces<Models.ApplicationModel.PatenteModel>(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);

            _ = app.MapGet(
               "/api/patentes/transitoNoReconocido",
               async () =>
               {
                   try
                   {
                       _logger.LogInformation("Obteniendo transito no reconocido del día...");
                       int result = await _patenteService.GetTraficoNoReconocido();
                       return result;
                   }
                   catch (Exception ex)
                   {
                       _logger.LogError(ex.Message, "Error al obtener el transito no reconocido.");
                       throw;
                   }
               })
           .WithTags("Patente")
           .WithMetadata(new SwaggerOperationAttribute("..."))
           .Produces<Models.ApplicationModel.PatenteModel>(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
           .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);
        }

    }
}
