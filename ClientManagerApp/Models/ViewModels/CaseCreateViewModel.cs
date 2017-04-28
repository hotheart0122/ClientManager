using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientManagerLibrary;
using System.Web.Mvc;

namespace ClientManagerApp.Models.ViewModels    //created this viewModel to fix the create and edit error, but failed
    //it seems like the MVC has an error regarding the default value and preselected value.
{
    public class CaseCreateViewModel : CaseViewModel
    {
        public SelectList CaseStatusSelectList { get; set; }
    }
}