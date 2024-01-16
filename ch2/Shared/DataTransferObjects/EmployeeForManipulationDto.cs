﻿

using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public abstract record EmployeeForManipulationDto
{
    [Required(ErrorMessage = "Employee name is a required field")]
    [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 chars")]
    public string? Name { get; init; }

    [Range(18, int.MaxValue, ErrorMessage = "Age is required and it can't be lower than 18")]
    public int Age { get; init; }

    [Required(ErrorMessage = "Position is a required field")]
    [MaxLength(20, ErrorMessage = "Maximum length for the Position is 20 chars")]
    public string? Position { get; init; }
}
