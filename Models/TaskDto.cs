using System;
using System.ComponentModel.DataAnnotations;
using Tasks.API1.Enums;

namespace Tasks.API1.Models
{
	public class TaskDto
	{
		public Guid Id { get; set; }

		[Required]
		public string Description { get; set; } = string.Empty;

        [Required]
        public ImportanceEnum Importance { get; set; }

        [Required]
        public int DaysTaken { get; set; }

        public bool IsCompleted { get; set; }

		public DateTime CreatedAt { get; set; }
	}
}

