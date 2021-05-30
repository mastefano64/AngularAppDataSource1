using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppStudentiCommon.Model.ViewModel
{
    public class TeachersViewModel
    {
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "Name mandatory")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname mandatory")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Address mandatory")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Cap mandatory")]
        public string Cap { get; set; }

        [Required(ErrorMessage = "City mandatory")]
        public string City { get; set; }

        [Required(ErrorMessage = "Province mandatory")]
        public string Province { get; set; }
    }
}
