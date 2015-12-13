using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Web.Domain.Services.Commands;

namespace Demo.Web.Domain.Contracts.Commands
{
    public interface ICommandHandler<in TParameter> where TParameter : class
    {
        CommandResult Execute(TParameter command);
    }

    public interface IAsyncCommandHandler<in TParameter> where TParameter : class
    {
        Task<CommandResult> ExecuteAsync(TParameter command);
    }
}
