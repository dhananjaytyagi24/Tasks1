using System;
using Tasks.API1.Models;

namespace Tasks.API1.Repository
{
	public interface ITaskRepository
	{
		public IEnumerable<TaskDto> GetTasksAsync();

		public TaskDto GetTask(string taskId);
		//public Task CreateTaskAsync();
	}
}

