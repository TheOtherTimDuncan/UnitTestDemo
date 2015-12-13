using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Web.Domain.Contracts;
using Demo.Web.Domain.Contracts.Commands;
using Demo.Web.Domain.Data.Extensions;
using Demo.Web.Domain.Data.Models;
using Demo.Web.Domain.Services.Commands;
using Demo.Web.Sections.Home.Models;

namespace Demo.Web.Sections.Home.CommandHandlers
{
    public class BlogCommandHandler : IAsyncCommandHandler<BlogModel>
    {
        private IStorageContext<Blog> _storageContext;

        public BlogCommandHandler(IStorageContext<Blog> storageContext)
        {
            this._storageContext = storageContext;
        }

        public async Task<CommandResult> ExecuteAsync(BlogModel command)
        {
            Blog blog = await _storageContext.Entities.FindByIDAsync(command.ID);
            if (blog == null)
            {
                return CommandResult.Failed("Blog ID " + command.ID + " not found");
            }

            blog.Name = command.Name;

            await _storageContext.SaveChangesAsync();

            return CommandResult.Success();
        }
    }
}
