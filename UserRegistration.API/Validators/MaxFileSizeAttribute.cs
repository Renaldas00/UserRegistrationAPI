﻿using System.ComponentModel.DataAnnotations;

namespace UserRegistration.API.Validators
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return ValidationResult.Success;
            }
            if (value is not IFormFile file)
            {
                return ValidationResult.Success;
            }
            if (file.Length > _maxFileSize)
            {
                return new ValidationResult($"File size should not be larger than {_maxFileSize} bytes");
            }
            return ValidationResult.Success;
        }
    }
}
