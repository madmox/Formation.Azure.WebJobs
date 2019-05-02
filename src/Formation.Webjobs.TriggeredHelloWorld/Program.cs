using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Formation.Webjobs.TriggeredHelloWorld
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder();

            // Configuration des extensions du SDK Azure Webjobs utilisées
            builder.ConfigureWebJobs(b =>
            {
                b.AddAzureStorageCoreServices();
            });

            // Configuration des sources de paramétrage de l'application
            builder.ConfigureAppConfiguration(b =>
            {
                // Fichiers de paramétrage JSON pour le développement local
                b.AddJsonFile("appsettings.json", optional: true); // Fichier versionné
                b.AddJsonFile("appsettings.local.json", optional: true); // Fichier non versionné

                // Variables d'environnement pour l'exécution sur Azure
                // (les appsettings Azure sont lus par l'application dans les variables d'environnement)
                b.AddEnvironmentVariables();
            });

            // Configuration des loggers de l'application
            builder.ConfigureLogging((context, b) =>
            {
                // La configuration des loggers sera lue dans la section "Logging" du paramétrage
                b.AddConfiguration(context.Configuration.GetSection("Logging"));

                // Ajout d'un logger Console
                b.AddConsole();
            });

            // Configuration des services de l'application (injection de dépendances)
            builder.ConfigureServices((context, services) =>
            {
                // Configuration des sections de paramétrage, qui pourront être injectées
                // via IOptions<MySettings> dans les différents services
                //services.Configure<MySettings>(context.Configuration.GetSection("My"));

                // Configuration des services
                //services.AddScoped<IMyService, MyServiceImplementation>();
            });

            // Initialisation et démarrage de l'hôte du webjob
            IHost host = builder.Build();
            using (host)
            {
                // Démarre l'hôte du webjob
                await host.StartAsync();

                // Exécution manuelle de la fonction Functions.HelloWorld
                var jobHost = host.Services.GetService(typeof(IJobHost)) as IJobHost;
                await jobHost.CallAsync(nameof(Functions.HelloWorld));

                // Stoppe l'hôte du webjob
                await host.StopAsync();
            }
        }
    }
}
