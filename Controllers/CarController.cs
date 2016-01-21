using CarFinder.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Bing;


namespace CarFinder.Controllers
{
    public class CarController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public class Selected
        {
            public string year { get; set; }
            public string make { get; set; }
            public string model { get; set; }
            public string trim { get; set; }
        }

        /// <summary>
        /// Method for Stored Procedure call to get Cars by Year, Make, Model, and Trim
        /// </summary>
        /// <param name="year">Parameter for year of cars</param>
        /// <param name="make">Parameter for make of cars</param>
        /// <param name="model">Parameter for model of cars</param>
        /// <param name="trim">Parameter for trim of cars</param>
        /// <returns>All cars by year, make, model, and trim</returns>
        /// <example>SQL Statement
        /// <code>
        /// select * from dbo.Cars 
        /// where (
        /// (@model_year is null or @model_year ='' or model_year = @model_year) and
        /// (@make is null or @make ='' or make = @make) and
        /// (@model_name is null or @model_name ='' or model_name = @model_name) and
        /// (@model_trim is null or @model_trim ='' or model_trim = @model_trim))
        /// </code>
        /// </example>
        [HttpPost]
        public IHttpActionResult GetCarsByYearMakeModelTrim(Selected selected)
        {
            // Input Parameters
            var Syear = new SqlParameter("@year", selected.year??"");
            var Smake = new SqlParameter("@make", selected.make??"");
            var Smodel = new SqlParameter("@model", selected.model??"");
            var Strim = new SqlParameter("@trim", selected.trim??"");
            var retval = db.Database.SqlQuery<Car>(
                // Run Stored Procedure
                "exec getCarsByYearMakeModelTrim @year, @make, @model, @trim",
                Syear, Smake, Smodel, Strim).ToList();

            return Ok(retval);
        }

        /// <summary>
        /// Method for Stored Procedure call to get all distinct years of cars
        /// </summary>
        /// <returns>All years of cars in database</returns>
        /// <example>SQL Statement
        /// <code>
        /// select * distinct years from dbo.Cars 
        /// order by years desc
        /// </code>
        /// </example>
        [HttpPost]
        public IHttpActionResult GetYears()
        {
            var retval = db.Database.SqlQuery<string>(
                // Run Stored Procedure
                "exec getYears").ToList();
            // Return Value
            return Ok(retval);
        }

        /// <summary>
        /// Method for Stored Procedure call to get all models of cars for specfic year
        /// </summary>
        /// <param name="year">Parameter for year</param>
        /// <returns>All makes of cars for specific year chosen</returns>
        /// <text>SQL Statement
        /// <xml>
        /// select * distinct years from dbo.Cars 
        /// select distinct make from dbo.Cars
        /// where model_year = @model_year
        /// order by make desc;
        /// </xml>
        /// </text>
        [HttpPost]
        public IHttpActionResult GetMakeByYear(Selected selected)
        {
            // Input Parameters
            var Syear = new SqlParameter("@year", selected.year);
            var retval = db.Database.SqlQuery<string>(
                // Run Stored Procedure
                "exec getMakeByYear @year",
                Syear).ToList();
            // Return Value
            return Ok(retval);
        }

        /// <summary>
        /// Method for Stored Procedure call to get all models of car by year and make
        /// </summary>
        /// <param name="year">Parameter for year</param>
        /// <param name="make">Parameter for make</param>
        /// <returns>Cars models by year and make</returns>
        /// <example>SQL Statement
        /// <code>
        /// select distinct model_name from dbo.Cars
        /// where model_year = @model_year and make = @make
        /// order by model_name asc;
        /// </code>
        /// </example>
        [HttpPost]
        public IHttpActionResult GetModelByYearMake(Selected selected)
        {
            // Input Parameters
            var Syear = new SqlParameter("@year", selected.year);
            var Smake = new SqlParameter("@make", selected.make);
            var retval = db.Database.SqlQuery<string>(
                // Run Stored Procedure
                "exec getModelByYearMake @year, @make",
                Smake, Syear).ToList();
            // Return Value
            return Ok(retval);
        }

        /// <summary>
        /// Method for Stored Procedure call to get cars trim by user chosen year, make, and model
        /// </summary>
        /// <param name="year">Parameter for year</param>
        /// <param name="make">Parameter for make</param>
        /// <param name="model">Parameter for model</param>
        /// <returns>Cars trim by year, make, and model </returns>
        /// <example>SQL Statement
        /// <code>
        /// select distinct model_trim from dbo.Cars
        /// where model_trim != '' and model_trim is not null and model_year = @model_year 
        /// and make = @make and model_name = @model_name
        /// order by model_trim asc
        /// </code>
        /// </example>
        [HttpPost]
        public IHttpActionResult GetTrimByYearMakeModel(Selected selected)
        {
            // Input Parameters
            var Syear = new SqlParameter("@year", selected.year);
            var Smake = new SqlParameter("@make", selected.make);
            var Smodel = new SqlParameter("@model", selected.model);
            var retval = db.Database.SqlQuery<string>(
                // Run Stored Procedure
                "exec getTrimByYearMakeModel @year, @make, @model",
                Smake, Syear, Smodel).ToList();
            // Return Value
            return Ok(retval);
        }


        // Third Party API's
        /// <summary>
        /// Used to access the NHTSA API for vehicle information including recalls as well as Bing API to get URL's for images of specific vehicles.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Car images as well as detailed information, including recalls, for specific car</returns>
        [HttpPost]
        public async Task<IHttpActionResult> getCar(int Id)
        {
            HttpResponseMessage response;
            string content = "";
            var Car = db.Cars.Find(Id);
            var Recalls = "";
            var Image = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://www.nhtsa.gov/");
                try
                {
                    response = await client.GetAsync("webapi/api/Recalls/vehicle/modelyear/" + Car.model_year +
                                                                                    "/make/" + Car.make +
                                                                                    "/model/" + Car.model_name + "?format=json");
                    content = await response.Content.ReadAsStringAsync();
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
            }
            Recalls = content;

            // Bing Search API
            var image = new BingSearchContainer(new Uri("https://api.datamarket.azure.com/Bing/search/v1/Image"));

            image.Credentials = new NetworkCredential("accountKey", "s8cUIpKPWJ609E7VqtBbx9HNdRp9Z2NbUne/HyPgxbQ");
            var marketData = image.Composite (
                "image",
                Car.model_year + " " + Car.make + " " + Car.model_name + " " + Car.model_trim + " " + "NOT ebay",
                null,null,null,null,null,null,null,null,null,null,null,null,null).Execute();

            Image = marketData.First().Image.First().MediaUrl;

            return Ok(new { car = Car, recalls = Recalls, image = Image });
        }
    }
}
