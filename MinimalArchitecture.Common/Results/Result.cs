using MinimalArchitecture.Common.Extensions;
using MinimalArchitecture.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MinimalArchitecture.Common.Results
{
    public  class Result
    {
        public Result()
        {
            
        }


        [JsonIgnore]
        public CancellationToken CancellationToken { get; }

        private IEnumerable<Error> _errors = new List<Error>();
        public bool IsFailed => _errors.HasElements();

        public IReadOnlyCollection<Error> Errors => _errors.ToList().AsReadOnly();

        protected internal Result(IEnumerable<Error> errors, CancellationToken cancellationToken)
        {
            AddErrors(errors);
            CancellationToken = cancellationToken;
        }

        protected internal Result(CancellationToken cancellationToken)
        {
            CancellationToken = cancellationToken;
        }

        public void AddError(Error error)
        {
            _errors = _errors.Append(error);
        }

        public void AddErrors(IEnumerable<Error> errors)
        {
            _errors = errors.Aggregate(_errors, (acum, error) => acum = acum.Append(error));
        }

        public static Result Fail(IEnumerable<Error> errors, CancellationToken cancellationToken = default)
        {
            return new Result(errors, cancellationToken);
        }
        public static Result Fail(Error error, CancellationToken cancellationToken = default)
        {
            return new Result(new[] { error }, cancellationToken);
        }

        public static Result Ok(CancellationToken cancellationToken = default)
        {
            return new Result(cancellationToken);
        }


        public static Result<T> Fail<T>(IEnumerable<Error> errors, CancellationToken cancellationToken)
        {
            return new Result<T>(errors, cancellationToken);
        }

        public static Result<T> Ok<T>(T value, CancellationToken cancellationToken = default)
        {
            return new Result<T>(value,cancellationToken);
        }

    }

    public class Result<TValue> : Result
    {
        public Result():base()
        {
            Value = default;
        }
        public TValue Value { get; private set; }

        protected internal Result(IEnumerable<Error> errors, CancellationToken cancellationToken = default)
            :base(errors, cancellationToken)
        {
            
        }

        protected internal Result(TValue value, CancellationToken cancellationToken = default):base(cancellationToken)
        {
            Value = value;
        }
    }
}
