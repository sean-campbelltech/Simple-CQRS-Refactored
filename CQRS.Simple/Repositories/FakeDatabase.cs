using System;
using System.Collections.Generic;
using CQRS.Simple.DTO;

namespace CQRS.Simple.Repositories
{
    internal class FakeDatabase
    {
        public static Dictionary<Guid, InventoryItemDetailsDto> details = new Dictionary<Guid, InventoryItemDetailsDto>();
        public static List<InventoryItemListDto> list = new List<InventoryItemListDto>();
    }
}