using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPIEmployesSalary.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIEmployesSalary.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebAPIEmployesSalary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPIEmployesSalary.Model;

//using Microsoft.AspNetCore.Mvc;
//using Moq;
using Xunit;


namespace WebAPIEmployesSalary.Controllers.Tests
{
    [TestClass()]
    public class EmployControllerTests
    {
        private readonly HttpClient _httpClient;
        private readonly EmployeeApiService _employeeApiService;
        private readonly ILogger<EmployController> _logger;
        private readonly IEmployController _employService;
        private readonly ILogger<WeatherForecastController> _logger2;
        private readonly IConfiguration _configuration;

        public EmployControllerTests(EmployeeApiService employeeApiService, ILogger<EmployController> logger, IEmployController employService, ILogger<WeatherForecastController> logger2, IConfiguration configuration)
        {
            _employeeApiService = employeeApiService;
            _logger = logger;
            _employService = employService;
            _logger2 = logger2;
            _configuration = configuration;
        }

        //[TestMethod()]
        [Fact]
        public void GetTest()
        {
            var mockEmployeeService = new Mock<EmployeeApiService>();
            mockEmployeeService.Setup(service => service.GetAllEmployees())
            .Returns(new List<EmployeeAnnualSalary>
                {
                new EmployeeAnnualSalary { Id = 1, Name = "John Doe",Age=23,AnnualSalary=12000,  Salary=1000,ProfileImage="image" },
                new EmployeeAnnualSalary { Id = 2, Name = "John Doe 2",Age=32,AnnualSalary=15000,  Salary=1500,ProfileImage="image 2" },
                
                });

            var controller = new EmployController(mockEmployeeService.Object);

            // Act
            var result = controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var employees = Assert.IsAssignableFrom<List<EmployeeAnnualSalary>>(okResult.Value);
            Assert.Equals(2, employees.Count);

            EmployeeApiService employeeApiService = new EmployeeApiService(_httpClient);
            EmployController e = new EmployController(employeeApiService,_logger,_employService,_logger2,_configuration);
            e.Get();
            Assert.Fail();
        }
    }
}