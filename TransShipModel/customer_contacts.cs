//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TransShipModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class customer_contacts
    {
        public decimal customerId { get; set; }
        public decimal contactId { get; set; }
        public string full_name { get; set; }
        public string title { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
    
        public virtual customers customers { get; set; }
    }
}