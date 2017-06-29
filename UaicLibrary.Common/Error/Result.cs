using System;
using System.Collections.Generic;
using System.Linq;

namespace UaicLibrary.Common.Error
{
    public struct Result
    {
        private static readonly Result OkResult = new Result(false);

        private readonly ResultCommonLogic logic;

        public bool IsFailure => logic.IsFailure;
        public bool IsSuccess => logic.IsSuccess;
        public IEnumerable<Error> Errors => logic.Errors;

        private Result(bool isFailure)
        {
            logic = new ResultCommonLogic(isFailure);
        }

        private Result(bool isFailure, Error error)
        {
            logic = new ResultCommonLogic(isFailure);
            logic.AddError(error);
        }

        private Result(bool isFailure, IEnumerable<Error> errors)
        {
            logic = new ResultCommonLogic(isFailure, errors);
        }

        public static Result Ok()
        {
            return OkResult;
        }

        public static Result Fail(string errorMessage)
        {
            return new Result(true, Error.New(errorMessage));
        }

        public static Result Fail(IEnumerable<Error> errors)
        {
            return new Result(true, errors);
        }

        public static Result<T> Fail<T>(IEnumerable<Error> errors)
        {
            return new Result<T>(true, default(T), errors);
        }


        public static Result Fail(Error error)
        {
            return new Result(true, error);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(false, value, new List<Error>());
        }

        public static Result<T> Fail<T>(string errorMessage)
        {
            return new Result<T>(true, default(T), Error.New(errorMessage));
        }

        public static Result Combine(params Result[] results)
        {
            var allFailResults = results.Where(x => x.IsFailure).ToList();
            if (!allFailResults.Any())
                return Ok();
            var combinedErrors = new List<Error>();
            foreach (var result in allFailResults)
            {
                combinedErrors.AddRange(result.Errors);
            }

            return new Result(true, combinedErrors);
        }

    }


    public struct Result<T>
    {
        private readonly ResultCommonLogic logic;

        public bool IsFailure => logic.IsFailure;
        public bool IsSuccess => logic.IsSuccess;
        public IEnumerable<Error> Errors => logic.Errors;

        public T Value { get; }

        internal Result(bool isFailure, T value, Error error)
        {
            if (!isFailure && value == null)
                throw new ArgumentNullException(nameof(value));

            logic = new ResultCommonLogic(isFailure);
            logic.AddError(error);
            Value = value;
        }

        internal Result(bool isFailure, T value, IEnumerable<Error> errors)
        {
            if (!isFailure && value == null)
                throw new ArgumentNullException(nameof(value));

            logic = new ResultCommonLogic(isFailure);
            logic.AddRange(errors);
            Value = value;
        }

        public static implicit operator Result(Result<T> result)
        {
            if (result.IsSuccess)
                return Result.Ok();
            return Result.Fail(result.Errors);
        }
    }
}
