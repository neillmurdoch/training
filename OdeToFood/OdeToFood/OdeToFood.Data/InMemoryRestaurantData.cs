using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> _restaurants;

        public InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Name = "Scott's Pizza", Location="Maryland", Cuisine = CuisineType.Italian },
                new Restaurant { Id = 2, Name = "Cinnamon Club", Location="London", Cuisine = CuisineType.Italian },
                new Restaurant { Id = 3, Name = "La Costa", Location="California", Cuisine = CuisineType.Mexican }
            };
        }

        public Restaurant GetById(int id)
        {
            return _restaurants.SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            //return restaurants.OrderBy(r => r.Name);

            var restaurants = from r in _restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name, System.StringComparison.CurrentCultureIgnoreCase)
                   orderby r.Name
                   select r;

            return restaurants;
        }
    }
}
