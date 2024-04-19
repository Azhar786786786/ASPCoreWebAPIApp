using ASPCoreWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace ASPCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly IConfiguration Configuration;
        public EmployeeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        [HttpGet]
        [Route("GetAllEmployees")]
        public string GetEmployees()
        {
            SqlConnection con = new SqlConnection(Configuration.GetConnectionString("EmpployeeAppCon").ToString());
            SqlDataAdapter da = new SqlDataAdapter("select Id,Name,Place,Gender,Salary from Employee", con);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            List<clsEmployee> employees = new List<clsEmployee>();
            clsResponse response = new clsResponse();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    clsEmployee employee = new clsEmployee();
                    employee.Id = Convert.ToInt32(dataTable.Rows[i]["id"]);
                    employee.Name = Convert.ToString(dataTable.Rows[i]["Name"]);
                    employee.Place = Convert.ToString(dataTable.Rows[i]["Place"]);
                    employee.Gender = Convert.ToString(dataTable.Rows[i]["Gender"]);
                    employee.Salary = Convert.ToDecimal(dataTable.Rows[i]["Salary"]);
                    employees.Add(employee);
                }
            }
            if (employees.Count > 0)
            {
                return JsonConvert.SerializeObject(employees);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "No data found";
                return JsonConvert.SerializeObject(response);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetData")]
        public string GetData()
        {
            return "Authenticated with JWT";
        }
        [HttpGet]
        [Route("GetDetails")]
        public string GetDetails()
        {
            return "Authenticate JWT";
        }
        [Authorize]
        [HttpPost]
        [Route("AddUser")]
        public string AddUser(clsUsers user)
        {
            return "Users Added with username:" + user.UserName;
        }
    }
}
