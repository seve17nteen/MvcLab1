using Microsoft.AspNetCore.Mvc;

namespace MvcLab1.Controllers
{
    [Route("music")]
    public class MusicController : Controller
    {
        // 1️⃣ Список исполнителей
        [Route("artists")]
        public IActionResult Artists()
        {
            ViewData["Title"] = "Исполнители";

            ViewBag.Artists = new string[]
            {
                "Imagine Dragons",
                "Adele",
                "Coldplay"
            };

            return View();
        }

        // 2️⃣ Информация об исполнителе
        [Route("artist/{artistName}")]
        public IActionResult Artist(string artistName)
        {
            if (string.IsNullOrEmpty(artistName))
                return Content("Исполнитель не найден");

            ViewData["Title"] = $"Исполнитель {artistName}";
            ViewBag.Name = artistName;
            ViewBag.Genre = "Pop / Rock";
            ViewBag.Year = 2010;

            return View();
        }

        // 3️⃣ Альбомы исполнителя
        [Route("albums/{artistId:int}")]
        public IActionResult Albums(int artistId)
        {
            if (artistId < 1)
                return Content("Неверный ID исполнителя");

            ViewData["Title"] = "Альбомы";

            ViewBag.Albums = new string[]
            {
                "Album 1 (2020)",
                "Album 2 (2022)",
                "Album 3 (2024)"
            };

            return View();
        }

        // 4️⃣ Кастомный маршрут
        [Route("top")]
        public IActionResult Top()
        {
            ViewData["Title"] = "Топ исполнителей";

            ViewBag.TopArtists = new string[]
            {
                "Taylor Swift",
                "Drake",
                "The Weeknd"
            };

            return View();
        }
    }
}