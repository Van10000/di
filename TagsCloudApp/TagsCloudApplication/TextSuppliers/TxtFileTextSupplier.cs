using System.IO;
using Utils;

namespace TagsCloudApplication.TextSuppliers
{
    public class TxtFileTextSupplier : ITextSupplier
    {
        private readonly string filename;

        public TxtFileTextSupplier(string filename)
        {
            this.filename = filename;
        }
        
        public Result<string> SupplyText()
        {
            return Result
                .Of(() => File.ReadAllText(filename));
        }
    }
}
