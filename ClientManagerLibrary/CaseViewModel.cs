using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagerLibrary
{
    public class CaseViewModel
    {
        public CaseViewModel()
        {
            CaseStatuses = new List<CaseStatus>(); // put this to make CaseViewModel.CaseStatus not null
        }

        public CaseViewModel(Case findCase)
        {   
            CaseStatuses = new List<CaseStatus>();
            

            try  //add this try and catch to fix the freezing error
            {   //put the entire part into try from here 
                CaseId = findCase.CaseId;
                FName = findCase.Client.FName; //since CaseViewModel already has client, we can do this way
                LName = findCase.Client.LName;
                Category = findCase.Category;
                OpenDate = findCase.OpenDate;
                CaseStatus = findCase.CaseStatus.Status; //[CaseStatus was returing null], so added this line.

                //Since we don't have Status yet, added this New as default
                //if (findCase.CaseStatus != null)
                //{
                //    CaseStatus = findCase.CaseStatus.Status;
                //}
                //else
                //{
                //    CaseStatus = "New";
                //}

                //to this part

                Note = findCase.Note;
                ClosedDate = findCase.ClosedDate;
                ClientId = findCase.ClientId;
            }
            catch (Exception ex)
            {
                var foo = ex;
            }
                       
            
        }
        [System.ComponentModel.DisplayName("Case Id")]
        public int CaseId { get; set; }

        [System.ComponentModel.DisplayName("First Name")]
        public string FName { get; set; }

        [System.ComponentModel.DisplayName("Last Name")]
        public string LName { get; set; }

        public string Category { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [System.ComponentModel.DisplayName("Open Date")]
        public DateTime? OpenDate { get; set; }

        [System.ComponentModel.DisplayName("Status")]
        public string CaseStatus { get; set; }
        //public int CaseStatusId { get; set; }

        public string Note { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [System.ComponentModel.DisplayName("Closed Date")]
        public DateTime? ClosedDate { get; set; }

        [System.ComponentModel.DisplayName("Client Id")]
        public int ClientId { get; set; }

        public List<CaseStatus> CaseStatuses { get; set; }

    }
}
