using ECommerce.Entities.Models;

namespace ECommerce.WebUI.Models
{
    public class AdminViewModel
    {
        public ProductListViewModel? ProductList { get; set; }
        public Category? NewCategory { get; set; }
    }
}
