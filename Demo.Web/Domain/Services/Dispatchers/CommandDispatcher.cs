﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Web.Domain.Common;
using Demo.Web.Domain.Contracts.Commands;
using Demo.Web.Domain.Services.Commands;
using SimpleInjector;

namespace Demo.Web.Domain.Services.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private Container _container;

        public CommandDispatcher(Container container)
        {
            ThrowIf.Argument.IsNull(container, "container");
            this._container = container;
        }

        public async Task<CommandResult> DispatchAsync<TParameter>(TParameter command) where TParameter : class
        {
            return await _container.GetInstance<IAsyncCommandHandler<TParameter>>().ExecuteAsync(command);
        }

        public CommandResult Dispatch<TParameter>(TParameter command) where TParameter : class
        {
            return _container.GetInstance<ICommandHandler<TParameter>>().Execute(command);
        }
    }
}
