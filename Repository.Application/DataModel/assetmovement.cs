//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Repository.Application.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class assetmovement
    {
        public int id { get; set; }
        public Nullable<int> idAsset { get; set; }
        public Nullable<int> idPlace { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<int> movementType { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string Remark { get; set; }
        public Nullable<short> IsActive { get; set; }
    
        public virtual asset asset { get; set; }
        public virtual place place { get; set; }
    }
}
