using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Tasks.API1.Models;

namespace Tasks.API1.Controllers
{
	[ApiController]
	[Route("api/v1/tasks")]
	public class TasksController : ControllerBase
	{
		private readonly ILogger<TasksController> _logger;

		public TasksController(ILogger<TasksController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public ActionResult<IEnumerable<TaskDto>> GetTasks()
		{
			return Ok(TaskDataStore.Current.Tasks);
		}

		[HttpGet("{taskId}", Name ="GetTask")]
		public ActionResult<TaskDto> GetTask([FromRoute] Guid taskId)
		{
			try
			{
				throw new Exception("new exception");
                var task = TaskDataStore.Current.Tasks.Where(x => x.Id == taskId).SingleOrDefault();

                if (task == null)
                {
                    _logger.LogInformation($"Task with Id: {taskId} could not be found");
                    return NotFound($"Task with Id: {taskId} could not be found");
                }

                return Ok(task);
            }
			catch (Exception ex)
			{
				_logger.LogCritical(ex, $"Exception thrown while getting task with Id: {taskId}");
				return StatusCode(500);
			}
		}

		[HttpPost]
		public IActionResult CreateTask(CreateTaskDto createTaskDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Invalid create request");
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

		[HttpPatch("{taskId}")]
		public IActionResult PatchTask([FromRoute] Guid taskId, JsonPatchDocument<UpdateTaskDto> jsonPatchDocument)
		{
            var task = TaskDataStore.Current.Tasks.Where(x => x.Id == taskId).SingleOrDefault();
            if (task == null)
            {
                return NotFound($"Task with Id: {taskId} could not be found");
            }

			UpdateTaskDto updateTaskDto = new UpdateTaskDto()
			{
				Description = task.Description,
				Importance = task.Importance,
				DaysTaken = task.DaysTaken,
				IsCompleted = task.IsCompleted
			};

			// Sending ModelState as a parameter to check for any errors after applying the json patch doc 
			jsonPatchDocument.ApplyTo(updateTaskDto, ModelState);
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

