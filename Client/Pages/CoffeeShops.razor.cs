using API.Model;
using Microsoft.AspNetCore.Components;

namespace Client.Pages
{
    public partial class CoffeeShops
    {
        private List<CoffeeShopModel> _shops = new();
        [Inject]
        private HttpClient _httpClient { get; set; }
        [Inject]
        private IConfiguration _config { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //return base.OnInitializedAsync();
            var result = await _httpClient.GetAsync($"{_config["apiUrl"]}/api/CoffeeShop");

            if (result.IsSuccessStatusCode)
            {
                _shops = await result.Content.ReadFromJsonAsync<List<CoffeeShopModel>>();
            }
        }
    }
}
