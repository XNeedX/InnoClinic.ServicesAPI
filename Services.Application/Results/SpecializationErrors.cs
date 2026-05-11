using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Application.Results;

public static class SpecializationErrors
{
    public static readonly Error SpecializationNotFound = new(
        "Specialization.NotFound",
        "The specialization was not found.",
        ErrorType.NotFound
    );
}
