using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoterRegistrationMVC.DAL;
using VoterRegistrationMVC.Models;

namespace VoterRegistrationMVC.Controllers
{
    [RoutePrefix("VoterSearchController")]
    public class VoterSearchController : Controller
    {
        private VoterRegistrationContext db = new VoterRegistrationContext();
        

        // GET: VoterSearch
        public ActionResult Index()
        {
            return View();
        }


        //Get action for main search, intro to the whole show!
        public ActionResult Search(int PetitionID = 0, int PetitionDetailID = 0)
        {
             VoterSearchesViewModel viewModel = new VoterSearchesViewModel();
                // make sure we don't get a null reference exceptions
                viewModel.VoterSearchCriteriaModel = new VoterSearchCriteriaModel();
                viewModel.VoterSearchResults = new List<VoterSearch>();
                
                //retain the petition ID if we know it being passed thru a param, forget why now, on add I think
                if (PetitionID != 0)
                {
                    viewModel.VoterSearchCriteriaModel.PetitionID = PetitionID;
                }
                PopulatePetitionDropDownList(viewModel.VoterSearchCriteriaModel, PetitionID);
                PopulatePetitionDetailDropDownList(viewModel.VoterSearchCriteriaModel, PetitionDetailID);

            //Set Total Rows so pagination works
            ViewBag.TotalRows = viewModel.VoterSearchResults.Select(q => q.totalRows).FirstOrDefault();
            if (ViewBag.TotalRows == null)
                ViewBag.TotalRows = 0;

            //tells us which little page number to start on and highlight, annoying redundancy but another day, another time.
            ViewBag.CurrentPage = 1;
            viewModel.VoterSearchCriteriaModel.Page = 1;

            return this.View(viewModel);

            
                
        }

        //comes here after entering found voter
        [Route("SearchAfterFound")]
        public ActionResult SearchAfterFound(int PetitionID = 0, int PetitionDetailID = 0)
        {
            VoterSearchesViewModel viewModel = new VoterSearchesViewModel();
            // make sure we don't get a null reference exceptions
            viewModel.VoterSearchCriteriaModel = new VoterSearchCriteriaModel();
            viewModel.VoterSearchResults = new List<VoterSearch>();

            PopulatePetitionDropDownList(viewModel.VoterSearchCriteriaModel, PetitionID);
            PopulatePetitionDetailDropDownList(viewModel.VoterSearchCriteriaModel, PetitionDetailID);
            return View("~/Views/VoterSearch/Search.cshtml");
        }


        [HttpPost]
        public ActionResult PetitionChange(int PetitionID = 0)
        {
            VoterSearchesViewModel viewModel = new VoterSearchesViewModel();
            // make sure we don't get a null reference exceptions
            viewModel.VoterSearchCriteriaModel = new VoterSearchCriteriaModel();
            if (PetitionID != 0)
            {
                viewModel.VoterSearchCriteriaModel.PetitionID = PetitionID;
            }
            
            PopulatePetitionDetailDropDownList(viewModel.VoterSearchCriteriaModel, 0);
            
            return Json(viewModel.VoterSearchCriteriaModel.PetitionDetailValues);

        }


        //After we submit the form, or if we click on a pagination button -- javascript switches it from get to post.
        [HttpPost]
        public ActionResult Search(VoterSearchesViewModel model)
        {
            int page = 0;
            page = model.VoterSearchCriteriaModel.Page ?? 0;



            model.VoterSearchResults = GetVoterResults(model.VoterSearchCriteriaModel, page);
            PopulatePetitionDropDownList(model.VoterSearchCriteriaModel, 0);
            PopulatePetitionDetailDropDownList(model.VoterSearchCriteriaModel, 0);
             

            ViewBag.TotalRows = model.VoterSearchResults.Select(q => q.totalRows).FirstOrDefault();
            if (ViewBag.TotalRows == null)
                ViewBag.TotalRows = 0;

            ViewBag.CurrentPage = page;

            return View(model);
        }


        [Route("AddVoter")]
        public ActionResult AddVoter(string VoterID, int PetitionID, int PetitionDetailID)
        {
           
            string query = "EXEC dbo.sp_VoterPetitionAdd @VoterID, @PetitionID, @PetitionDetailID";
            SqlParameter petitionIDParam = new SqlParameter("PetitionID", PetitionID);
            SqlParameter PetitionDetailIDParam = new SqlParameter("PetitionDetailID", PetitionDetailID);
            SqlParameter VoterIDParam = new SqlParameter("VoterID", VoterID); 

            //add the voter
            db.Database.ExecuteSqlCommand(query, VoterIDParam, petitionIDParam, PetitionDetailIDParam);

            string var = Request.UrlReferrer.ToString();
            //return Redirect(Request.UrlReferrer.ToString());
            return RedirectToAction("Search", new { PetitionID = PetitionID, PetitionDetailID = PetitionDetailID });
        }


        private void PopulatePetitionDropDownList(VoterSearchCriteriaModel criteria, int SelectedValue)
        {

            criteria.Petitions = 
            db.Petitions.Select(h => 
                                new SelectListItem
                                { Text = h.PetitionName, Value = h.PetitionID.ToString() }
                               ).ToList();

            if (SelectedValue != 0)
            {
                SelectListItem selected = criteria.Petitions.FirstOrDefault(x => x.Value.Equals(SelectedValue));
                if (selected != null)
                {
                    criteria.Petitions = new SelectList(criteria.Petitions, "value", "text", selected.Value);
                } 
            } 

        }

        private void PopulatePetitionDetailDropDownList(VoterSearchCriteriaModel criteria, int SelectedValue)
        {
            int _PetitionID = criteria.PetitionID;
            if (_PetitionID == 0)
            {
                _PetitionID = 1;
            }



            if (SelectedValue != 0)
            {
                criteria.PetitionDetailValues =
                db.PetitionDetails.Where(X => X.PetitionID == _PetitionID).Select(h =>
                                new SelectListItem
                                { Text = h.PetitionDetailID.ToString() + "-" + h.PersonAssigned, Value = h.PetitionDetailID.ToString(), Selected = h.PetitionDetailID.Equals(SelectedValue) }
                               ).ToList();
                criteria.PetitionDetailID = SelectedValue;
            }
            else
            {
                criteria.PetitionDetailValues =
                    db.PetitionDetails.Where(X => X.PetitionID == _PetitionID).Select(h =>
                                new SelectListItem
                                { Text = h.PetitionDetailID.ToString() + "-" + h.PersonAssigned, Value = h.PetitionDetailID.ToString() }
                               ).ToList();
            }
        }


        private IEnumerable<VoterSearch> GetVoterResults(VoterSearchCriteriaModel criteria, int page)
        {

            int PetitionID = criteria.PetitionID;
            int PeitionDetailID = criteria.PetitionDetailID;
            int startNum = (page * 25)+1;
            int endNum = startNum + (25-1);
            criteria.Page = 1;

            String FirstName = String.Empty;
            if (criteria.FirstName != null)
                FirstName = criteria.FirstName;
            else
            {
                FirstName = String.Empty;
            }

            String LastName = String.Empty;
            if (criteria.LastName != null)
                LastName = criteria.LastName;
            else
            {
                LastName = String.Empty;
            }

            String HouseNum = String.Empty;
            if (criteria.HouseNumber != null)
                HouseNum = criteria.HouseNumber;
            else
            {
                HouseNum = String.Empty;
            }
             

            string query = "EXEC dbo.sp_VoterSearch @PetitionID, @PetitionDetailID, @FirstName, @LastName, @HouseNum, @startNum, @endNum";
            SqlParameter petitionIDParam = new SqlParameter("PetitionID", PetitionID);
            SqlParameter PetitionDetailIDParam = new SqlParameter("PetitionDetailID", PeitionDetailID);
            SqlParameter FirstNameParam = new SqlParameter("FirstName", FirstName);
            SqlParameter LastNameParam = new SqlParameter("LastName", LastName);
            SqlParameter HouseNumParam = new SqlParameter("HouseNum", HouseNum);
            SqlParameter startNumParam = new SqlParameter("startNum", startNum);
            SqlParameter endNumParam = new SqlParameter("endNum", endNum);


            IEnumerable<VoterSearch> data = db.Database.SqlQuery<VoterSearch>(query, petitionIDParam, PetitionDetailIDParam, FirstNameParam, LastNameParam, HouseNumParam, startNumParam, endNumParam).ToList();

            //we dont get all the rows, we just get what we need and let the query tell us how many total if we were to get all of them
            //less data less overhead
            if (data.Count() != 0)
            { 
                ViewBag.TotalRows = data.Select(x => x.totalRows).First();
            }


            if (ViewBag.TotalRows == null)
            {
                ViewBag.TotalRows = 0;
            }

            return data;


        }


    }
}