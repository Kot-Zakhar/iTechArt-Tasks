using MoneyManager.DataAccess.Entity;
using System;

namespace MoneyManager.Service.Model
{
    public class AssetInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public AssetInfo(Asset asset)
        {
            Id = asset.Id;
            Name = asset.Name;
        }
    }
}
