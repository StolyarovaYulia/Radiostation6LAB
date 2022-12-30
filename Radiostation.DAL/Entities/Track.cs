using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radiostation.DAL.Entities
{
    public class Track
    {
        [DisplayName("#")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Наименование")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Исполнитель")]
        public int PerformerId { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Жанр")]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Дата выхода")]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Длительность")]
        [RegularExpression(@"\d\d:\d\d", ErrorMessage = "Формат: mm:ss")]
        public string Duration { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("Рейтинг")]
        [Range(1, 5, ErrorMessage = "От 1 до 5")]
        public int Rating { get; set; }


        public Genre Genre { get; set; }

        public Performer Performer { get; set; }

        public List<Translation> Translations { get; set; }
    }
}
