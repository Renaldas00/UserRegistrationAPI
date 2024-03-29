﻿using System.ComponentModel.DataAnnotations;
using UserRegistration.API.Validators;

namespace UserRegistration.API.DTOS.Requests
{
    public class UpdateLastNameRequestDTO
    {
        [Required]
        [LastNameValidator]
        public string LastName {  get; set; }
        
    }
}
