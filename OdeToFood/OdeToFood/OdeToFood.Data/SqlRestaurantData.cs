using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext _db;

        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            _db = db;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            _db.Restaurants.Add(newRestaurant);

            // Could also use this as ef can work out that you are adding a restaurant so knows where to put it.
            //_db.Add(newRestaurant);

            return newRestaurant;
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetById(id);

            if (restaurant != null)
            {
                _db.Restaurants.Remove(restaurant);
            }

            return restaurant;
        }

        public Restaurant GetById(int id)
        {
            return _db.Restaurants.Find(id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            var query = from r in _db.Restaurants
                        where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby r.Name
                        select r;
            return query;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            // Attach tells ef to start tracking changes about the object. Not adding a new one or retrieving one.
            var entity = _db.Restaurants.Attach(updatedRestaurant);
            
            // Tells ef that 'everything' has changed about this restaurant.
            entity.State = EntityState.Modified;

            return updatedRestaurant;
        }

        public int GetCountOfRestaurants()
        {
            return _db.Restaurants.Count();
        }

    }

}
