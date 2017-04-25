using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ClientManagerLibrary
{
    public class ClientCaseRepositoryinMemory : IClientCaseRepository
    {
        private static List<Client> _clientList = new List<Client>();
        //private List<ClientViewModel> _clientViewList = new List<ClientViewModel>();

        private static List<Case> _caseList = new List<Case>();
        private static List<string> _sexList = new List<string>();

        private static List<CaseStatus> _statusList = new List<CaseStatus>();

        
        private static int nextClientId = 0;
        private static int nextCaseId = 0;
        private static int nextStatusId = 0;


        public ClientCaseRepositoryinMemory()
        {
            LoadSexList();
            LoadSampleClients();
            LoadSampleCases();
            LoadSampleCaseStatus();
        }

        public List<Client> GetClients()
        {
            return _clientList;
        }

        public Client GetClientById(int id)
        {
            return _clientList.Find(client => client.ClientId == id);

        }

        //public ClientViewModel GetClientViewById(int id)
        //{
        //    return _clientViewList.Find(client => client.ClientId == id);
        //}

        public void AddClient(Client newClient)
        {
            newClient.ClientId = nextClientId++;
            _clientList.Add(newClient);
        }

        public void DeleteClient(int id)
        {
            _clientList.RemoveAll(client => client.ClientId == id);
        }

        public void UpdateClient(Client updateClient)
        {
            _clientList.RemoveAll(client => client.ClientId == updateClient.ClientId);
            _clientList.Add(updateClient);
        }

        public List<Case> GetCases()
        {
            return _caseList;
        }

        public Case GetCaseById(int id)
        {
            return _caseList.Find(getCase => getCase.CaseId == id);
        }

        public void AddCase(Case newCase)
        {
            //return _clientList.Find(client => client.ClientId == id);
            newCase.CaseId = nextCaseId++; //not just nextCaseID++;

            var client = _clientList.Find(c => c.ClientId == newCase.ClientId);
            client.Cases.Add(newCase); //adding newCase to client.Cases and _caseList both to get 
            _caseList.Add(newCase);    // number of cases
        }

        public void DeleteCase(int id)
        {
            _caseList.RemoveAll(deleteCase => deleteCase.CaseId == id);
        }

        public void UpdateCase(Case updateCase)
        {
            _caseList.RemoveAll(existingCase => existingCase.CaseId == updateCase.CaseId);
            _caseList.Add(updateCase);
        }
        //add new method to link "Number of Cases" and Cases.
        public List<Case> GetCaseByClientId(int id)
        {
            var targetCase = _caseList.Where(client => client.ClientId == id).ToList();
            //instead of _caseList.Find, _caseList.Where, and add ToList(); 
            return targetCase;
        }

        public List<CaseStatus> GetStatuses()
        {
            return _statusList;
        }
        
        public CaseStatus GetStatusById(int id)
        {
            return _statusList.Find(status => status.CaseStatusId == id);

        }
        //[CaseStatus was returing null], so added this new method.
        //public CaseStatus GetStatusByName(string statusName)
        //{
        //    return _statusList.Find(status => status.Status.ToLower() == statusName.ToLower());
        //}

        public void AddStatus(CaseStatus newStatus)
        {
            newStatus.CaseStatusId = nextStatusId++;
            _statusList.Add(newStatus);
        }

        public void DeleteStatus(int id)
        {
            _statusList.RemoveAll(deleteStatus => deleteStatus.CaseStatusId == id );
        }

        public void UpdateStatus(CaseStatus updateStatus)
        {
            _statusList.RemoveAll(status => status.CaseStatusId == updateStatus.CaseStatusId);
            _statusList.Add(updateStatus);
        }




        public List<string> GetSex()
        {
            return _sexList;
        }

        public void LoadSampleClients()
        {//Error2: after I fixed error1, this time number of the clients was incrementing when I clicked 
         //the manage cases, then come back to manage clients. to resolve this, add the following: 
            if (_clientList.Count > 0)
            {
                return;
            }
            Client c1 = new Client
            {
                FName = "Kevin",
                LName = "Kim",
                Bday = DateTime.Parse("01/22/1975"),
                Sex = "Male",
                PhoneNo = "555-555-5555",
                Email = "hotheart0122@gmail.com",
                Address = "1234 Atlanta, GA 30030",
                Cases = new List<Case>()

            };
            AddClient(c1);
        }

        public void LoadSampleCases()
        {//Error1: number of the cases was incrementing when I clicked the manage clients
         //   then come back to manage cases. to resolve this, add the following: 
            if (_caseList.Count > 0)
            {
                return;
            }

            Case cs1 = new Case
            {
                Client = new Client { FName = "Kevin", LName = "Kim"},
                //FName = "Kevin",
                //LName = "Kim",
                Category = "Immigration",
                OpenDate = DateTime.Parse("1/1/2017"),
                //CaseStatusId = casestatus.CaseStatusId;
                //CaseStatus = new CaseStatus { Status = "new"},
                CaseStatus = new CaseStatus(),
                Note = "first client",
                ClosedDate = null,
                ClientId = 0

            };
            AddCase(cs1);
        }

        public void LoadSampleCaseStatus()
        {
            if (_statusList.Count > 0)
            {
                return;
            }

            CaseStatus s1 = new CaseStatus
            {
                Status = "Closed"
            };
            AddStatus(s1);
            //[CaseStatus was returing null], so added this sample status.
            CaseStatus s2 = new CaseStatus
            {
                Status = "New"
            };
            AddStatus(s2);
        }

        public void LoadSexList()
        {
            //Error3: male/female was duplicating. to resolve this, add the following: 
            if (_sexList.Count > 0)
            {
                return;
            }
            _sexList.Add("Male");
            _sexList.Add("Female");
        }
    }
}
