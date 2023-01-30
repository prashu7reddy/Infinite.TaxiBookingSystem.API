using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.TaxiBookingSystem.API.Models
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        
        public int DesignationId { get; set; }
        public string PhoneNo { get; set; }
        public string EmailId { get; set; }
        public string Address { get; set; }
        public string DrivingLicenseNo { get; set; }

        //added later
        public string DesignationName { get; set; }
    }
}
