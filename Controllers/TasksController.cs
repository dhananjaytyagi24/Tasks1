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

		[HttpGet("{taskId}", Name ="GetTask")]
		public ActionResult<TaskDto> GetTask([FromRoute] Guid taskId)
		{
			var task = TaskDataStore.Current.Tasks.Where(x => x.Id == taskId).SingleOrDefault();

			if (task == null)
			{
				return NotFound($"Task with Id: {taskId} could not be found");
			}

			return Ok(task);
		}

		[HttpPost]
		public IActionResult CreateTask(CreateTaskDto createTaskDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Invalid request");
			}
			var newTask = new TaskDto();
			newTask.Id = Guid.NewGuid();
			newTask.CreatedAt = DateTime.Now;
			newTask.IsCompleted = false;
			newTask.Description = createTaskDto.Description;
			newTask.Importance = createTaskDto.Importance;
			newTask.DaysTaken = newTask.DaysTaken;

			TaskDataStore.Current.Tasks.Add(newTask);

			return CreatedAtRoute("GetTask", new { taskId = newTask.Id }, newTask);

		}

		[HttpPut("{taskId}")]
		public IActionResult UpdateTask([FromRoute]Guid taskId, UpdateTaskDto updateTaskDto)
		{
			var task = TaskDataStore.Current.Tasks.Where(x => x.Id == taskId).SingleOrDefault();

			if (task == null)
			{
				return NotFound($"Task with Id: {taskId} could not be found");
			}

			task.Description = updateTaskDto.Description;
			task.Importance = updateTaskDto.Importance;
			task.DaysTaken = updateTaskDto.DaysTaken;
			task.IsCompleted = updateTaskDto.IsCompleted;

			return NoContent();
		}

		[HttpDelete("{taskId}")]
		public IActionResult DeleteTask([FromRoute]Guid taskId)
		{
            var task = TaskDataStore.Current.Tasks.Where(x => x.Id == taskId).SingleOrDefault();

            if (task == null)
            {
                return NotFound($"Task with Id: {taskId} could not be found");
            }

			TaskDataStore.Current.Tasks.Remove(task);

			return NoContent();
        }
	}
}

