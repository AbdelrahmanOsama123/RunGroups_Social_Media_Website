using Advanced_Web_Application.Models;

namespace Advanced_Web_Application.ViewModels
{
    public class HomeViewModel
    {
        public string City { get; set; }

        public string State { get; set; }

        public IEnumerable<Club> clubs { get; set; }

    }
}
