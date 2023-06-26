using System;
using System.ComponentModel.DataAnnotations;
using Tasks.API1.Enums;

namespace Tasks.API1.Models
{
	public class Task
	{
		[Required]
		public string Description { get; set; } = string.Empty;

        [Required]
        public ImportanceEnum Importance { get; set; }

        [Required]
        public DateTime TimeTaken { get; set; }

        public bool IsCompleted { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime TargetDate { get; set; }
	}
}

