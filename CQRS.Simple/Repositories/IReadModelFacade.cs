using System;
using System.Collections.Generic;
using CQRS.Simple.DTO;

namespace CQRS.Simple.Repositories
{
    public interface IReadModelFacade
    {
        IEnumerable<InventoryItemListDto> GetInventoryItems();
        InventoryItemDetailsDto GetInventoryItemDetails(Guid id);
    }
}