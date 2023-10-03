﻿using System.Diagnostics;
using AireSpringTechTest.Data;
using Microsoft.AspNetCore.Mvc;
using AireSpringTechTest.Models;
using AireSpringTechTest.Repositories.Contracts;

namespace AireSpringTechTest.Controllers;

public class HomeController : Controller
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

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}