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

		[HttpPost]
		public IActionResult CreateTask(CreateTaskDto createTaskDto)
		{
			var newTask = new TaskDto();
			newTask.Id = Guid.NewGuid();
			newTask.CreatedAt = DateTime.Now;
			newTask.IsCompleted = false;
			newTask.Description = createTaskDto.Description;
			newTask.Importance = createTaskDto.Importance;
			newTask.TimeTaken = newTask.TimeTaken;

			TaskDataStore.Current.Tasks.Add(newTask);

			return NoContent();
			//return CreatedAtRoute($"api/v1/tasks/{newTask.Id}", newTask);

		}

		[HttpPut("{taskId}")]
		public IActionResult UpdateTask([FromRoute]Guid taskId, UpdateTaskDto updateTaskDto)
		{
			var task = TaskDataStore.Current.Tasks.Where(x => x.Id == taskId).SingleOrDefault();

			if (task == null)
			{
				return NotFound();
			}

			task.Description = updateTaskDto.Description;
			task.Importance = updateTaskDto.Importance;
			task.TimeTaken = updateTaskDto.TimeTaken;
			task.TargetDate = updateTaskDto.TargetDate;
			task.IsCompleted = updateTaskDto.IsCompleted;

			return NoContent();
		}
	}
}

