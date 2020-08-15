using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Project1.Website.Models
{
    public class ClassRegistrationModel
    {
        public ClassModel[] myClasses { get; set; }
        public int myClassIdToAdd { get; set; }
    }
}