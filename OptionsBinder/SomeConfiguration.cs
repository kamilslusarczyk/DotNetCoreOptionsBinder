using OptionsBinder.Configuration;

namespace OptionsBinder
{
    public class SomeConfiguration : IOptionsModel
    {
        public string SomeProp { get; set; }
    }
}
