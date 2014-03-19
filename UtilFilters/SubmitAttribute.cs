using System;
using System.Reflection;
using System.Web.Mvc;

namespace UtilFilters
{

    public enum SubmitRequirement
    {
        Equal,
        StartsWith
    }

    public sealed class SubmitAttribute : ActionMethodSelectorAttribute
    {
        private readonly string submitButtonName;
        private readonly SubmitRequirement requirement;

        public SubmitAttribute(string submitButtonName, SubmitRequirement requirement = SubmitRequirement.Equal)
        {
            this.submitButtonName = submitButtonName;
            this.requirement = requirement;
        }

        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            string value = null;

            switch (requirement)
            {
                case SubmitRequirement.Equal:
                    value = controllerContext.HttpContext.Request.Form[submitButtonName];
                    break;
                case SubmitRequirement.StartsWith:

                    for (int index = 0, length = controllerContext.HttpContext.Request.Form.AllKeys.Length; index < length; index++)
                    {
                        var key = controllerContext.HttpContext.Request.Form.AllKeys[index];
                        if (key.StartsWith(submitButtonName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            value = controllerContext.HttpContext.Request.Form[key];
                            break;
                        }
                    }

                    break;
            }

            if (!string.IsNullOrEmpty(value)) return true;

            return false;
        }
    }
}
