using Newtonsoft.Json;
using RestSharp;
using System.Net;
using Employee_RESTAPI;
using Newtonsoft.Json.Linq;

namespace Employee_RESTAPI_test
{
    public class TestRESTAPI
    {
        //uc1 get method
        RestClient restClient;
        [Test]
        public void EmployeeDetails_GETmethod()
        {
            restClient = new RestClient("http://localhost:3000");
            RestRequest request = new RestRequest("/employees", Method.Get);
            RestResponse response = restClient.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<Employee_Model> list = JsonConvert.DeserializeObject<List<Employee_Model>>(response.Content);
            Assert.AreEqual(5, list.Count);
            foreach (Employee_Model emp in list)
            {
                Console.WriteLine("Employee ID: " + emp.id + "\nEmployee Name: " + emp.name + "\nEmployee Salary: " + emp.salary);
            }
        }

        [Test]
        //uc2 post method
        public void EmployeeDetails_POSTmethod()
        {
            restClient = new RestClient("http://localhost:3000");
            RestRequest request = new RestRequest("/employees", Method.Post);
            //JObject jObjectbody= new JObject();
            //jObjectbody.Add("name", "Sandeep");
            //jObjectbody.Add("salary", "26000");
            var body = new Employee_Model { id = 6, name = "Sandeep", salary = "26000" };
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = restClient.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Employee_Model emp = JsonConvert.DeserializeObject<Employee_Model>(response.Content);
            Assert.AreEqual("Sandeep", emp.name);
            Assert.AreEqual("26000", emp.salary);
            Console.WriteLine(response.Content);
        }
        [Test]
        public void addingMultiplevalues()
        {
            restClient = new RestClient("http://localhost:3000");
            List<Employee_Model> employee = new List<Employee_Model>();
            employee.Add(new Employee_Model { id = 7, name = "Keerthi", salary = "26500" });
            employee.Add(new Employee_Model { id = 8, name = "Kumar", salary = "27000" });
            employee.ForEach(body =>
            {
                RestRequest request = new RestRequest("/employees", Method.Post);
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                RestResponse response = restClient.Execute(request);
                Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
                Employee_Model emp = JsonConvert.DeserializeObject<Employee_Model>(response.Content);
                Assert.AreEqual(body.name, emp.name);
                Assert.AreEqual(body.salary, emp.salary);
                Console.WriteLine(response.Content);
            });
        }
        //uc4-update the existing values
       [Test]
        public void EmployeeDetails_UPDATEmethod()
        {
            restClient = new RestClient("http://localhost:3000");
            RestRequest request = new RestRequest("/employees/7", Method.Put);
            var body = new Employee_Model
            {
                id = 7,
                name = "Kamali",
                salary = "31000"
            };
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = restClient.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Employee_Model emp = JsonConvert.DeserializeObject<Employee_Model>(response.Content);
            Assert.AreEqual(body.name, emp.name);
            Assert.AreEqual(body.salary, emp.salary);
            Console.WriteLine(response.Content);
            //(id 8 has been changed using postman)
        }
        [Test]
        //uc5 delete method
        public void EmployeeDetails_DELETEmethod()
        {
            restClient = new RestClient("http://localhost:3000");
            RestRequest request = new RestRequest("/employees/8", Method.Delete);
            RestResponse response = restClient.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Console.WriteLine(response.Content);
            //(id 7 has been deleted using postman)
        }
    }
}
