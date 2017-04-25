using System.Collections.Generic;

namespace ClientManagerLibrary
{
    public interface IClientCaseRepository
    {
        void AddCase(Case newCase);
        void AddClient(Client newClient);
        void AddStatus(CaseStatus newStatus);
        void DeleteCase(int id);
        void DeleteClient(int id);
        void DeleteStatus(int id);
        List<Case> GetCaseByClientId(int id);
        Case GetCaseById(int id);
        List<Case> GetCases();
        Client GetClientById(int id);
        List<Client> GetClients();
        List<string> GetSex();
        CaseStatus GetStatusById(int id);
        //CaseStatus GetStatusByName(string statusName);
        List<CaseStatus> GetStatuses();
        void LoadSexList();
        void UpdateCase(Case updateCase);
        void UpdateClient(Client updateClient);
        void UpdateStatus(CaseStatus updateStatus);
    }
}