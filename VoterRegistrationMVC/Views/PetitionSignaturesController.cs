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

namespace VoterRegistrationMVC.Views
{
    public class PetitionSignaturesController : Controller
    {
        private VoterRegistrationContext db = new VoterRegistrationContext();

        // GET: PetitionSignatures
        public ActionResult Index()
        {
            return View(db.PetitionSignatures.ToList());
        }

        // GET: PetitionSignatures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetitionSignatures petitionSignatures = db.PetitionSignatures.Find(id);
            if (petitionSignatures == null)
            {
                return HttpNotFound();
            }
            return View(petitionSignatures);
        }

        // GET: PetitionSignatures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PetitionSignatures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PetitionID,PetitionDetailID,LineNumber,VoteFileID,VoterID")] PetitionSignatures petitionSignatures)
        {
            if (ModelState.IsValid)
            {
                db.PetitionSignatures.Add(petitionSignatures);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(petitionSignatures);
        }

        // GET: PetitionSignatures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetitionSignatures petitionSignatures = db.PetitionSignatures.Find(id);
            if (petitionSignatures == null)
            {
                return HttpNotFound();
            }
            return View(petitionSignatures);
        }

        // POST: PetitionSignatures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PetitionID,PetitionDetailID,LineNumber,VoteFileID,VoterID")] PetitionSignatures petitionSignatures)
        {
            if (ModelState.IsValid)
            {
                db.Entry(petitionSignatures).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(petitionSignatures);
        }

        // GET: PetitionSignatures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetitionSignatures petitionSignatures = db.PetitionSignatures.Find(id);
            if (petitionSignatures == null)
            {
                return HttpNotFound();
            }
            return View(petitionSignatures);
        }

        // POST: PetitionSignatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PetitionSignatures petitionSignatures = db.PetitionSignatures.Find(id);
            db.PetitionSignatures.Remove(petitionSignatures);
            db.SaveChanges();
            return RedirectToAction("Index");
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
