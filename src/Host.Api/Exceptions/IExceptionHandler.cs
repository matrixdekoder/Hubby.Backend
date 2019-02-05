using System;
using Microsoft.AspNetCore.Mvc;

namespace Host.Api.Exceptions
{
    public interface IExceptionHandler
    {
        IActionResult Handle(Exception e);
    }
}