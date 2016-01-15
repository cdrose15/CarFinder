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

        /// <summary>
        /// Method for Stored Procedure call to get Cars by Year, Make, Model, and Trim
        /// </summary>
        /// <param name="carsbyyear">Parameter for year of cars</param>
        /// <param name="carsbymake">Parameter for make of cars</param>
        /// <param name="carsbymodel">Parameter for model of cars</param>
        /// <param name="carsbytrim">Parameter for trim of cars</param>
        /// <returns>All cars by year, make, model, and trim</returns>
        /// 
        public IHttpActionResult GetCarsByYearMakeModelTrim(string carsbyyear, string carsbymake, string carsbymodel, string carsbytrim)
        {
            // Input Parameters
            var Scarsbyyear = new SqlParameter("@carsbyyear", carsbyyear);
            var Scarsbymake = new SqlParameter("@carsbymake", carsbymake);
            var Scarsbymodel = new SqlParameter("@carsbymodel", carsbymodel);
            var Scarsbytrim = new SqlParameter("@carsbytrim", carsbytrim);
            var retval = db.Database.SqlQuery<string>(
                // Run Stored Procedure
                "exec getCarsByYearMakeModelTrim @carsbyyear, @carsbymake, @carsbymodel, @carsbytrim",
                Scarsbyyear, Scarsbymake, Scarsbymodel, Scarsbytrim).ToList();

            return Ok(retval);
        }

        /// <summary>
        /// Method for Stored Procedure call to get all distinct years of cars
        /// </summary>
        /// <returns>All years of cars in database</returns>
        ///
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
        /// <param name="makebyyear">Parameter for year</param>
        /// <returns>All makes of cars for specific year chosen</returns>
        ///
        public IHttpActionResult GetMakeByYear(string makebyyear)
        {
            var retval = db.Database.SqlQuery<string>(
                // Run Stored Procedure
                "exec getMakeByYear @model_year",
                // Input Parameter
                new SqlParameter("@model_year", makebyyear)).ToList();
            // Return Value
            return Ok(retval);
        }

        /// <summary>
        /// Method for Stored Procedure call to get all models of car by year and make
        /// </summary>
        /// <param name="modelbyyear">Parameter for year</param>
        /// <param name="modelbymake">Parameter for make</param>
        /// <returns>Cars models by year and make</returns>
        ///
        public IHttpActionResult GetModelByYearMake(string modelbyyear, string modelbymake)
        {
            // Input Parameters
            var Smodelbyyear = new SqlParameter("@modelbyyear", modelbyyear);
            var Smodelbymake = new SqlParameter("@modelbymake", modelbymake);
            var retval = db.Database.SqlQuery<string>(
                // Run Stored Procedure
                "exec getModelByYearMake @modelbyyear, @modelbymake",
                Smodelbymake, Smodelbyyear).ToList();
            // Return Value
            return Ok(retval);
        }

        /// <summary>
        /// Method for Stored Procedure call to get cars trim by user chosen year, make, and model
        /// </summary>
        /// <param name="trimbyyear">Parameter for year</param>
        /// <param name="trimbymake">Parameter for make</param>
        /// <param name="trimbymodel">Parameter for model</param>
        /// <returns>Cars trim by year, make, and model </returns>
        ///
        public IHttpActionResult GetTrimByYearMakeModel(string trimbyyear, string trimbymake, string trimbymodel)
        {
            // Input Parameters
            var Strimbyyear = new SqlParameter("@trimbyyear", trimbyyear);
            var Strimbymake = new SqlParameter("@trimbymake", trimbymake);
            var Strimbymodel = new SqlParameter("@trimbymodel", trimbymodel);
            var retval = db.Database.SqlQuery<string>(
                // Run Stored Procedure
                "exec getTrimByYearMakeModel @trimbyyear, @trimbymake, @trimbymodel",
                Strimbymake, Strimbyyear, Strimbymodel).ToList();
            // Return Value
            return Ok(retval);
        }


        // Third Party API's
        /// <summary>
        /// Used to conncect to Bing search API for images and NHTSA API for recall information
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Car images as well as recall information for specific car</returns>
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
                    response = await client.GetAsync("webapi/api/Recalls/vehicle/modelyear/" + Car.model_name +
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

            var image = new BingSearchContainer(new Uri("https://api.datamarket.azure.com/Bing/search/"));

            image.Credentials = new NetworkCredential("accountKey", "s8cUIpKPWJ609E7VqtBbx9HNdRp9Z2NbUne/HyPgxbQ");
            var marketData = image.Composite(
                "image",
                Car.model_year + " " + Car.make + " " + Car.model_name + " " + Car.model_trim,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null
                ).Execute();

            Image = marketData.First().Image.First().MediaUrl;
            return Ok(new { car = Car, recalls = Recalls, image = Image });
        }
    }
}
