using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Core;
using OdeToFood.Data;
using System.Collections.Generic;

namespace OdeToFood.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly IRestaurantData _restaurantData;

        public string Message { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }

        // Functions as output and input model with this property. Makes this property receive information from the request.
        // By default asp.net only binds to input properties when doing a http POST operation. Can add the SupportsGet flag to make it work with a GET request
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public ListModel(
            IConfiguration config,
            IRestaurantData restaurantData)
        {
            _config = config;
            _restaurantData = restaurantData;
        }

        // As an 'output model' property
        //public string SearchTerm { get; set; }

        // If searchTerm is a parameter, asp.net core will dig through the request for something of that name. Name has to match.
        //public void OnGet(string searchTerm)
        //{
        //    // You can make searchTerm accessible to the page by adding an 'output model' property SaerchTerm, as above.
        //    SearchTerm = searchTerm;

        public void OnGet()
        {
            //Message = "Hello, World!";
            Message = _config["Message"];
            Restaurants = _restaurantData.GetRestaurantsByName(SearchTerm);
        }
    }
}