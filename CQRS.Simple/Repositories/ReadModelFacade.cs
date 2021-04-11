using System;
using System.Collections.Generic;
using CQRS.Simple.DTO;

namespace CQRS.Simple.Repositories
{
    public class ReadModelFacade : IReadModelFacade
    {
        public IEnumerable<InventoryItemListDto> GetInventoryItems()
        {
            return FakeDatabase.list;
        }

        public InventoryItemDetailsDto GetInventoryItemDetails(Guid id)
        {
            return FakeDatabase.details[id];
        }
    }
}