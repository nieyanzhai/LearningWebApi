using System.ComponentModel.DataAnnotations;

namespace LearningWebApi.Entity;

public class Project
{
    [Required]
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    public string Description { get; set; }
    
}