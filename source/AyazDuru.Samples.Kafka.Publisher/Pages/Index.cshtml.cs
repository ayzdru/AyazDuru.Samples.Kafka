using AyazDuru.Samples.Kafka.Publisher.Services;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AyazDuru.Samples.Kafka.Publisher.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        // Kafka'ya mesaj göndermek için kullanýlan servis
        private readonly ProducerService _producerService;

        // Razor Page'den gelen mesajý tutan property
        [BindProperty]
        public string Message { get; set; }
       
        // Baðýmlýlýklarý (logger ve producerService) enjekte eden constructor
        public IndexModel(ILogger<IndexModel> logger, ProducerService producerService)
        {
            _logger = logger;
            _producerService = producerService;
        }

        public void OnGet()
        {

        }

        // Form gönderildiðinde çalýþan ve mesajý Kafka'ya ileten metot
        public async Task<IActionResult> OnPostAsync()
        {
            // Mesajý "Messages" adlý Kafka topic'ine gönderir
            await _producerService.PublishAsync("Messages", Message);
            return RedirectToPage();
        }
    }
}
