using Utils;

namespace TagsCloudApplication.TextSuppliers
{
    public interface ITextSupplier
    {
        Result<string> SupplyText();
    }
}
