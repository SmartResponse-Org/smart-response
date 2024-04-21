using FluentValidation;
using SmartResponse.Enums;
using SmartResponse.Localization;
using SmartResponse.Localization.Helpers;
using System;
using System.Linq;

namespace SmartResponse.FluentValidation
{
    public static class FluentExtension
    {
        public static IRuleBuilderOptions<T, TProperty> SmartResponse<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                                    MessageCode messageCode,
                                                                                    params object[] labels)
        {
            ruleBuilder.Configure(config =>
            {
                var messageLocalizer = LocalizerProvider<ErrorMessage>.GetLocalizer();
                var labelLocalizer = LocalizerProvider<Label>.GetLocalizer();
            
                // cfg.CascadeMode = CascadeMode.Stop;
                var trimedPropertyName = config.PropertyName.Trim()?.Replace(" ", "");

                var localizedLabel = labelLocalizer[trimedPropertyName];
                
                var localizedPropertyDisplayName = !string.IsNullOrWhiteSpace(localizedLabel) ? localizedLabel : trimedPropertyName;
                
                string localizedMessage = messageLocalizer[messageCode.GetDescription()];

                //add field name dynamically which being validated and update in localization resx file
                if (labels != null && labels.Count() > 0)
                {
                    Array.Resize(ref labels, labels.Length + 1);
                    labels[labels.Length - 1] = trimedPropertyName;
                    labels = labels.PrefSuffArray("[", "]");
                }

                localizedMessage = labels == null || labels.Count() == 0 ? localizedMessage : string.Format(localizedMessage, labels);

                config.Current.ErrorCode = messageCode.GetDescription();
                config.Current.SetErrorMessage(localizedMessage);
            });

            return ruleBuilder;
        }
    }
}
