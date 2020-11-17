using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Application.DataModel.StoreProcedure
{
    public class PCFDetail
    {
        private long id;
        private string basicProductId;
        private string basicProductLimitCode;
        
        public long Id { get => id; set => id = value; }
        public string BasicProductId { get => basicProductId; set => basicProductId = value; }
        public string BasicProductLimitCode { get => basicProductLimitCode; set => basicProductLimitCode = value; }
    }
}
