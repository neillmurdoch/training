using Microsoft.AspNetCore.Mvc;
using OdeToFood.Data;


namespace OdeToFood.Pages.ViewComponents
{
    // ViewComponents don't respond to a http request

    public class RestaurantCountViewComponent : ViewComponent
    {
        private readonly IRestaurantData _restaurantData;

        public RestaurantCountViewComponent(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }

        // IViewComponentResult is very similar to IActionResult and encapsulates what is going to happen next
        public IViewComponentResult Invoke()
        {
            // Would probably cache this as it will be on every page.
            var count = _restaurantData.GetCountOfRestaurants();

            // Difference between view components and razor pages. View components work like mvc...action method that builds a model. Pass that model to a view by returning a view result.
            return View(count);
        }
    }
}
