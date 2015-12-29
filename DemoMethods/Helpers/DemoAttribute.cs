using System.ComponentModel;

namespace DemoMethods.Helpers
{
    public class DemoAttribute : DisplayNameAttribute
    {
        public DemoAttribute(DemoOutputType demoOutputType)
            : base(null)
        {
            DemoOutputType = demoOutputType;
        }

        public DemoAttribute(string demoName, DemoOutputType demoOutputType = DemoOutputType.Standard)
            : base(demoName)
        {
            DemoOutputType = demoOutputType;
        }

        public DemoOutputType DemoOutputType { get; private set; }
    }

    public enum DemoOutputType
    {
        Standard,
        Flatten,
        Json,
        String
    }
}