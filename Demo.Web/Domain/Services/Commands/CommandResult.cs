using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Web.Domain.Common;

namespace Demo.Web.Domain.Services.Commands
{
    public class CommandResult
    {
        private List<string> _errors;

        protected CommandResult()
        {
            this._errors = new List<string>();
        }

        public static CommandResult Success()
        {
            CommandResult result = new CommandResult();
            result.Succeeded = true;
            return result;
        }

        public static CommandResult Failed()
        {
            CommandResult result = new CommandResult();
            result.Succeeded = false;
            return result;
        }

        public static CommandResult Failed(string errorMessage)
        {
            return Failed().AddError(errorMessage);
        }

        public static CommandResult Failed(IEnumerable<string> errorMessages)
        {
            return Failed().AddErrors(errorMessages);
        }

        public bool Succeeded
        {
            get;
            private set;
        }

        public IEnumerable<string> Errors
        {
            get
            {
                return _errors;
            }
        }

        public CommandResult AddError(string error)
        {
            ThrowIf.Argument.IsNullOrEmpty(error, "error");
            _errors.Add(error);
            return this;
        }

        public CommandResult AddErrors(IEnumerable<string> errors)
        {
            ThrowIf.Argument.IsNull(errors, "errors");
            _errors.AddRange(errors);
            return this;
        }
    }
}
