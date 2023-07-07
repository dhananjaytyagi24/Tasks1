using System;
using System.ComponentModel.DataAnnotations;
using Tasks.API1.Enums;

namespace Tasks.API1.Models
{
	public class UpdateTaskDto
	{
        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public ImportanceEnum Importance { get; set; }

        [Required]
        public DateTime TimeTaken { get; set; }

        public DateTime TargetDate { get; set; }

        public bool IsCompleted { get; set; }
    }
}

