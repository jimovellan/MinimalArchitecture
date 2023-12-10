using FluentValidation;
using MediatR;
using MinimalArchitecture.Common.Extensions;
using MinimalArchitecture.Common.Models;
using MinimalArchitecture.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Architecture.Pipelines
{
    /// <summary>
    /// Pipeline to validate the model with fluent validation
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class ValidatingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidatingPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            
            if(_validators.Any())
            {

                var errors = _validators.Select(s => s.Validate(request))
                                .SelectMany(s => s.Errors)
                                .Where(w => w is not null)
                                .Select(s => new Error(s.ErrorCode, s.ErrorMessage))
                                .AsEnumerable();

                if (errors.HasElements())
                {


                    if (typeof(TResponse).IsAssignableTo(typeof(Result)))
                    {
                        // Invoca el método estático.
                        var result = Activator.CreateInstance(typeof(TResponse));
                        ((Result)result).AddErrors(errors);
                        return (TResponse)result;
                    }    

                }
            }

            return await next();
        }
    }
}
