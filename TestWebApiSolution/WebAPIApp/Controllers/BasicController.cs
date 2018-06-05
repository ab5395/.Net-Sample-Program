using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Data.API;
using Business.API;


namespace WebAPIApp.Controllers
{
    [RoutePrefix("Common")]
    public class BasicController : ApiController
    {
        public CommonDomain Cd=new CommonDomain();

        //Country
        [Route("GetCountry")]
        [HttpGet]
        public IEnumerable<Country> GetCountryList()
        {
            return Cd.GetCountryList();
        }

        [Route("AddCountry")]
        [AcceptVerbs("POST")]
        public bool AddCountry(Country country)
        {
            Cd.AddCountry(country);
            return true;
        }

        [Route("UpdateCountry")]
        [AcceptVerbs("POST")]
        public bool UpdateCountry(Country country)
        {
            Cd.UpdateCountry(country);
            return true;
        }

        //State
        [Route("GetState")]
        [HttpGet]
        public IEnumerable<State> GetStateList()
        {
            return Cd.GetStateList();
        }

        [Route("GetStateByCountryId")]
        [HttpGet]
        public IEnumerable<State> GetStateListByCountryId(int cid)
        {
            //return Cd.GetStateListByCountryId(int.Parse(cid));
            return Cd.GetStateListByCountryId(cid);
        }

        [Route("AddState")]
        [AcceptVerbs("POST")]
        public bool AddState(State state)
        {
            Cd.AddState(state);
            return true;
        }

        [Route("UpdateState")]
        [AcceptVerbs("POST")]
        public bool UpdateState(State state)
        {
            Cd.UpdateState(state);
            return true;
        }

        //State
        [Route("GetCity")]
        [HttpGet]
        public IEnumerable<City> GetCityList()
        {
            return Cd.GetCityList();
        }

        [Route("GetCityByStateId")]
        [HttpGet]
        public IEnumerable<City> GetCityListByStateId(int sid)
        {
            //return Cd.GetCityListByStateId(int.Parse(sid));
            return Cd.GetCityListByStateId(sid);
        }

        [Route("AddCity")]
        [AcceptVerbs("POST")]
        public bool AddCity(City city)
        {
            Cd.AddCity(city);
            return true;
        }

        [Route("UpdateCity")]
        [AcceptVerbs("POST")]
        public bool UpdateCity(City city)
        {
            Cd.UpdateCity(city);
            return true;
        }
    }
}
