using System;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Exceptions
{
    public interface IExceptionHandler
    {
        IActionResult Handle(Exception e);
    }
}