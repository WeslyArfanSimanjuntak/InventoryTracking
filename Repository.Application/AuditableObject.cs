using System;
using Interface.Application;

namespace Repository.Application.DataModel
{
    public partial class AuditableObject : IAuditableObject
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public short? IsActive { get; set; }
    }

    public partial class AspNetGroups : IAuditableObject
    {
    }

    public partial class AspNetGroupUser : IAuditableObject
    {
    }

    public partial class AspNetRoleGroup : IAuditableObject
    {
    }

    public partial class AspNetRoles : IAuditableObject
    {
    }
    public partial class AspNetUsers : IAuditableObject
    {
    }
    public partial class AspNetMenu : IAuditableObject
    {
    }

    public partial class CommonListValue : IAuditableObject
    {
    }
    public partial class asset  : IAuditableObject
    {
    }
    public partial class place : IAuditableObject
    {
    }
    public partial class assethistory : IAuditableObject
    {
    }
    public partial class assetmovement : IAuditableObject
    {
    }
    public partial class movementtype  : IAuditableObject
    {
    }

}