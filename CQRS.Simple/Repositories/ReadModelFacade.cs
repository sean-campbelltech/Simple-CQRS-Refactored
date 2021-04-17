using System;
using System.Collections.Generic;
using CQRS.Simple.DTO;

namespace CQRS.Simple.Repositories
{
    // Queries the read database
    public class ReadModelFacade : IReadModelFacade
    {
        public IEnumerable<InventoryItemListDto> GetInventoryItems()
        {
            return ReadDatabase.list;
        }

        public InventoryItemDetailsDto GetInventoryItemDetails(Guid id)
        {
            return ReadDatabase.details[id];
        }
    }
}