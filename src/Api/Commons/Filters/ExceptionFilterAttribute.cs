using System;
using System.Collections.Generic;
using System.Linq;
using Application.Commons.Exceptions;
using Application.Commons.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Commons.Filters;

public class ExceptionFilter : ExceptionFilterAttribute
{
    private readonly IDictionary<string, Action<ExceptionContext>> _exceptionHandlers;

    private string _customMessage = string.Empty;
    
    private string _customErrorMessage = string.Empty;

    private List<string> _customValidationMessage = [];

    public ExceptionFilter()
    {
        _exceptionHandlers = new Dictionary<string, Action<ExceptionContext>>
        {
            { nameof(ValidationException), HandleValidationException },
            { nameof(ErrorInvalidException), HandleErrorInvalidException },
            { nameof(AutoMapperMappingException), HandleAutoMapperMappingException },
            { nameof(NotFoundException), HandleNotFoundException },
            { nameof(ForbiddenException), HandleForbiddenAccessException },
        };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);
        
        if (!string.IsNullOrEmpty(_customErrorMessage))
        {
            var errorResponse = Response.Fail<object>(_customErrorMessage);

            context.Result = new JsonResult(errorResponse);
        }
        
        if(_customValidationMessage != null && (!string.IsNullOrEmpty(_customMessage) || _customValidationMessage.Any()))
        {
            var message = _customMessage;
            
            if (!string.IsNullOrEmpty(context.Exception.InnerException?.ToString()))
                message = $"{_customMessage} - {context.Exception.InnerException}";
        
            var errorResponse = Response.Error<object>(message, _customValidationMessage);

            context.Result = new JsonResult(errorResponse);
        }

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        var type = context.Exception.GetType().Name;
        
        if (_exceptionHandlers.TryGetValue(type, out var handler))
        {
            handler.Invoke(context);
            return;
        }

        HandleUnknownException(context);
    }

    private void HandleValidationException(ExceptionContext context)
    {
        var exception = context.Exception as ValidationException;

        _customValidationMessage = exception?.Errors;

        context.ExceptionHandled = true;
    }
    
    private void HandleErrorInvalidException(ExceptionContext context)
    {
        var exception = context.Exception as ErrorInvalidException;

        _customErrorMessage = exception?.Errors.FirstOrDefault();

        context.ExceptionHandled = true;
    }
    
    private void HandleAutoMapperMappingException(ExceptionContext context)
    {
        var exception = context.Exception as AutoMapperMappingException;

        _customMessage = exception?.Message;

        context.ExceptionHandled = true;
    }

    private void HandleNotFoundException(ExceptionContext context)
    {
        var exception = context.Exception as NotFoundException;

        _customMessage = exception?.Message;

        context.ExceptionHandled = true;
    }

    private void HandleForbiddenAccessException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status403Forbidden,
            Title = "Forbidden",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
        };

        _customMessage = "Error " + details.Status + ": " + details.Title;

        context.ExceptionHandled = true;
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        _customMessage = context.Exception.Message;

        context.ExceptionHandled = true;
    }
}