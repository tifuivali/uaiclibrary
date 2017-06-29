using System;
using System.Collections.Generic;

namespace UaicLibrary.Common.Error
{
    internal sealed class ResultCommonLogic
    {
        public bool IsFailure { get; }
        public bool IsSuccess => !IsFailure;

        public List<Error> Errors { get;  }

        public ResultCommonLogic(bool isFailure)
        {
            Errors = new List<Error>();
            IsFailure = isFailure;
        }

        public ResultCommonLogic(bool isFailure, IEnumerable<Error> errors)
        {
            Errors = new List<Error>();
            Errors.AddRange(errors);
            IsFailure = isFailure;
        }


        public void AddError(Error error)
        {
            Errors.Add(error);
        }

        public void AddRange(IEnumerable<Error> errors)
        {
            Errors.AddRange(errors);
        }
    }
}
