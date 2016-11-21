using System.ComponentModel;

namespace DemoServer.Helpers
{
    public class DemoAttribute : DisplayNameAttribute
    {
        public const int DefaultDemoOrder = 999;

        public DemoAttribute(string demoName = null, DemoOutputType demoOutputType = DemoOutputType.Standard, int demoOrder = DefaultDemoOrder)
            : base(demoName)
        {
            DemoOutputType = demoOutputType;
            DemoOrder = demoOrder;
        }

        public DemoOutputType DemoOutputType { get; private set; }

        public int DemoOrder { get; private set; }
    }

    public enum DemoOutputType
    {
        Standard,
        Flatten,
        Json,
        String
    }
}