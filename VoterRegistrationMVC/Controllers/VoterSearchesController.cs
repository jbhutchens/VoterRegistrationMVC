using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VoterRegistrationMVC.DAL;
using VoterRegistrationMVC.Models;

namespace VoterRegistrationMVC.Controllers
{
    public class VoterSearchesController : Controller
    {
        private VoterRegistrationContext db = new VoterRegistrationContext();

        // GET: VoterSearches
        public ActionResult Index()
        {
            PopulatePetitionDropDownList();
            string query = "EXEC sp_VoterSearch 0,0,'','',''";
            IEnumerable<VoterSearch> data = db.Database.SqlQuery<VoterSearch>(query);
            //var data = db.sp_VoterSearch(1).ToList(); // db.VoterSearch.ToList();
            return View(data.ToList());

            //return View(db.VoterSearch.ToList());
        }
         

        [HttpGet]
        public ActionResult WebGrid()
        {
            PopulatePetitionDropDownList();
            string query = "EXEC sp_VoterSearch 0,0,'','',''";
            IEnumerable<VoterSearch> data = db.Database.SqlQuery<VoterSearch>(query);
            //var data = db.sp_VoterSearch(1).ToList(); // db.VoterSearch.ToList();
            return View(data.ToList());
        }

   

        [HttpPost]
        public ActionResult WebGrid(FormCollection form)
        {
            PopulatePetitionDropDownList();

           //String PetitionID =  form["PetitionID"].ToString();

            int PetitionID = Convert.ToInt32(Request.Form["PetitionID"].ToString());
            int PeitionDetailID = 1;
            String FirstName = Request.Form["FirstName"].ToString();
            String LastName = Request.Form["LastName"].ToString();
            String HouseNum = Request.Form["HouseNumber"].ToString();

            
            string query = "EXEC dbo.sp_VoterSearch @PetitionID, @PetitionDetailID, @FirstName, @LastName, @HouseNum";
            SqlParameter petitionIDParam = new SqlParameter("PetitionID", PetitionID);
            SqlParameter PetitionDetailIDParam = new SqlParameter("PetitionDetailID", PeitionDetailID);
            SqlParameter FirstNameParam = new SqlParameter("FirstName", FirstName);
            SqlParameter LastNameParam = new SqlParameter("LastName", LastName);
            SqlParameter HouseNumParam = new SqlParameter("HouseNum", HouseNum);


            IEnumerable<VoterSearch> data = db.Database.SqlQuery<VoterSearch>(query, petitionIDParam, PetitionDetailIDParam, FirstNameParam, LastNameParam, HouseNumParam);
            //var data = db.sp_VoterSearch(1).ToList(); // db.VoterSearch.ToList();
            return View(data.ToList());
        }


        private void PopulatePetitionDropDownList(object selectedPetition = null)
        {
            var petitionQuery = from d in db.Petitions 
                             orderby d.PetitionName
                             select d;

            ViewBag.PetitionID = new SelectList(petitionQuery, "PetitionID", "PetitionName", selectedPetition);
        }

        /*
        private void PopulatePetitionDetailDropDownList(object selectPetitionDetail = null)
        {
            var petitionDetailQuery = from d in db.Petitions
                                orderby d.PetitionName
                                select d;

            ViewBag.PetitionID = new SelectList(petitionQuery, "PetitionID", "PetitionName", selectedTeams);
        }*/

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
