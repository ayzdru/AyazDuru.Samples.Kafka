using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AyazDuru.Samples.Kafka.Consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Host yapılandırmasını başlatır (uygulama yaşam döngüsünü yönetir)
            var builder = Host.CreateDefaultBuilder(args);

            // Servisleri yapılandırır ve ConsumerService'i arka plan servisi olarak ekler
            builder.ConfigureServices(services =>
            {
                services.AddHostedService<ConsumerService>();
            });

            // Host'u oluşturur
            var host = builder.Build();

            // Uygulamayı başlatır ve arka plan servislerini çalıştırır
            host.Run();
        }
    }
}
