namespace DemoServer.Utils.Conventions
{
    public class ExecuteDemoRouteAttribute : KebabCaseRouteAttribute
    {
        protected override string ModifyPathTemplate(string template) => $"execute/{template}";
    }
}
