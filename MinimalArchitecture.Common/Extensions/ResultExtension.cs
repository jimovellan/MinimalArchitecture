using MinimalArchitecture.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Common.Extensions
{
    public static class ResultExtension
    {
        
        public static Result Then(this Result result, Func<CancellationToken,Result> func)
        {
            if (result.IsFailed)
            {
                return Result.Fail(result.Errors, result.CancellationToken);
            }

            return func(result.CancellationToken);

        }

        public static Result<TOut> Then<TIn, TOut>(this Result<TIn> result, Func<TIn,CancellationToken,Result<TOut>> func)
        {
            if (result.IsFailed)
            {
                return Result.Fail<TOut>(result.Errors, result.CancellationToken);
            }

            return func(result.Value, result.CancellationToken);
        }

        public static Result<TOut> Then<TOut>(this Result result, Func<CancellationToken, Result<TOut>> func)
        {
            if (result.IsFailed)
            {
                return Result.Fail<TOut>(result.Errors, result.CancellationToken);
            }

            return func(result.CancellationToken);
        }

        public static Result Then<TIn>(this Result<TIn> result, Func<TIn,CancellationToken, Result> func)
        {
            if (result.IsFailed)
            {
                return Result.Fail(result.Errors, result.CancellationToken);
            }

            return func(result.Value, result.CancellationToken);
        }


        public async static Task<Result> ThenAsync(this Result result, Func<CancellationToken, Task<Result>> func)
        {
            if (result.IsFailed)
            {
                return Result.Fail(result.Errors, result.CancellationToken);
            }

            return await func(result.CancellationToken);

        }

        public async static Task<Result<TOut>> ThenAsync<TIn, TOut>(this Result<TIn> result, Func<TIn, CancellationToken, Task<Result<TOut>>> func)
        {
            if (result.IsFailed)
            {
                return Result.Fail<TOut>(result.Errors, result.CancellationToken);
            }

            return await func(result.Value, result.CancellationToken);
        }

        public async static Task<Result<TOut>> ThenAsync<TOut>(this Result result, Func<CancellationToken, Task<Result<TOut>>> func)
        {
            if (result.IsFailed)
            {
                return Result.Fail<TOut>(result.Errors, result.CancellationToken);
            }

            return await func(result.CancellationToken);
        }

        public async static Task<Result> ThenAsync<TIn>(this Result<TIn> result, Func<TIn, CancellationToken, Task<Result>> func)
        {
            if (result.IsFailed)
            {
                return Result.Fail(result.Errors, result.CancellationToken);
            }

            return await func(result.Value, result.CancellationToken);
        }
    }
}
