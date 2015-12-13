using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace Demo.Web.Domain.Data
{
    /// <summary>
    /// This exception serves as a replacement to DbEntityValidationException and include the validation errors
    /// in the ToString method
    /// </summary>
    public class DbEntityValidationDetailException : DbEntityValidationException
    {
        public DbEntityValidationDetailException(DbEntityValidationException validationException)
            : base(validationException.Message, validationException.EntityValidationErrors, validationException)
        {
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("Entity validation failed");

            foreach (DbEntityValidationResult entityError in EntityValidationErrors)
            {
                builder.AppendFormat("{0} failed validation\n", entityError.Entry.Entity.GetType());
                foreach (DbValidationError validationError in entityError.ValidationErrors)
                {
                    builder.AppendFormat("- {0} : {1}\n", validationError.PropertyName, validationError.ErrorMessage);
                }
            }

            builder.Append(base.ToString());
            return builder.ToString();
        }
    }
}
