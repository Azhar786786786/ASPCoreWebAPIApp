using ASPCoreWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace ASPCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        public readonly IConfiguration Configuration;
        public RegistrationController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        [HttpPost]
        [Route("Registration")]
        public string Registration(clsRegistration registration)
        {
            clsResponse response = new clsResponse();
            if (registration != null)
            {
                SqlConnection con = new SqlConnection(Configuration.GetConnectionString("EmpployeeAppCon").ToString());
                SqlCommand cmd = new SqlCommand("insert into Registeration(UserName,Password,Email,IsActive) values('" + registration.UserName + "','" + registration.Password + "','" + registration.Email + "','" + registration.IsActive + "')", con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    response.StatusCode = 200;
                    response.ErrorMessage = "Data is now Inserted.";
                    return JsonConvert.SerializeObject(response);
                }
                else
                {
                    response.StatusCode = 100;
                    response.ErrorMessage = "Data is not inserted properly";
                    return JsonConvert.SerializeObject(response);
                }
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "Data is Empty";
                return JsonConvert.SerializeObject(response);
            }
        }

        [HttpPost]
        [Route("Login")]
        public string Login(clsRegistration registration)
        {
            clsResponse response = new clsResponse();
            if (registration != null)
            {
                SqlConnection con = new SqlConnection(Configuration.GetConnectionString("EmpployeeAppCon").ToString());
                SqlDataAdapter da = new SqlDataAdapter("select * from Registration where Email='" + registration.Email + "' AND Password='" + registration.Password + "' AND IsActive='" + registration.IsActive + "' ", con);
                DataTable dataTable = new DataTable();
                da.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    response.StatusCode = 200;
                    response.ErrorMessage = "Data is now Available.";
                    return JsonConvert.SerializeObject(response);
                }
                else
                {
                    response.StatusCode = 100;
                    response.ErrorMessage = "Data is not inserted properly";
                    return JsonConvert.SerializeObject(response);
                }
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "Data is Empty";
                return JsonConvert.SerializeObject(response);
            }
        }
    }
}
