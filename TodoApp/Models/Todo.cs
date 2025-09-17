using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class Todo
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage ="Le titre est obligatoire")]
        [StringLength(100, ErrorMessage ="Le titre ne peut pas depasser 100 caracteres")]
        public string TodoTitle { get; set; }

        [StringLength(500, ErrorMessage ="La description est trop longue")]
        public string TodoDescription { get; set; }

        public bool IsDone { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; } // date limite optionnelle

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
