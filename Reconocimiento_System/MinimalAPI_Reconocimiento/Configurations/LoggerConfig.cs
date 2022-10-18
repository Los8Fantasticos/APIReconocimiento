using Serilog;

using System.Reflection;

namespace MinimalAPI_Reconocimiento.Configurations
{
    public static class LoggerConfig
    {
        [System.Obsolete]
        public static WebApplicationBuilder ConfigureLogger(this IServiceCollection services, WebApplicationBuilder builder)
        {
            #region Logging

            _ = builder.Host.UseSerilog((hostContext, loggerConfiguration) =>
            {
                var assembly = Assembly.GetEntryAssembly();
                _ = loggerConfiguration.WriteTo.MSSqlServer(connectionString: builder.Configuration.GetConnectionString("SqlConnection"),
                    tableName: builder.Configuration.GetSection("Serilog").GetSection("TableName").Value,
                    appConfiguration: builder.Configuration,
                    autoCreateSqlTable: true,
                    columnOptionsSection: builder.Configuration.GetSection("Serilog").GetSection("ColumnOptions"),
                    schemaName: builder.Configuration.GetSection("Serilog").GetSection("SchemaName").Value);
                _ = loggerConfiguration.WriteTo.MSSqlServer(connectionString: builder.Configuration.GetConnectionString("SqlConnection2"),
                    tableName: builder.Configuration.GetSection("Serilog").GetSection("TableName").Value,
                    appConfiguration: builder.Configuration,
                    autoCreateSqlTable: true,
                    columnOptionsSection: builder.Configuration.GetSection("Serilog").GetSection("ColumnOptions"),
                    schemaName: builder.Configuration.GetSection("Serilog").GetSection("SchemaName").Value);
            });

            #endregion Logging

            
            //var loggerConfig = new LoggerConfiguration()
            //    .Filter.ByExcluding(logEvent =>
            //    {
            //        bool hasContext = logEvent.Properties.ContainsKey("SourceContext");

            //        if (hasContext)
            //        {
            //            var sourceContext = (Serilog.Events.ScalarValue)logEvent.Properties["SourceContext"];
            //            return sourceContext.Value.ToString() == "Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware";
            //        }

            //        return false;
            //    })
            //    .ReadFrom.Configuration(configuration, sectionName: "Serilog")
            //    .WriteTo.File("Log.txt", rollingInterval: RollingInterval.Day)
            //    .WriteTo.MSSqlServer(
            //        connectionString: configuration.GetConnectionString("SqlConnection"),
            //        tableName: configuration.GetSection("Serilog").GetSection("TableName").Value,
            //        appConfiguration: configuration,
            //        autoCreateSqlTable: true,
            //        columnOptionsSection: configuration.GetSection("Serilog").GetSection("ColumnOptions"),
            //        schemaName: configuration.GetSection("Serilog").GetSection("SchemaName").Value);
            //.WriteTo.Email(
            //    toEmail: configuration.GetSection("Serilog").GetSection("ToEmail").Value,
            //    fromEmail: configuration.GetSection("Serilog").GetSection("FromEmail").Value,
            //    mailSubject: configuration.GetSection("Serilog").GetSection("MailSubject").Value,
            //    mailServer: configuration.GetSection("Serilog").GetSection("MailServer").Value,
            //    new System.Net.NetworkCredential(
            //        userName: configuration.GetSection("Serilog").GetSection("NetworkCredentials").GetSection("UserName").Value,
            //        password: configuration.GetSection("Serilog").GetSection("NetworkCredentials").GetSection("Password").Value,
            //        domain: configuration.GetSection("Serilog").GetSection("NetworkCredentials").GetSection("Domain").Value));

            //services.AddLogging(builder2 =>
            //{
            //    builder2.AddConfiguration(configuration.GetSection("Logging"))
            //        .AddSerilog(loggerConfig.CreateLogger());
            //});

            return builder;
        }


    }
}
