using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ClientManagerLibrary
{
    public class ClientCaseRepositoryEF : IClientCaseRepository
    {
        private static List<string> _sexList = new List<string>();

        public ClientCaseRepositoryEF()
        {
            LoadSexList();
        }

        public void AddCase(Case newCase)
        {
            //This is difficult
            using (var db = new ClientModel())
            {
                var client = db.Clients.FirstOrDefault(c => c.ClientId == newCase.ClientId);
                newCase.Client = client; //2. 'Create case' had an error, so added this line.
                client.Cases.Add(newCase);
                db.SaveChanges();
            }
            //
        }

        public void AddClient(Client newClient)
        {
            using (var db = new ClientModel())
            {
                db.Clients.Add(newClient);
                db.SaveChanges();
            }
        }

        public void AddStatus(CaseStatus newStatus)
        {
            using (var db = new ClientModel())
            {
                db.CaseStatus.Add(newStatus);
                db.SaveChanges();
            }
            
        }

        public void DeleteCase(int id)
        {
            using (var db = new ClientModel())
            {
                var targetCase = db.Cases.Find(id);
                db.Cases.Remove(targetCase);
                db.SaveChanges();
            }
                
        }

        public void DeleteClient(int id)
        {
            using (var db = new ClientModel())
            {
                var client = db.Clients.Find(id);
                db.Clients.Remove(client);
                db.SaveChanges();
            }
        }

        public void DeleteStatus(int id)
        {
            using (var db = new ClientModel())
            {
                var status = db.CaseStatus.Find(id);
                db.CaseStatus.Remove(status);
                db.SaveChanges();
            }
        }

        public List<Case> GetCaseByClientId(int id)
        {
            using (var db = new ClientModel())
            {    
                var targetCase = db.Cases
                    .Include(c => c.Client) //'Number of Cases' was not working, so added this line
                    .Include(c => c.CaseStatus) //Case Index was not showing Case Status, note, and wrong Client Id(0),
                                                // so added this line to fix. It's caused by Lazy Loading
                    .Where(client => client.ClientId == id).ToList();
                //db.Entry(targetCase).Collection(c => c.Clients).Load();
                return targetCase;
            }
            
        }

        public Case GetCaseById(int id)
        {
            using (var db = new ClientModel())
            {
                var targetCase = db.Cases
                    //.Find(id)
                    .Include(c => c.Client)
                    .Include(c => c.CaseStatus)
                    .Where(c => c.CaseId == id)
                    .FirstOrDefault();

                return targetCase;
            }
                
        }

        public List<Case> GetCases()
        {
            using (var db = new ClientModel())
            {
                //var cases = from c in db.Cases
                //                orderby c.CaseId descending
                //                select c;
                //3. 'Create Case was having an error, so changed above three lines to below 4 lines.
                var cases = db.Cases
                    .Include(c => c.Client)
                    .Include(c => c.CaseStatus) //Case Index was not showing Case Status, note, and wrong Client Id(0),
                    // so added this line to fix. It's caused by Lazy Loading
                    .OrderByDescending(c => c.CaseId)
                    .ToList();

                return cases;  //4. 'Create Case' had an error, so deleted ToLIst because we already added it above.
            }
              
        }

        public Client GetClientById(int id)
        {
            using (var db = new ClientModel())
            {
                var client = db.Clients
                    .Include(c => c.Cases)//to fix the error, include cases to client
                    .Where(c => c.ClientId == id)
                    .FirstOrDefault();
                return client;
            }
        }

        public List<Client> GetClients()
        {
            using (var db = new ClientModel())
            {
                //var clients = from c in db.Clients
                //            orderby c.LName descending
                //            select c;

                //Clients had an error, we need to tell EF to load Case table too using 'Include'
                var clients = db.Clients
                    .Include(c => c.Cases)
                    .ToList();

                return clients;
            }
        }

        public List<string> GetSex()
        {
           return _sexList;
        }

        public CaseStatus GetStatusById(int id)
        {
            using (var db = new ClientModel())
            {
                var status = db.CaseStatus.Find(id);
                return status;
            }
            
        }

        //public CaseStatus GetStatusByName(string statusName)
        //{
        //    throw new NotImplementedException();
        //}

        public List<CaseStatus> GetStatuses()
        {
            using (var db = new ClientModel())
            {
                var statuses = from s in db.CaseStatus
                            orderby s.CaseStatusId descending
                            select s;

                return statuses.ToList();
            };
        }

        public void LoadSexList()
        {
            if (_sexList.Count > 0)
            {
                return;
            }
            _sexList.Add("Male");
            _sexList.Add("Female");
            
        }

        public void UpdateCase(Case updateCase)
        {
            using (var db = new ClientModel())
            {
                //var targetCase = db.Cases.FirstOrDefault(t => t.CaseId == updateCase.CaseId);
                //db.Cases.Remove(targetCase);
                //db.Cases.Add(updateCase);
                //db.SaveChanges();

                db.Cases.Attach(updateCase); //everytime I edit case, new case and new client was being created.
                var entry = db.Entry(updateCase); // so instead of above 4 lines, I added this 3 lines.
                entry.State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
            }
        }

        public void UpdateClient(Client updateClient)
        {
            using (var db = new ClientModel())
            {
                //var client = db.Clients.FirstOrDefault(c => c.ClientId == updateClient.ClientId);

                //.Include(c=>c.Cases)
                //db.Clients.Remove(client);
                //db.Clients.Add(updateClient); these 2 lines are for inMemory Repository, Not EF

                //client.Address = updateClient.Address; //1. we can add each property one by one like this, or we can do 2 below.
                //client.Bday = updateClient.Bday; //1. we can add each property one by one like this, or we can do 2 below.

                //foreach (var item in client)
                //{
                //    var clientInfo = db.Entry(client);
                //clientInfo.State = EntityState.Modified;
                    //if (client.CaseId == 0)   
                    //{
                    //    clientInfo.State = EntityState.Added;
                    //}
                    //else
                    //{
                    //    clientInfo.State = System.Data.Entity.EntityState.Modified;
                    //}
                //}

                db.Clients.Attach(updateClient); //2. This seems easier thant 1. above.
                var entry = db.Entry(updateClient);
                entry.State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

                //var clients = db.Clients
                //    .Include(c => c.Cases)
                //    .ToList();

                //return clients;

            }
        }

        public void UpdateStatus(CaseStatus updateStatus)
        {
            using (var db = new ClientModel())
            {
                //var status = db.CaseStatus.FirstOrDefault(s => s.CaseStatusId == updateStatus.CaseStatusId);
                //db.CaseStatus.Remove(status);
                //db.CaseStatus.Add(updateStatus);
                //db.SaveChanges();

                db.CaseStatus.Attach(updateStatus); //Edit Status was not working when there is a case linked.
                var entry = db.Entry(updateStatus); // so I changed above 4 lines to these 4 lines.
                entry.State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
            }
        }
    }
}
