using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountriesManager.Core.Entities
{
    public class Country
    {
        [Key]
        public Guid CountryId { get; set; }

        [Required(ErrorMessage = "Country Name can't be blank")]
        public string? CountryName { get; set; }
    }
}
