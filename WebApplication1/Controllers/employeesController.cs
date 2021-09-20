using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Text.Json;
using Dapper;

namespace anresh_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class employeesController : ControllerBase
    {

        private const string CONNECTION_STRING =
            @"Server=DESKTOP-30N0GBN\SQLEXPRESS;Database=AnReshProbation;Trusted_Connection=True;";


        [HttpGet]
        public ActionResult Get()
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                dynamic test = connection.Query<dynamic>(
                    "SELECT em.[id] as id, em.[id_department] as id_department, em.[fio] as fio, de.[id] as Dp_id, de.[name] as Dp_name, em.[salary] FROM Employees em INNER JOIN Department de ON em.id_department=de.id");

                Slapper.AutoMapper.Configuration.AddIdentifiers(typeof(Employees), new List<string> { "id" });
                Slapper.AutoMapper.Configuration.AddIdentifiers(typeof(Department), new List<string> { "id" });

                var testData = (Slapper.AutoMapper.MapDynamic<Employees>(test) as IEnumerable<Employees>).ToList();
                return Ok(testData);
            }
        }


        [HttpPut()]
        [Consumes("application/x-www-form-urlencoded")]
        public ActionResult Put([FromForm] UpdateEmpoyeeDataModel data)
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))

            {
                var result_employee = connection.Execute("UPDATE Employees SET "+data.key+"=@value WHERE id=@id", data);

                return Ok(result_employee);
            }
        }


        [HttpPost()]
        [Consumes("application/x-www-form-urlencoded")]
        public ActionResult Post([FromForm] InsertEmpoyeeDataModel data)
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {

                var result_employee = connection.Execute("INSERT INTO Employees (id_department,fio,salary) VALUES (@id_department,@fio,@salary)",data);

                return Ok(result_employee);
            }
        }


        [HttpDelete()]
        [Consumes("application/x-www-form-urlencoded")]
        public ActionResult Delete([FromForm] DeleteEmployeeModel data)
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {


                var result_employee = connection.Execute("DELETE FROM Employees WHERE id = @id",data);

                return Ok(result_employee);
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


    public class UpdateEmpoyeeDataModel
    {
        public int id { get; set; }
        public string key { get; set; }

        public string value { get; set; }
    }

    public class InsertEmpoyeeDataModel
     
    {
       
        public int id_department { get; set; }
        public string fio { get; set; }
        public float salary { get; set; }

    }

    public class DeleteEmployeeModel
    {
        public int id { get; set; }
    }

}
