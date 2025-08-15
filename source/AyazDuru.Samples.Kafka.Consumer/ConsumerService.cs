using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyazDuru.Samples.Kafka.Consumer
{
    // Kafka'dan mesajları arka planda dinleyen servis
    public class ConsumerService : BackgroundService
    {
        // Kafka'dan mesaj almak için kullanılan consumer nesnesi
        private readonly IConsumer<Ignore, string> _consumer;

        // Servis başlatılırken consumer yapılandırılır
        public ConsumerService(IConfiguration configuration)
        {
            // Kafka consumer yapılandırması
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092", // Kafka sunucu adresi
                GroupId = "MessagesConsumerGroup",   // Consumer group adı
                AutoOffsetReset = AutoOffsetReset.Earliest // En eski mesajdan başlat
            };

            // Consumer nesnesi oluşturuluyor
            _consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
        }

        // Arka planda sürekli olarak Kafka'dan mesaj dinleyen ana metot
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // "Messages" adlı topic'e abone olunur
            _consumer.Subscribe("Messages");
            Console.WriteLine("Kafka'dan mesaj bekleniyor...");

            // Servis durdurulana kadar sürekli dinleme yapılır
            while (!stoppingToken.IsCancellationRequested)
            {
                // Mesaj işleme metodu çağrılır
                ProcessKafkaMessage(stoppingToken);

                // 1 dakika beklenir (asenkron bekleme eksik, istenirse await eklenebilir)
                Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }

            // Consumer kapatılır
            _consumer.Close();
        }

        // Kafka'dan gelen mesajı işleyen metot
        public void ProcessKafkaMessage(CancellationToken stoppingToken)
        {
            try
            {
                // Mesaj alınır
                var consumeResult = _consumer.Consume(stoppingToken);

                // Mesaj içeriği alınır
                var message = consumeResult.Message.Value;

                // Mesaj konsola yazdırılır
                Console.WriteLine($"Kafka Gelen Mesaj: {message}");
            }
            catch (Exception ex)
            {
                // Hata durumunda konsola hata mesajı yazdırılır
                Console.WriteLine($"Kafka hata meydana geldi: {ex.Message}");
            }
        }
    }
}
