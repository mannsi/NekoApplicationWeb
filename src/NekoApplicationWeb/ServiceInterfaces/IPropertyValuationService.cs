using NekoApplicationWeb.Models;

namespace NekoApplicationWeb.ServiceInterfaces
{
    public interface IPropertyValuationService
    {
        PropertyValuation GetPropertyValuation(string propertyNumber);
    }
}