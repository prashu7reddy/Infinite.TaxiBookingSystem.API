using Infinite.TaxiBookingSystem.API.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.TaxiBookingSystem.API.Repositories
{
    public class EmployeeRepository : IRepository<Employee>,IGetRepository<EmployeeDto>,IEmployeeRepository
    {
        private readonly ApplicationDbContext _Context;


        public EmployeeRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task Create(Employee obj)
        {
            if (obj != null)
            {
                _Context.Employees.Add(obj);
                await _Context.SaveChangesAsync();
            }
        }

        public async Task<Employee> Delete(int id)
        {
            var employeeDb = await _Context.Employees.FindAsync(id);
            if (employeeDb != null)
            {
                _Context.Employees.Remove(employeeDb);
                await _Context.SaveChangesAsync();
                return employeeDb;
            }
            return null;
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            var employees = _Context.Employees.Include(x => x.Designation).Select(x => new EmployeeDto
            {
                EmployeeId = x.EmployeeId,
                EmployeeName = x.EmployeeName,
                DesignationId = x.DesignationId,
                PhoneNo = x.PhoneNo,
                EmailId = x.EmailId,
                Address = x.Address,
                DrivingLicenseNo = x.DrivingLicenseNo,
                DesignationName = x.Designation.DesignationName
            }).ToList();
            return employees;
                
                }

        

        public async Task<EmployeeDto> GetById(int id)
        {
            var employees = await _Context.Employees.Include(x => x.Designation).Select(x => new EmployeeDto
            {
                EmployeeId = x.EmployeeId,
                EmployeeName = x.EmployeeName,
                DesignationId = x.DesignationId,
                PhoneNo = x.PhoneNo,
                EmailId = x.EmailId,
                Address = x.Address,
                DrivingLicenseNo = x.DrivingLicenseNo,
                DesignationName = x.Designation.DesignationName
            }).ToListAsync();
            var employee = employees.FirstOrDefault(x => x.EmployeeId == id);
            if(employee != null)
            {
                return employee;
            }
            return null;
        }

        

        public async Task<Employee> Update(int id, Employee obj)
        {
            var employeeDb = await _Context.Employees.FindAsync(id);
            if (employeeDb != null)
            {
                employeeDb.EmployeeName = obj.EmployeeName;
                employeeDb.Designation = obj.Designation;
                employeeDb.PhoneNo = obj.PhoneNo;
                employeeDb.EmailId = obj.EmailId;
                employeeDb.Address = obj.Address;
                //employeeDb.DrivingLicenseNo = obj.DrivingLicenseNo;
                _Context.Employees.Update(employeeDb);
                await _Context.SaveChangesAsync();
                return employeeDb;
            }
            return null;
        }

        //get designation
        public async Task<IEnumerable<Designation>> GetDesignations()
        {
            var designations = await _Context.Designations.ToListAsync();
            return designations;
        }

    }
}
