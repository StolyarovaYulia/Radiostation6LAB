using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Radiostation.DAL.Attribures
{
    public class TimeAttribute : ValidationAttribute
    {
        protected const byte MaxHours = 23;
        protected const byte MinHours = 0;

        protected const byte MaxMinutes = 59;
        protected const byte MinMinutes = 0;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var stringValue = (string) value;
            var times = stringValue.Split(":")
                .Select(int.Parse)
                .ToList();

            var minutes = times[1];
            var hours = times[0];

            return minutes is >= MinMinutes and <= MaxMinutes && hours is >= MinHours and <= MaxHours
                ? ValidationResult.Success
                : new ValidationResult($"Время должно быть больше {MinHours}:{MinMinutes} и меньше {MaxHours}:{MaxMinutes}");
        }
    }
}