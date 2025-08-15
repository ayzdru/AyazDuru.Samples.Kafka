using AyazDuru.Samples.Kafka.Publisher.Services;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AyazDuru.Samples.Kafka.Publisher.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        // Kafka'ya mesaj g�ndermek i�in kullan�lan servis
        private readonly ProducerService _producerService;

        // Razor Page'den gelen mesaj� tutan property
        [BindProperty]
        public string Message { get; set; }
       
        // Ba��ml�l�klar� (logger ve producerService) enjekte eden constructor
        public IndexModel(ILogger<IndexModel> logger, ProducerService producerService)
        {
            _logger = logger;
            _producerService = producerService;
        }

        public void OnGet()
        {

        }

        // Form g�nderildi�inde �al��an ve mesaj� Kafka'ya ileten metot
        public async Task<IActionResult> OnPostAsync()
        {
            // Mesaj� "Messages" adl� Kafka topic'ine g�nderir
            await _producerService.PublishAsync("Messages", Message);
            return RedirectToPage();
        }
    }
}
