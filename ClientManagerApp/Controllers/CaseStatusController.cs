using ClientManagerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientManagerApp.Controllers
{
    public class CaseStatusController : Controller
    {
        private static ClientCaseRepositoryinMemory _statusRepo;

        public CaseStatusController()
        {
            if (_statusRepo == null)
            {
                _statusRepo = new ClientCaseRepositoryinMemory();
            }
        }

        // GET: CaseStatus
        public ActionResult Index()
        {
            return View(_statusRepo.GetStatuses());
        }

        // GET: CaseStatus/Details/5
        public ActionResult Details(int id)
        {
            return View(_statusRepo.GetStatusById(id));
        }

        // GET: CaseStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CaseStatus/Create
        [HttpPost]
        public ActionResult Create(CaseStatus newStatus, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                _statusRepo.AddStatus(newStatus);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CaseStatus/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_statusRepo.GetStatusById(id));
        }

        // POST: CaseStatus/Edit/5
        [HttpPost]
        public ActionResult Edit(CaseStatus editStatus, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                _statusRepo.UpdateStatus(editStatus);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CaseStatus/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_statusRepo.GetStatusById(id));
        }

        // POST: CaseStatus/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                _statusRepo.DeleteStatus(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
