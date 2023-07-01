using System;
using Microsoft.AspNetCore.Mvc;
using Tasks.API1.Models;

namespace Tasks.API1.Controllers
{
	[ApiController]
	[Route("api/v1/tasks")]
	public class TasksController : ControllerBase
	{
		[HttpGet]
		public ActionResult<IEnumerable<TaskDto>> GetTasks()
		{
			return Ok(TaskDataStore.Current.Tasks);
		}

		[HttpGet("{taskId}")]
		public ActionResult<TaskDto> GetTask([FromRoute] Guid taskId)
		{
			var task = TaskDataStore.Current.Tasks.Where(x => x.Id == taskId).SingleOrDefault();

			if (task == null)
			{
				return NotFound();
			}

            return Ok(task);
		}

		//[HttpPost]
		//public async Task<IActionResult> CreateTask()
		//{

		//}
	}
}

