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

            PopulatePetitionDropDownList(viewModel.PetitionSignatureSearchCriteriaModel, PetitionID);
            PopulatePetitionDetailDropDownList(viewModel.PetitionSignatureSearchCriteriaModel, PetitionDetailID);
            return this.View(viewModel);
        }
         
        [HttpPost]
        public ActionResult Search(PetitionSignaturesSearchViewModel model)
        {
            model.PetitionSignatureSearchResults = GetVoterResults(model.PetitionSignatureSearchCriteriaModel);
            PopulatePetitionDropDownList(model.PetitionSignatureSearchCriteriaModel, 0);
            PopulatePetitionDetailDropDownList(model.PetitionSignatureSearchCriteriaModel, 0);

            return this.View(model);
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


        private IEnumerable<PetitionSignatureSearch> GetVoterResults(PetitionSignatureSearchCriteriaModel criteria)
        {

            int PetitionID = criteria.PetitionID;
            int PeitionDetailID = criteria.PetitionDetailID;

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


            IEnumerable<PetitionSignatureSearch> data = db.Database.SqlQuery<PetitionSignatureSearch>(query, petitionIDParam, PetitionDetailIDParam, FirstNameParam, LastNameParam, HouseNumParam).ToList();



            return data;


        }

    }
}