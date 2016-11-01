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
    public class VoterMergeFileDetailsController : Controller
    {
        private VoterRegistrationContext db = new VoterRegistrationContext();

        // GET: VoterMergeFileDetails
        public ActionResult Index()
        {
            return View(db.VoterMergeFileDetails.ToList());
        }

        // GET: VoterMergeFileDetails/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoterMergeFileDetails voterMergeFileDetails = db.VoterMergeFileDetails.Find(id);
            if (voterMergeFileDetails == null)
            {
                return HttpNotFound();
            }
            return View(voterMergeFileDetails);
        }

        // GET: VoterMergeFileDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VoterMergeFileDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VoterFileID,DateMerged,JeffcoDate,AdamsCountyDate")] VoterMergeFileDetails voterMergeFileDetails)
        {
            if (ModelState.IsValid)
            {
                db.VoterMergeFileDetails.Add(voterMergeFileDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(voterMergeFileDetails);
        }

        // GET: VoterMergeFileDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoterMergeFileDetails voterMergeFileDetails = db.VoterMergeFileDetails.Find(id);
            if (voterMergeFileDetails == null)
            {
                return HttpNotFound();
            }
            return View(voterMergeFileDetails);
        }

        // POST: VoterMergeFileDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VoterFileID,DateMerged,JeffcoDate,AdamsCountyDate")] VoterMergeFileDetails voterMergeFileDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voterMergeFileDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(voterMergeFileDetails);
        }

        // GET: VoterMergeFileDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoterMergeFileDetails voterMergeFileDetails = db.VoterMergeFileDetails.Find(id);
            if (voterMergeFileDetails == null)
            {
                return HttpNotFound();
            }
            return View(voterMergeFileDetails);
        }

        // POST: VoterMergeFileDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            VoterMergeFileDetails voterMergeFileDetails = db.VoterMergeFileDetails.Find(id);
            db.VoterMergeFileDetails.Remove(voterMergeFileDetails);
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
