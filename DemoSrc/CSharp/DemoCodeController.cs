namespace DemoSrc.CSharp
{
    public abstract class DemoCodeController
    {
        public abstract void SetPrerequisites();
    }

    public abstract class DemoCodeController<TRunInput, TRunOutput> : DemoCodeController
    {
        public abstract TRunOutput Run(TRunInput input);

    }

    public class EmptyParameters
    {
    }
}
