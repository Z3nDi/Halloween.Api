using System.ComponentModel.DataAnnotations;

namespace Halloween.Api.Models
{
    public class Monster
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Range(1, 10)]
        public int CreepinessLevel { get; set; }

        [Required]
        [StringLength(200)]
        public string? HauntedLocation { get; set; }

        [StringLength(500)]
        public string? SpecialRule { get; set; }
    }
}