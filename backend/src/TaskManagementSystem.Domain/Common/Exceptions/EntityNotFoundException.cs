using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(Type entityType, object id)
            : this(entityType, id, null)
        {

        }
        public EntityNotFoundException(Type entityType, object id, Exception innerException) : base(
                   id == null
                   ? $"There is no such an entity given id. Entity type: {entityType.FullName}"
                   : $"There is no such an entity. Entity type: {entityType.FullName}, id: {id}", innerException)
        {
        }
    }
}