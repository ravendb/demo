using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace DemoServer.Utils.Conventions
{
    public class KebabCaseRouteAttribute : Attribute, IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var hasRouteAttributes = controller.Selectors.Any(x => x.AttributeRouteModel != null);

            if (hasRouteAttributes)
                return;

            foreach (var controllerAction in controller.Actions)
            {
                var controllerName = PascalCaseToKebabCase(controller.ControllerName);
                var actionName = PascalCaseToKebabCase(controllerAction.ActionName);
                var template = ModifyPathTemplate($"{controllerName}/{actionName}");

                SetTemplateInSelectors(controllerAction, template);
            }
        }

        protected virtual string ModifyPathTemplate(string template) => template;

        private void SetTemplateInSelectors(ActionModel controllerAction, string template)
        {
            var selectors = controllerAction.Selectors.Where(x => x.AttributeRouteModel == null);

            foreach (var selector in selectors)
            {
                selector.AttributeRouteModel = new AttributeRouteModel
                {
                    Template = template
                };
            }
        }

        private string PascalCaseToKebabCase(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            return Regex.Replace(
                    text,
                    "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])",
                    "-$1",
                    RegexOptions.Compiled)
                .Trim()
                .ToLower();
        }
    }
}
