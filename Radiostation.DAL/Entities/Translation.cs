using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Radiostation.DAL.Attribures;

namespace Radiostation.DAL.Entities
{
    public class Translation
    {
        [DisplayName("#")]
        public int Id { get; set; }

        [DisplayName("Время")]
        [Required(ErrorMessage = "Обязательное поле")]
        [RegularExpression(@"\d\d:\d\d", ErrorMessage = "Формат: hh:mm")]
        [Time]
        public string Time { get; set; }

        [DisplayName("Дата")]
        [Required(ErrorMessage = "Обязательное поле")]
        public DateTime Date { get; set; }

        [DisplayName("Вещатель")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int EmployeeId { get; set; }

        [DisplayName("Трек")]
        [Required(ErrorMessage = "Обязательное поле")]
        public int TrackId { get; set; }


        public Track Track { get; set; }

        public Employee Employee { get; set; }
    }
}
