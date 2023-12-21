using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;
using System.Diagnostics;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();
            Console.WriteLine(IncreaseSalaries(context));
        }
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(e=>new
                {
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary
                }).ToList();

            string result = string.Join(Environment.NewLine, employees.Select(e=>$"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}"));

            return result;
        }
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(e => new 
                { 
                    e.FirstName,
                    e.Salary
                }).Where(e=>e.Salary>50000).OrderBy(e=>e.FirstName).ToList();

            string result = string.Join(Environment.NewLine, employees.Select(e => $"{e.FirstName} - {e.Salary:f2}"));
            return result;
        }
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var rndEmployees = context.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Department.Name,
                    e.Salary
                }).OrderBy(e => e.Salary).ThenByDescending(e => e.FirstName);
            //foreach (var employee in rndEmployees)
            //{
            //    Console.WriteLine($"{employee.FirstName} {employee.LastName} from Research and Development - ${employee.Salary}");
            //}

            return string.Join(Environment.NewLine,rndEmployees.Select(e=>$"{e.FirstName} {e.LastName} from {e.Name} - ${e.Salary:f2}"));
        }
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            Address address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            var employee = context.Employees
                .FirstOrDefault(e => e.LastName == "Nakov");

            employee.Address=address;

            context.SaveChanges();

            var employees = context.Employees
                .Select(e => new { e.AddressId, e.Address.AddressText })
                .OrderByDescending(e => e.AddressId)
                .Take(10)
                .ToList();

            return string.Join(Environment.NewLine,employees.Select(e=> $"{e.AddressText}"));
        }
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var employees = context.Employees.Where(e => e.EmployeesProjects.Any(ep => ep.Project.StartDate.Year >= 2001 && ep.Project.StartDate.Year <= 2003))
                .Select(e => new
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    ManagerFirstName = e.Manager.FirstName,
                    ManagerLastName = e.Manager.LastName,
                    Projects = e.EmployeesProjects.Select(ep => new
                    {
                        ProjectName = ep.Project.Name,
                        ProjectStartDate = ep.Project.StartDate,
                        ProjectEndDate = ep.Project.EndDate
                    })
                }).Take(10);

            StringBuilder employeeManagerResult = new StringBuilder();

            foreach (var employee in employees)
            {
                employeeManagerResult.AppendLine($"{employee.FirstName} {employee.LastName} - Manager: {employee.ManagerFirstName} {employee.ManagerLastName}");

                foreach (var project in employee.Projects)
                {
                    var startDate = project.ProjectStartDate.ToString("M/d/yyyy h:mm:ss tt");
                    var endDate = project.ProjectEndDate.HasValue ? project.ProjectEndDate.Value.ToString("M/d/yyyy h:mm:ss tt") : "not finished";

                    employeeManagerResult.AppendLine($"--{project.ProjectName} - {startDate} - {endDate}");
                }
            }
            return employeeManagerResult.ToString().TrimEnd();
        }

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var employees = context.Employees
                            .Select(e => new
                            {
                                e.FirstName,
                                e.LastName,
                                e.JobTitle,
                                e.Salary
                            })
                            .Where(e=>e.FirstName.StartsWith("Sa"))
                            .OrderBy(e=>e.FirstName)
                            .ThenByDescending(e => e.LastName)
                            .ToList();

            return string.Join(Environment.NewLine, employees.Select(e => $"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})"));
                            
        }
        //public static string GetAddressesByTown(SoftUniContext context)
        //{
        //    var addresses = context.Employees
        //        .Select(e=> new
        //        {
        //           e.Address.AddressText,
        //           e.Address.Town.Name,

        //        }
        //        )


        //    return addresses.ToString();
        //}
        //public static string GetEmployee147(SoftUniContext context)
        //{

        //    var employee = context.Employees
        //        .Include(e => e.EmployeesProjects
        //        .Where(e => e.EmployeeId == 147)).ThenInclude(ep => ep.Project)
        //        .FirstOrDefault();
        //    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

        //    foreach (var employeeProject in employee.EmployeesProjects.OrderBy(e=>e.Name))
        //    {
        //        Console.WriteLine(employeeProject.Project.Name);
        //    }
        //    return string.Join(Environment.NewLine)
        //}
        //public static string IncreaseSalaries(SoftUniContext context)
        //{
        //    var employees = context.Employees
        //        .Where(e => e.Departments.Any(e => e.Name == "Engineering" || e.Name == "Tool Design" || e.Name == "Marketing" || e.Name == "Information Services"))
        //        .Select(e=> new
        //        {
        //            e.FirstName,
        //            e.LastName,
        //            e.Salary
        //        }).OrderBy(e=>e.FirstName);

        //    Console.WriteLine(employees.ToQueryString());
        //    return "";
        //}
    }
}