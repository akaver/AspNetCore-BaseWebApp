using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApp.TagHelpers
{
    /// <summary>
    /// Add validation tags for Time, Date and DateTime DataType (data-val-date)
    /// Triggers clientside validation (jquery-validation and unobtrusive validation)
    /// Requires globalize based plugin to jquery-validation (~/js/jquery.validate.globalize.datetime.js)
    /// Usually also dom manipulation via js is required to display datetime types in correct locale (html5 wire format is sql)
    /// </summary>
    [HtmlTargetElement(tag: "input", Attributes = "asp-for")]
    public class DateTimeTagHelper : TagHelper
    {
        public override int Order { get; } = int.MaxValue;

        [HtmlAttributeName(name: "asp-for")]
        public ModelExpression For { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context: context, output: output);

            // get display name attribute
            var displayName = GetDisplayName(expression: For);

            // dont overwrite manually provided attributes
            if (!output.Attributes.ContainsName(name: "data-val-date"))
            {
                if (IsDate(validatorMetadata: For.ModelExplorer.Metadata.ValidatorMetadata))
                {
                    output.Attributes.Add(name: "data-val-date",
                        value: GetValidateErrorMessage(validatorMetadata: For.ModelExplorer.Metadata.ValidatorMetadata, displayName: displayName, defaultMessage: "The field {0} must be a date."));
                }
            }
            if (!output.Attributes.ContainsName(name: "data-val-time"))
            {
                if (IsTime(validatorMetadata: For.ModelExplorer.Metadata.ValidatorMetadata))
                {
                    output.Attributes.Add(name: "data-val-time",
                        value: GetValidateErrorMessage(validatorMetadata: For.ModelExplorer.Metadata.ValidatorMetadata, displayName: displayName, defaultMessage: "The field {0} must be a time."));

                }
            }
            if (!output.Attributes.ContainsName(name: "data-val-datetime"))
            {
                if (IsDateTime(validatorMetadata: For.ModelExplorer.Metadata.ValidatorMetadata))
                {
                    output.Attributes.Add(name: "data-val-datetime",
                        value: GetValidateErrorMessage(validatorMetadata: For.ModelExplorer.Metadata.ValidatorMetadata, displayName: displayName, defaultMessage: "The field {0} must be a datetime."));
                }
            }
        }

        private string GetValidateErrorMessage(IReadOnlyList<object> validatorMetadata, string displayName, string defaultMessage)
        {
            // get the attribute
            var attribute = validatorMetadata.FirstOrDefault(predicate: t => t is DataTypeAttribute) as DataTypeAttribute;

            if (attribute != null)
            {
                // first choice - errorMessage 
                if (!string.IsNullOrEmpty(value: attribute.ErrorMessage))
                {
                    return string.Format(format: attribute.ErrorMessage, arg0: displayName);
                }

                // second choice - get the message from resource
                if (!string.IsNullOrEmpty(value: attribute.ErrorMessageResourceName) &&
                    attribute.ErrorMessageResourceType != null)
                {
                    ResourceManager rm = new ResourceManager(resourceSource: attribute.ErrorMessageResourceType);
                    string msg = rm.GetString(name: attribute.ErrorMessageResourceName);
                    return string.Format(format: msg, arg0: displayName);
                }
            }

            // use default
            return string.Format(format: defaultMessage, arg0: displayName);
        }

        /// <summary>
        /// Get the field name from model expression
        /// Use Display attribute based name when present
        /// </summary>
        /// <param name="expression">ModelExpression to extact name from</param>
        /// <returns></returns>
        private string GetDisplayName(ModelExpression expression)
        {
            var displayName = expression.ModelExplorer.Metadata.DisplayName ??
                              expression.ModelExplorer.Metadata.PropertyName;
            if (displayName == null && expression.Name != null)
            {
                var index = expression.Name.LastIndexOf(value: '.');
                // when in dot notation, use only last part
                displayName = index == -1 ? expression.Name : expression.Name.Substring(startIndex: index + 1);
            }

            return displayName;
        }

        private static bool IsDateTime(IReadOnlyList<object> validatorMetadata)
        {
            return validatorMetadata.Any(predicate: t => t is DataTypeAttribute dataTypeAttribute && dataTypeAttribute.DataType == DataType.DateTime);
        }
        private static bool IsDate(IReadOnlyList<object> validatorMetadata)
        {
            return validatorMetadata.Any(predicate: t => t is DataTypeAttribute dataTypeAttribute && dataTypeAttribute.DataType == DataType.Date);
        }
        private static bool IsTime(IReadOnlyList<object> validatorMetadata)
        {
            return validatorMetadata.Any(predicate: t => t is DataTypeAttribute dataTypeAttribute && dataTypeAttribute.DataType == DataType.Time);
        }
    }
}
