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
    }
}