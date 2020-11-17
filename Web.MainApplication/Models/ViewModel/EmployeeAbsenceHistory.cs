using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.MainApplication.Models.ViewModel
{
    public class EmployeeAbsenceHistory
    {
        public string EmployeeId
        {
            get; set;
        }
        public string EmployeeName
        {
            get; set;
        }
        public string MonthName
        {
            get; set;
        }
        public int Month
        {
            get; set;
        }
        public int Year
        {
            get; set;
        }
        public List<EmployeeAbsenceHistoryRow> EmployeeAbsenceHistoryRows;
    }
    public class EmployeeAbsenceHistoryRow
    {
        public int No { get; set; }
        public DateTime Tanggal
        {
            get; set;
        }
        public DateTime? ClockIn
        {
            get; set;
        }
        public DateTime? ClockOut
        {
            get; set;
        }
        public string JadwalKerja
        {
            get; set;
        }
        public int? DurasiMenit
        {
            get; set;
        }
        public List<string> Keterangan
        {
            get; set;
        }
    }
}