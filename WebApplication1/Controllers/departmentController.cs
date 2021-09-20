using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography.X509Certificates;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class departmentController : ControllerBase
    {

        private const string CONNECTION_STRING =
            @"Server=DESKTOP-30N0GBN\SQLEXPRESS;Database=AnReshProbation;Trusted_Connection=True;";


        [HttpGet()]
        public ActionResult Get()
        {
            
            using (var connection = new SqlConnection(CONNECTION_STRING))
                
            {
                    var names = connection.Query<Department>(
                        "SELECT * FROM [AnReshProbation].[dbo].[Department]").ToList();
                    return Ok(names);
            }

        }


        [HttpPut()]
        [Consumes("application/x-www-form-urlencoded")]
        public ActionResult Put([FromForm] UpdateDepartmentDataModel data)
        {
           
            using (var connection = new SqlConnection(CONNECTION_STRING))
                
            {
                var result_department = connection.Execute("UPDATE Department SET name = @name WHERE id =@id",data);

                    return Ok(result_department);
            }
        }


        [HttpPost()]
        [Consumes("application/x-www-form-urlencoded")]
        public ActionResult Post([FromForm] InsertDepartmentDataModel data)
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                
                var result_department = connection.Execute("INSERT INTO Department (name) VALUES (@name)",data);

                return Ok(result_department);
            }
        }


        [HttpDelete()]
        [Consumes("application/x-www-form-urlencoded")]
        public ActionResult Delete([FromForm]DeleteDepartmentModel data)
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                var result_department = connection.Execute("DELETE FROM Department WHERE id = @id",data);

                return Ok(result_department);
            }
        }
    }


    public class Department
    {
        public int id { get; set; }
        public string name { get; set; }

    }


    public class Employees
    {
        public int id { get; set; }
        public int id_department { get; set; }

        public string fio { get; set; }

        public float salary { get; set; }
        public List<Department> Dp { get; set; }

    }


    public class UpdateDepartmentDataModel
    {
        public int id { get; set; }
        public string name { get; set; }
    }


    public class InsertDepartmentDataModel
    {
        public string name { get; set; }
    }


    public class DeleteDepartmentModel
    {
        public int id { get; set; }
    }


}