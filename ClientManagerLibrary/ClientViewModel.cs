using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClientManagerLibrary
{
    public class ClientViewModel
    {
        public ClientViewModel()
        {
            Cases = new List<CaseViewModel>();
        }

        public ClientViewModel(Client client)
        {   //1. add this 5 lines.  Question: 'Cases' come from here? or from Client.cs?
            Cases = new List<CaseViewModel>();

            foreach (var caseItem in client.Cases)//Client has only 'Case' not 'CaseViewModel', how it works?
            {
                Cases.Add(new CaseViewModel(caseItem));
            }

            ClientId = client.ClientId;
            FName = client.FName;
            LName = client.LName;
            Bday = client.Bday;
            Sex = client.Sex;
            PhoneNo = client.PhoneNo;
            Email = client.Email;
            Address = client.Address;
            //NumCases = Cases.Count;  2. comment out because we will do number 3.   
            //doesn't have to be in constructor?
            
        }
        [System.ComponentModel.DisplayName("Client Id")]
        public int ClientId { get; set; }

        [System.ComponentModel.DisplayName("First Name")]
        public string FName { get; set; }

        [System.ComponentModel.DisplayName("Last Name")]
        public string LName { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [System.ComponentModel.DisplayName("Birthday")]
        public DateTime? Bday { get; set; }

        public string Sex { get; set; }

        [System.ComponentModel.DisplayName("Phone Number")]
        public string PhoneNo { get; set; }

        public string Email { get; set; }
        public string Address { get; set; }

        [System.ComponentModel.DisplayName("Number of Cases")]
        public int NumCases  //3. add getter of NumCases, no need setter
        {
            get { return Cases.Count(); }

        }

        public List<CaseViewModel> Cases {get; set;}
    }
}