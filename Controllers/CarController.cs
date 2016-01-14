using CarFinder.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarFinder.Controllers
{
    public class CarController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Method for Stored Procedure for getCarsByYearMakeModelTrim
        public IHttpActionResult GetCarsByYear(string carsbyyear, string carsbymake, string carsbymodel, string carsbytrim)
        {
            var Scarsbyyear = new SqlParameter("@carsbyyear", carsbyyear);
            var Scarsbymake = new SqlParameter("@carsbymake", carsbymake);
            var Scarsbymodel = new SqlParameter("@carsbymodel", carsbymodel);
            var Scarsbytrim = new SqlParameter("@carsbytrim", carsbytrim);
            var retval = db.Database.SqlQuery<string>(
                "exec getCarsByYearMakeModelTrim @carsbyyear, @carsbymake, @carsbymodel, @carsbytrim",
                Scarsbyyear, Scarsbymake, Scarsbymodel, Scarsbytrim).ToList();

            return Ok(retval);
        }

        // Method for Stored Procedure for getYears
        public IHttpActionResult GetYears()
        {
            var retval = db.Database.SqlQuery<string>(
                "exec getYears").ToList();

            return Ok(retval);
        }

        // Method for Stored Procedure for getMakeByYear
        public IHttpActionResult GetMakeByYear(string makebyyear)
        {
            var retval = db.Database.SqlQuery<string>(
                "exec getMakeByYear @model_year",
                new SqlParameter("@model_year", makebyyear)).ToList();

            return Ok(retval);
        }

        // Method for Stored Procedure for getModelByYearMake
        public IHttpActionResult GetModelByYearMake(string modelbyyear, string modelbymake)
        {
            var Smodelbyyear = new SqlParameter("@modelbyyear", modelbyyear);
            var Smodelbymake = new SqlParameter("@modelbymake", modelbymake);
            var retval = db.Database.SqlQuery<string>(
                "exec getModelByYearMake @modelbyyear, @modelbymake",
                Smodelbymake, Smodelbyyear).ToList();

            return Ok(retval);
        }

        // Method for Stored Procedure for getTrimByYearMakeModel
        public IHttpActionResult GetModelByYearMake(string trimbyyear, string trimbymake, string trimbymodel)
        {
            var Strimbyyear = new SqlParameter("@trimbyyear", trimbyyear);
            var Strimbymake = new SqlParameter("@trimbymake", trimbymake);
            var Strimbymodel = new SqlParameter("@trimbymodel", trimbymodel);
            var retval = db.Database.SqlQuery<string>(
                "exec getTrimByYearMakeModel @trimbyyear, @trimbymake, @trimbymodel",
                Strimbymake, Strimbyyear, Strimbymodel).ToList();

            return Ok(retval);
        }
    }
}
