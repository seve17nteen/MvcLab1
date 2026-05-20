using MvcLab1.Models;

namespace MvcLab1.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            // ===== ТЕСТОВЫЕ ТОВАРЫ (если нужны) =====
            if (!context.Products.Any())
            {
                var products = new Product[]
                {
                    new Product
                    {
                        Name = "Ноутбук ASUS",
                        Price = 75000m,
                        Category = "Электроника",
                        Description = "Игровой ноутбук",
                        CreatedDate = DateTime.Now.AddDays(-30),
                        InStock = true
                    },
                    new Product
                    {
                        Name = "Смартфон Samsung",
                        Price = 45000m,
                        Category = "Электроника",
                        Description = "Galaxy S23",
                        CreatedDate = DateTime.Now.AddDays(-15),
                        InStock = true
                    },
                    new Product
                    {
                        Name = "Наушники Sony",
                        Price = 8000m,
                        Category = "Аксессуары",
                        Description = "Беспроводные наушники",
                        CreatedDate = DateTime.Now.AddDays(-5),
                        InStock = false
                    }
                };
                context.Products.AddRange(products);
            }

            // ===== ТЕСТОВЫЕ ТРЕНИРОВКИ (МИНИМУМ 3 ЗАПИСИ) =====
            if (!context.Workouts.Any())
            {
                var workouts = new Workout[]
                {
                    new Workout
                    {
                        Name = "Утренняя зарядка",
                        Type = "Кардио",
                        Duration = 15,
                        Calories = 100,
                        Difficulty = "Низкая",
                        Instructor = "Анна Иванова",
                        Date = DateTime.Now.AddDays(-10)
                    },
                    new Workout
                    {
                        Name = "Силовая тренировка",
                        Type = "Силовая",
                        Duration = 45,
                        Calories = 400,
                        Difficulty = "Высокая",
                        Instructor = "Михаил Петров",
                        Date = DateTime.Now.AddDays(-5)
                    },
                    new Workout
                    {
                        Name = "Йога для начинающих",
                        Type = "Йога",
                        Duration = 60,
                        Calories = 250,
                        Difficulty = "Средняя",
                        Instructor = "Елена Смирнова",
                        Date = DateTime.Now.AddDays(-3)
                    },
                    new Workout
                    {
                        Name = "Интервальная тренировка HIIT",
                        Type = "Кардио",
                        Duration = 30,
                        Calories = 350,
                        Difficulty = "Высокая",
                        Instructor = "Дмитрий Козлов",
                        Date = DateTime.Now.AddDays(-1)
                    },
                    new Workout
                    {
                        Name = "Пилатес",
                        Type = "Пилатес",
                        Duration = 50,
                        Calories = 180,
                        Difficulty = "Средняя",
                        Instructor = "Ольга Новикова",
                        Date = DateTime.Now
                    }
                };
                context.Workouts.AddRange(workouts);
            }

            context.SaveChanges();
        }
    }
}