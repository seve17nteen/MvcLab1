using System.ComponentModel.DataAnnotations;

namespace MvcLab1.Models
{
    public class Workout
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(100)]
        [Display(Name = "Название")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Тип тренировки обязателен")]
        [Display(Name = "Тип тренировки")]
        public string Type { get; set; } = string.Empty;

        [Range(5, 180, ErrorMessage = "Длительность должна быть от 5 до 180 минут")]
        [Display(Name = "Длительность (мин)")]
        public int Duration { get; set; }

        [Range(10, 2000, ErrorMessage = "Калории должны быть от 10 до 2000")]
        [Display(Name = "Калории")]
        public int Calories { get; set; }

        [Display(Name = "Сложность")]
        public string Difficulty { get; set; } = string.Empty;

        [Display(Name = "Инструктор")]
        public string Instructor { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        public double GetIntensity()
        {
            if (Duration == 0) return 0;
            return (double)Calories / Duration;
        }
    }
}
