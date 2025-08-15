using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace AyazDuru.Samples.Kafka.Publisher.Services
{
    public class ProducerService    
    {
        // Kafka'ya mesaj göndermek için kullanılan producer nesnesi
        private readonly IProducer<Null, string> _producer;

        // ProducerService sınıfı başlatılırken Kafka producer'ı yapılandırılır
        public ProducerService()
        {
            // Kafka sunucusunun adresini belirten yapılandırma
            var producerconfig = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            // Belirtilen yapılandırma ile producer nesnesi oluşturuluyor
            _producer = new ProducerBuilder<Null, string>(producerconfig).Build();
        }

        // Belirtilen topic'e asenkron olarak mesaj gönderir
        public async Task PublishAsync(string topic, string message)
        {           
            // Mesajı Kafka'ya gönderir
            await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
        }
    }
}
