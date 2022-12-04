using iv_lab5.Models;
using LabsLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace db_lab5.Controllers
{
	[Authorize]
	public class LabsController : Controller
	{
		private readonly ILogger<LabsController> _logger;

		public LabsController(ILogger<LabsController> logger)
		{
			_logger = logger;
		}

		public IActionResult Lab1()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Lab1(Lab1DataModel model)
		{
			var labRunner = new Lab1Runner(model.InputLine);

			try
			{
				model.Calculated = labRunner.RunLab();
			}
			catch (ArgumentException e)
			{
				model.ErrorValue = e.Message;
			}
			catch (Exception e)
			{
				model.ErrorValue = e.Message;
			}

			return View(model);
		}

		public IActionResult Lab2()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Lab2(Lab2DataModel model)
		{
			var labRunner = new Lab2Runner(model.InputNumber);

			try
			{
				model.Calculated = labRunner.RunLab();
			}
			catch (ArgumentException e)
			{
				model.ErrorValue = e.Message;
			}
			catch (Exception e)
			{
				model.ErrorValue = e.Message;
			}

			return View(model);
		}

		public IActionResult Lab3()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Lab3(Lab3DataModel model)
		{
			var labRunner = new Lab3Runner(model.FirstInputLine, model.SecondInputLine, model.ThirdInputLine, model.FourthInputLine,
				model.FifthInputLine, model.SixthInputLine, model.SeventhInputLine, model.EighthInputLine);

			try
			{
				model.Calculated = labRunner.RunLab();
			}
			catch (ArgumentException e)
			{
				model.ErrorValue = e.Message;
			}
			catch (Exception e)
			{
				model.ErrorValue = e.Message;
			}

			return View(model);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}