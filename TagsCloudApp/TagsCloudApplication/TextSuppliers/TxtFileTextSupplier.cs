using System.IO;

namespace TagsCloudApplication.TextSuppliers
{
    public class TxtFileTextSupplier : ITextSupplier
    {
        private readonly string filename;

        public TxtFileTextSupplier(string filename)
        {
            this.filename = filename;
        }
        
        public string SupplyText()
        {
            return File.ReadAllText(filename);
        }
    }
}
