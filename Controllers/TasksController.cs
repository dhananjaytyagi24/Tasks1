using System;
using Microsoft.AspNetCore.Mvc;

namespace Tasks.API1.Controllers
{
	[ApiController]
	[Route("api/v1/tasks")]
	public class TasksController : ControllerBase
	{
		[HttpGet]
		public JsonResult GetTasks()
		{
			return new JsonResult(TaskDataStore.Current.Tasks);
		}

		//[HttpPost]
		//public async Task<IActionResult> CreateTask()
		//{

		//}
	}
}

