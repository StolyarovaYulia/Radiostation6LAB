using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radiostation.DAL.Entities
{
    public class Employee
    {
        [DisplayName("#")]
        public int Id { get; set; }

        [DisplayName("Имя")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string FirstName { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string LastName { get; set; }

        [DisplayName("Образование")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Education { get; set; }

        [DisplayName("Должность")]
        [Required(ErrorMessage = "Обязательное поле")]
        public string Role { get; set; }


        public List<Translation> Translations { get; set; }
    }
}
