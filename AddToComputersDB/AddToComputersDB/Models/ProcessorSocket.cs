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
    
    public partial class ProcessorSocket
    {
        public int Id { get; set; }
        public int ProcessorID { get; set; }
        public int SocketID { get; set; }
    
        public virtual Sockets Sockets { get; set; }
        public virtual Processors Processors { get; set; }
    }
}
