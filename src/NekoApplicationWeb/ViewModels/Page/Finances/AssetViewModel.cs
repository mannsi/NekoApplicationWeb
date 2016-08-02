using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.ViewModels.Page.Finances
{
    public class AssetViewModel
    {
        private string _assetTypeString;

        public string Id { get; set; }
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
