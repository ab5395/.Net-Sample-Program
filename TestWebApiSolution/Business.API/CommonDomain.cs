using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.API;

namespace Business.API
{
    public class CommonDomain
    {
        public EmployeeDbEntities Ee=new EmployeeDbEntities();

        //Country
        public void AddCountry(Country country)
        {
            Ee.Countries.Add(country);
            Ee.SaveChanges();
        }

        public void UpdateCountry(Country country)
        {
            Country data = Ee.Countries.FirstOrDefault(x => x.CountryId == country.CountryId);
            if (data != null)
            {
                data.Country1 = country.Country1;
            }
            Ee.SaveChanges();
        }

        public IEnumerable<Country> GetCountryList()
        {
            return Ee.Countries.ToList();
        }


        //state
        public void AddState(State state)
        {
            Ee.States.Add(state);
            Ee.SaveChanges();
        }

        public void UpdateState(State state)
        {
            State data = Ee.States.FirstOrDefault(x => x.StateId == state.StateId);
            if (data != null)
            {
                data.State1= state.State1;
                data.CountryId = state.CountryId;
            }
            Ee.SaveChanges();
        }

        public IEnumerable<State> GetStateList()
        {
            return Ee.States.ToList();
        }

        public IEnumerable<State> GetStateListByCountryId(int cid)
        {
            return Ee.States.Where(x=>x.CountryId==cid).ToList();
        }

        //city
        public void AddCity(City city)
        {
            Ee.Cities.Add(city);
            Ee.SaveChanges();
        }

        public void UpdateCity(City city)
        {
            City data = Ee.Cities.FirstOrDefault(x => x.CityId == city.CityId);
            if (data != null)
            {
                data.City1 = city.City1;
                data.StateId = city.StateId;
            }
            Ee.SaveChanges();
        }

        public IEnumerable<City> GetCityList()
        {
            return Ee.Cities.ToList();
        }

        public IEnumerable<City> GetCityListByStateId(int sid)
        {
            return Ee.Cities.Where(x => x.StateId == sid).ToList();
        }



    }
}
