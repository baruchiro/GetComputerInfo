//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AddToComputersDB.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Disks
    {
        public int id { get; set; }
        public string name { get; set; }
        public Nullable<int> capacity { get; set; }
        public Nullable<bool> type { get; set; }
        public string comments { get; set; }
        public string status { get; set; }
        public Nullable<bool> computerType { get; set; }
        public Nullable<int> osID { get; set; }
        public string osVersion { get; set; }
        public Nullable<bool> freeToUse { get; set; }
        public Nullable<int> moboID { get; set; }
    
        public virtual MoBos MoBos { get; set; }
    }
}
