using System.ComponentModel.DataAnnotations;

namespace NekoApplicationWeb.ViewModels.Page.Finances
{
    public enum AssetType
    {
        Property,
        Vehicle
    }

    public class ApplicantAssets
    {
        private string _assetTypeString;

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
