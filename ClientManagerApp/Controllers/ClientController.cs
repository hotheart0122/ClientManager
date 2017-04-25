using ClientManagerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientManagerApp.Controllers
{
    public class ClientController : Controller
    {//add these changes to swap to EFRepo
        private static IClientCaseRepository _clientRepo;

        public ClientController()
        {
            if (_clientRepo == null)
            {
                _clientRepo = new ClientCaseRepositoryEF();
            }
        }

        public ClientController(IClientCaseRepository newRepo)
        {
            _clientRepo = newRepo;
        }
            
        // GET: Client
        public ActionResult Index()
        {   // having an error when it's running
            // Option 1: put the GetClients result through a foreach
            // on each item, create a ViewModel and add that to 
            // a new List<ClientViewModel> then pass THAT list
            // to the view
            List<ClientViewModel> clientVmList = new List<ClientViewModel>();
            foreach (var client in _clientRepo.GetClients())
            {
                clientVmList.Add(new ClientViewModel(client));
            }
            //ViewBag.sexList = new SelectList(_clientRepo.GetSex());
            return View(clientVmList);
        }

        // GET: Client/Details/5
        public ActionResult Details(int id)
        {
            var client = _clientRepo.GetClientById(id);
            ClientViewModel clientVm = new ClientViewModel(client);
            
            return View(clientVm);
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            ViewBag.sexList = new SelectList(_clientRepo.GetSex());
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        public ActionResult Create(Client newClient, FormCollection formdata)
        {
            try
            {
                // TODO: Add insert logic here
                _clientRepo.AddClient(newClient);
                
                //ClientViewModel clientVm = new ClientViewModel(newClient);

                return RedirectToAction("Index");
            }
            catch
            {
                
                ViewBag.sexList = new SelectList(_clientRepo.GetSex());
                return View();
            }
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            var client = _clientRepo.GetClientById(id);
            ClientViewModel clientVm = new ClientViewModel(client);

            ViewBag.sexList = new SelectList(_clientRepo.GetSex());

            return View(clientVm);
            //return View(_clientRepo.GetClientById(id));
        }

        // POST: Client/Edit/5
        [HttpPost]
        public ActionResult Edit(Client updateClient, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                _clientRepo.UpdateClient(updateClient);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.sexList = new SelectList(_clientRepo.GetSex());
                return View();
            }
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int id)
        {
            var client = _clientRepo.GetClientById(id);
            ClientViewModel clientVm = new ClientViewModel(client);
            return View(clientVm);

            //return View(_clientRepo.GetClientById(id));
        }

        // POST: Client/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                _clientRepo.DeleteClient(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
