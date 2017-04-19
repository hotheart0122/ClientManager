﻿using ClientManagerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientManagerApp.Controllers
{
    public class CaseController : Controller
    {
        private static ClientCaseRepositoryinMemory _caseRepo;

        public CaseController()
        {
            if (_caseRepo == null)
            {
                _caseRepo = new ClientCaseRepositoryinMemory();
            }
        }

        // GET: Case
        public ActionResult Index()
        {
            List<CaseViewModel> caseVmList = new List<CaseViewModel>();
            foreach (var cases in _caseRepo.GetCases())
            {
                caseVmList.Add(new CaseViewModel(cases));
            }
            
            return View(caseVmList);
                
        }

        // GET: Case/Details/5
        public ActionResult Details(int id)
        {
            var targetCase = _caseRepo.GetCaseById(id);
            CaseViewModel caseVm = new CaseViewModel(targetCase);
            return View(caseVm);
        }

        // GET: Case/Create
        public ActionResult Create(int clientId)
        { // to add new case to existing clients. we need to get the clientId, FName and LName from _caseRepo.
            var client = _caseRepo.GetClientById(clientId);
            CaseViewModel newCase = new CaseViewModel {  FName=client.FName, LName=client.LName, ClientId = client.ClientId };

            //to make a dropdown menu in the create and edit view
            ViewBag.statusList = new SelectList(_caseRepo.GetStatuses(), "CaseStatusId", "Status");
            return View(newCase);
        }

        //POST: Case/Create
       [HttpPost]
        public ActionResult Create(Case newCase, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                //add this after getting an error
                var client = _caseRepo.GetClientById(newCase.ClientId);
                newCase.Client = client;
                //compare to ClientController has only this: _clientRepo.AddClient(newClient);

                //[CaseStatus was returing null], so added these two lines.
                newCase.CaseStatus = _caseRepo.GetStatusById(newCase.CaseStatusId); //need to get CaseStatusId
                //because that is the one passed in
                //newCase.CaseStatusId = newCase.CaseStatus.CaseStatusId; no need any more because 
                //it already has CaseStatusId

                _caseRepo.AddCase(newCase);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.statusList = new SelectList(_caseRepo.GetStatuses(), "CaseStatusId", "Status");
                // CaseStatusId: DataValue, Status is DataText
                //ViewBag.statusList = new SelectList(_caseRepo.GetStatuses());
                return View();
            }
        }

        // GET: Case/Edit/5
        public ActionResult Edit(int id)
        {
            var targetCase = _caseRepo.GetCaseById(id);
            CaseViewModel caseVm = new CaseViewModel(targetCase);

            ViewBag.statusList = new SelectList(_caseRepo.GetStatuses(), "CaseStatusId", "Status");
            //ViewBag.statusList = new SelectList(_caseRepo.GetStatuses());
            return View(caseVm);
        }

        // POST: Case/Edit/5
        [HttpPost]
        public ActionResult Edit(Case updateCase, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                var client = _caseRepo.GetClientById(updateCase.ClientId); //having an error,  
                updateCase.Client = client; //fixed it by adding these two lines.

                //[CaseStatus was returing null], so added these two lines.
                updateCase.CaseStatus = _caseRepo.GetStatusById(updateCase.CaseStatusId); //need to get CaseStatusId
                //because that is the one passed in

                //updateCase.CaseStatusId = updateCase.CaseStatus.CaseStatusId; do not need it any more
                //

                _caseRepo.UpdateCase(updateCase);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.statusList = new SelectList(_caseRepo.GetStatuses(), "CaseStatusId", "Status");
                //ViewBag.statusList = new SelectList(_caseRepo.GetStatuses());
                return View();
            }
        }

        // GET: Case/Delete/5
        public ActionResult Delete(int id)
        {
            var targetCase = _caseRepo.GetCaseById(id);
            CaseViewModel caseVm = new CaseViewModel(targetCase);
            return View(caseVm);
        }

        // POST: Case/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                _caseRepo.DeleteCase(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult GetCaseByClientId(int clientId)
        {
            var targetCases = _caseRepo.GetCaseByClientId(clientId);
            List<CaseViewModel> CasesVm = new List<CaseViewModel>();
            //create new method in controller

            foreach (var caseItem in targetCases)
            {
                CasesVm.Add(new CaseViewModel(caseItem));
            }
            return View(CasesVm);
        }
    }
}
