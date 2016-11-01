using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VoterRegistrationMVC.DAL;
using VoterRegistrationMVC.Models;

namespace VoterRegistrationMVC.Controllers
{
    public class PetitionDetailsController : Controller
    {
        private VoterRegistrationContext db = new VoterRegistrationContext();

        // GET: PetitionDetails
        public ActionResult Index()
        {
            return View(db.PetitionDetails.ToList());
        }

        // GET: PetitionDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetitionDetails petitionDetails = db.PetitionDetails.Find(id);
            if (petitionDetails == null)
            {
                return HttpNotFound();
            }
            return View(petitionDetails);
        }

        // GET: PetitionDetails/Create
        public ActionResult Create()
        {
            PetitionDetailsViewModel viewModel = new PetitionDetailsViewModel();
            PopulatePetitionDropDownList(viewModel);
            if (viewModel.PetitionIDValues.First().Value != null)
                viewModel.PetitionID = Convert.ToInt32(viewModel.PetitionIDValues.First().Value);
            else
                viewModel.PetitionID = 1;

            return View(viewModel);
        }

        // POST: PetitionDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PetitionID,PetitionDetailID,PersonAssigned")] PetitionDetails petitionDetails)
        {
            if (ModelState.IsValid)
            {
                db.PetitionDetails.Add(petitionDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(petitionDetails);
        }

        // GET: PetitionDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetitionDetails petitionDetails = db.PetitionDetails.Find(id);
            if (petitionDetails == null)
            {
                return HttpNotFound();
            }
            return View(petitionDetails);
        }

        // POST: PetitionDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PetitionID,PetitionDetailID,PersonAssigned")] PetitionDetails petitionDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(petitionDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(petitionDetails);
        }

        // GET: PetitionDetails/Delete/5
        public ActionResult Delete(int PetitionId, int PetitionDetailID)
        {
        
            PetitionDetails petitionDetails = db.PetitionDetails.Find(PetitionId, PetitionDetailID);
            if (petitionDetails == null)
            {
                return HttpNotFound();
            }
            return View(petitionDetails);
        }

        // POST: PetitionDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int PetitionID, int PetitionDetailID)
        {
            PetitionDetails petitionDetails = db.PetitionDetails.Find(PetitionID, PetitionDetailID);
            db.PetitionDetails.Remove(petitionDetails);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void PopulatePetitionDropDownList(PetitionDetailsViewModel viewModel)
        {

            viewModel.PetitionIDValues =
            db.Petitions.Select(h =>
                                new SelectListItem
                                { Text = h.PetitionName, Value = h.PetitionID.ToString() }
                               ).ToList();

         
            //SelectList myList = GetMySelectList();
            //SelectListItem selected = myList.FirstOrDefault(x => x.Text.ToUpper().Contains("UNITED STATES"));
            //if (selected != null)
            //{
            //    myList = new SelectList(myList, "value", "text", selected.Value);
            //}

        }

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
