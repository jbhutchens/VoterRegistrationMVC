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
    [RoutePrefix("PetitionSignatureController")]
    public class PetitionSignatureSearchController : Controller
    {

        private VoterRegistrationContext db = new VoterRegistrationContext();

        // GET: PetitionSignatureSearch
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(int PetitionID = 0, int PetitionDetailID = 0)
        {
           PetitionSignaturesSearchViewModel viewModel = new PetitionSignaturesSearchViewModel();
            // make sure we don't get a null reference exceptions
            viewModel.PetitionSignatureSearchCriteriaModel = new PetitionSignatureSearchCriteriaModel();
            viewModel.PetitionSignatureSearchResults = new List<PetitionSignatureSearch>();

            //retain the petition ID if we know it being passed thru a param, forget why now, on add I think
            if (PetitionID != 0)
            {
                viewModel.PetitionSignatureSearchCriteriaModel.PetitionID = PetitionID;
            }
            PopulatePetitionDropDownList(viewModel.PetitionSignatureSearchCriteriaModel, PetitionID);
            PopulatePetitionDetailDropDownList(viewModel.PetitionSignatureSearchCriteriaModel, PetitionDetailID);

            //Set Total Rows so pagination works
            ViewBag.TotalRows = viewModel.PetitionSignatureSearchResults.Select(q => q.totalRows).FirstOrDefault();
            if (ViewBag.TotalRows == null)
                ViewBag.TotalRows = 0;

            //tells us which little page number to start on and highlight, annoying redundancy but another day, another time.
            ViewBag.CurrentPage = 1;
            viewModel.PetitionSignatureSearchCriteriaModel.Page = 1;

            return this.View(viewModel);
        }

        [HttpPost]
        public ActionResult Search(PetitionSignaturesSearchViewModel model)
        {
            int page = 0;
            page = model.PetitionSignatureSearchCriteriaModel.Page ?? 0;



            model.PetitionSignatureSearchResults = GetVoterResults(model.PetitionSignatureSearchCriteriaModel, page);
            PopulatePetitionDropDownList(model.PetitionSignatureSearchCriteriaModel, 0);
            PopulatePetitionDetailDropDownList(model.PetitionSignatureSearchCriteriaModel, 0);


            ViewBag.TotalRows = model.PetitionSignatureSearchResults.Select(q => q.totalRows).FirstOrDefault();
            if (ViewBag.TotalRows == null)
                ViewBag.TotalRows = 0;

            ViewBag.CurrentPage = page;

            return View(model);
        }


        [Route("RemoveVoter")]
        public ActionResult RemoveVoter(string VoterID, int PetitionID, int PetitionDetailID)
        {

            string query = "EXEC dbo.sp_VoterPetitionRemove @VoterID, @PetitionID, @PetitionDetailID";
            SqlParameter petitionIDParam = new SqlParameter("PetitionID", PetitionID);
            SqlParameter PetitionDetailIDParam = new SqlParameter("PetitionDetailID", PetitionDetailID);
            SqlParameter VoterIDParam = new SqlParameter("VoterID", VoterID);

            //add the voter
            db.Database.ExecuteSqlCommand(query, VoterIDParam, petitionIDParam, PetitionDetailIDParam);

            string var = Request.UrlReferrer.ToString();
            //return Redirect(Request.UrlReferrer.ToString());
            return RedirectToAction("Search", new { PetitionID = PetitionID, PetitionDetailID = PetitionDetailID });
        }






        private void PopulatePetitionDropDownList(PetitionSignatureSearchCriteriaModel criteria, int SelectedValue)
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
            //SelectList myList = GetMySelectList();
            //SelectListItem selected = myList.FirstOrDefault(x => x.Text.ToUpper().Contains("UNITED STATES"));
            //if (selected != null)
            //{
            //    myList = new SelectList(myList, "value", "text", selected.Value);
            //}

        }

        [HttpPost]
        public ActionResult PetitionChange(int PetitionID = 0)
        {
            PetitionSignaturesSearchViewModel viewModel = new PetitionSignaturesSearchViewModel();
            // make sure we don't get a null reference exceptions
            viewModel.PetitionSignatureSearchCriteriaModel = new PetitionSignatureSearchCriteriaModel();
            if (PetitionID != 0)
            {
                viewModel.PetitionSignatureSearchCriteriaModel.PetitionID = PetitionID;
            }

            PopulatePetitionDetailDropDownList(viewModel.PetitionSignatureSearchCriteriaModel, 0);

            return Json(viewModel.PetitionSignatureSearchCriteriaModel.PetitionDetailValues);

        }



        private void PopulatePetitionDetailDropDownList(PetitionSignatureSearchCriteriaModel criteria, int SelectedValue)
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


        private IEnumerable<PetitionSignatureSearch> GetVoterResults(PetitionSignatureSearchCriteriaModel criteria, int page)
        {

            int PetitionID = criteria.PetitionID;
            int PeitionDetailID = criteria.PetitionDetailID;
            int startNum = (page * 25) + 1;
            int endNum = startNum + (25 - 1);
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


            string query = "EXEC dbo.[sp_ReturnAssignedVoters] @PetitionID, @PetitionDetailID, @FirstName, @LastName, @HouseNum";
            SqlParameter petitionIDParam = new SqlParameter("PetitionID", PetitionID);
            SqlParameter PetitionDetailIDParam = new SqlParameter("PetitionDetailID", PeitionDetailID);
            SqlParameter FirstNameParam = new SqlParameter("FirstName", FirstName);
            SqlParameter LastNameParam = new SqlParameter("LastName", LastName);
            SqlParameter HouseNumParam = new SqlParameter("HouseNum", HouseNum);
            SqlParameter startNumParam = new SqlParameter("startNum", startNum);
            SqlParameter endNumParam = new SqlParameter("endNum", endNum);


            IEnumerable<PetitionSignatureSearch> data = db.Database.SqlQuery<PetitionSignatureSearch>(query, petitionIDParam, PetitionDetailIDParam, FirstNameParam, LastNameParam, HouseNumParam, startNumParam, endNumParam).ToList();

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