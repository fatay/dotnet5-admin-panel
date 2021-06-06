using ProgrammersBlog.Shared.Utilities.Results.Types;
using System;

namespace ProgrammersBlog.Shared.Utilities.Results.Abstract
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get; }  // "ResultStatus.Success" veya "ResultStatus.Error" şeklinde kullanacağız.
        public string Message { get; }
        public Exception Exception { get; }
    }
}
