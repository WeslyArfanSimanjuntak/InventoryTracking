using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.MainApplication.Models.ViewModel
{
    public class spEmployeeLeave_Result
    {
        public long IdLeaveApplication { get; set; }
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime LeaveDate { get; set; }
        public DateTime EndDateTime { get; set; }
        public int Day { get; set; }
        public int IdLeaveType { get; set; }
        public string LeaveTypeName { get; set; }
    }
}