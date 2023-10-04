using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using AireSpringTechTest.Data;
using Microsoft.AspNetCore.Mvc;
using AireSpringTechTest.Models;
using AireSpringTechTest.Repositories.Contracts;
using AireSpringTechTest.Utils;

namespace AireSpringTechTest.Controllers;

public partial class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IEmployeeRecordRepository _employeeRecordRepository;

    public HomeController(ILogger<HomeController> logger, IEmployeeRecordRepository employeeRecordRepository)
    {
        _logger = logger;
        _employeeRecordRepository = employeeRecordRepository;
    }

    public async Task<IActionResult> Index()
    {
        IList<EmployeeRecord> employeeRecords = await _employeeRecordRepository.GetEmployeeRecords();
        
        return View(employeeRecords);
    }

    public IActionResult Edit()
    {
        EmployeeViewModel employeeViewModel = new()
        {
            HireDate = DateTime.Today.ToString(AppExtensions.DefaultDateTemplate)
        };
            
        return View(employeeViewModel);
    }
    
    [HttpGet]
    [Route("/Home/Edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        EmployeeRecord? employeeRecord = await _employeeRecordRepository.GetEmployeeRecord(id);
        if (employeeRecord == null)
            return RedirectToAction("Index");

        EmployeeViewModel employeeViewModel = new()
        {
            IsEdit = true,
            Id = employeeRecord.EmployeeId,
            FirstName = employeeRecord.EmployeeFirstName,
            LastName = employeeRecord.EmployeeLastName,
            PhoneNumber = employeeRecord.EmployeePhone,
            ZipCode = employeeRecord.EmployeeZip,
            HireDate = employeeRecord.HireDate.ToString(AppExtensions.DefaultDateTemplate)
        };

        return View(employeeViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromForm]EmployeeViewModel model)
    {
        // If not valid, return the model and display the Add View again
        if (!ModelState.IsValid) return View(model);
        
        // Since the model binder is not validating the date correctly in some cases. Let's validate that here.
        if (!DateTime.TryParseExact(model.HireDate, AppExtensions.DefaultDateTemplate, null, DateTimeStyles.None , out DateTime date))
        {
            ModelState.AddModelError(nameof(EmployeeViewModel.HireDate), "The date provided is invalid");
            return View(model);
        }

        // Model is valid, save into the database.
        EmployeeRecord employeeRecord = new()
        {
            EmployeeLastName = model.LastName,
            EmployeeFirstName = model.FirstName,
            EmployeePhone = model.PhoneNumber,
            EmployeeZip = model.ZipCode,
            HireDate = date
        };
        
        // We are storing the Phone Numbers without masks. Let the UI do its job to format it
        Regex regex = PhoneNumberRegex();
        employeeRecord.EmployeePhone = regex.Replace(employeeRecord.EmployeePhone, "");
        
        if (model.IsEdit)
        {
            employeeRecord.EmployeeId = model.Id;
            await _employeeRecordRepository.UpdateEmployeeRecord(employeeRecord);
        }
        else
        {
            await _employeeRecordRepository.InsertEmployeeRecord(employeeRecord);
        }
        
        return RedirectToAction("Index", "Home");

    }

    [HttpGet]
    [Route("/Home/Delete/{id:int}")]
    public async Task<IActionResult> Delete(int? id)
    {
        // If the ID is null, redirect to home.
        if (id == null)
            return RedirectToAction("Index");

        // Check if the employee record exists, If it does, We can deleted
        EmployeeRecord? employeeRecord = await _employeeRecordRepository.GetEmployeeRecord(id.Value);

        // If the employee has been deleted or doesn't exists, We can handle a warning/error message here.
        if (employeeRecord == null) 
            return RedirectToAction("Index");

        // Let's delete the employee record.
        await _employeeRecordRepository.DeleteEmployeeRecord(id.Value);
        
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [GeneratedRegex("[^\\d]")]
    private static partial Regex PhoneNumberRegex();
}