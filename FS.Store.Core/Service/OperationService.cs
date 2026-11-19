using FS.Store.Core.Interfaces;
using FS.Store.DAL.Interfaces;
using FS.Store.Model.Dtos.Operations;
using FS.Store.Model.Entity;
using FS.Store.Model.Extensions;

namespace FS.Store.Core.Service
{
    public class OperationService(IOperationRepository repository) : BaseService<Operation, Guid>(repository), IOperationService
    {
        public async Task<Operation> CreateOperation(CreateOperationDto createOperationDto)
        {
            var operation = createOperationDto.ToDto<Operation>();

            await base.AddOrUpdateAsync(operation);

            return operation;
        }
    }
}
