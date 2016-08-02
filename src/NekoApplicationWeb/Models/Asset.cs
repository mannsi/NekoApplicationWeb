using System;

namespace NekoApplicationWeb.Models
{
    public enum AssetType
    {
        Property,
        Vehicle
    }

    public class Asset
    {
        public Asset()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public Application Application { get; set; }
        public AssetType AssetType { get; set; }
        public string AssetNumber { get; set; }
        public bool AssetWillBeSold { get; set; }
    }
}
