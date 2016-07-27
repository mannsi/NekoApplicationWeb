using System;

namespace NekoApplicationWeb.Models
{
    public enum AssetType
    {
        Property,
        Vehicle
    }

    public class ApplicantFinancesAsset
    {
        private string _assetTypeString;

        public ApplicantFinancesAsset()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public ApplicationUser User { get; set; }

        public AssetType AssetType { get; set; }
        public string AssetNumber { get; set; }
        public bool AssetWillBeSold { get; set; }

        public string AssetTypeString
        {
            get
            {
                if (!string.IsNullOrEmpty(_assetTypeString))
                {
                    return _assetTypeString;
                }

                switch (AssetType)
                {
                    case AssetType.Property:
                        return "Fasteign";
                    case AssetType.Vehicle:
                        return "Ökutæki";
                }
                return "";
            }
            set { _assetTypeString = value; }
        }
    }
}
