using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData _restaurantData;
        private readonly IHtmlHelper _htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(
            IRestaurantData restaurantData,
            IHtmlHelper htmlHelper)
        {
            _restaurantData = restaurantData;
            _htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int? restaurantId)
        {
            // Html(helper) is not available in this code...only in the cshtml file. Instead inject an IHtmlHelper.
            Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();

            if (restaurantId.HasValue)
            {
                Restaurant = _restaurantData.GetById(restaurantId.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }

            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        // Original.
        //public IActionResult OnPost()
        //{
        //    // Hard way to validate is with if/else, etc.

        //    // Can inspect binding with...
        //    //ModelState["Location"].Errors   // .AttemptedValue to show what user typed in.

        //    // Use this, based on the attributes in the model object.
        //    if (ModelState.IsValid)
        //    {
        //        _restaurantData.Update(Restaurant);
        //        _restaurantData.Commit();

        //        // IMPORTANT: Don't leave the user on a page with the result of a POST operation. PRG - Post > Redirect > Get

        //        // Create an anonymous object to match the parameters expected by the Detail page OnGet request.
        //        return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id} );
        //    }

        //    // Needs this here because asp.net core is stateless and cuisines will be lost on a post operation.
        //    Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();

        //    //_restaurantData.Update(Restaurant);
        //    //_restaurantData.Commit();

        //    return Page();
        //}

        // Refactored to remove nested ifs.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }

            if (Restaurant.Id > 0)
            {
                _restaurantData.Update(Restaurant);
            }
            else
            {
                _restaurantData.Add(Restaurant);
            }
            _restaurantData.Commit();

            // Could add an additional parameter to the anonymous object to tell the detail page to show a 'just added' message.
            // Trouble is, this could be book marked and would show the message, even though they haven't just added one.
            
            // Instead use TempData which is only available for the next request.
            TempData["Message"] = "Restaurant saved!";

            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id } );
        }
    }
}
