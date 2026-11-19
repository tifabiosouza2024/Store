using FS.Store.Model.Dtos.Operations;
using FS.Store.Model.Entity;

namespace FS.Store.Core.Interfaces
{
    public interface IOperationService : IBaseService<Operation, Guid>
    {
        Task<Operation> CreateOperation(CreateOperationDto createOperationDto);
    }
}
