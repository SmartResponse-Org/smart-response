using FluentValidation;
using SmartResponse.Enums;
using SmartResponse.Localization;
using SmartResponse.Localization.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartResponse.FluentValidation
{
    public static class FluentExtension
    {
        private static ICustomStringLocalizer<ErrorMessage> _messageLocalizer;
        private static ICustomStringLocalizer<Label> _labelLocalizer;

        public static IRuleBuilderOptions<T, TProperty> SmartResponse<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                                    MessageCode messageCode,
                                                                                    string label = "")
        {
            ruleBuilder.Configure(cfg =>
            {
                _messageLocalizer = LocalizerProvider<ErrorMessage>.GetLocalizer();
                _labelLocalizer = LocalizerProvider<Label>.GetLocalizer();

                //cfg.CascadeMode = CascadeMode.Stop;
                var trimedPropertyName = cfg.PropertyName.Trim()?.Replace(" ", "");
                var localizedLabel = _labelLocalizer[trimedPropertyName];
                var localizedPropertyDisplayName = !string.IsNullOrWhiteSpace(localizedLabel) ? localizedLabel : trimedPropertyName;
                string localizedMessage = _messageLocalizer[messageCode.GetDescription(), localizedPropertyDisplayName];
               
                localizedMessage = string.IsNullOrWhiteSpace(label) ? localizedMessage : _messageLocalizer[messageCode.GetDescription(), label];
                cfg.Current.ErrorCode = messageCode.GetDescription();
                cfg.Current.SetErrorMessage(localizedMessage);
            });

            return ruleBuilder;
        }

        public static IRuleBuilderOptions<T, TProperty> SmartResponse<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                                    MessageCode messageCode,
                                                                                    params object[] labels)
        {
            ruleBuilder.Configure(cfg =>
            {
                _messageLocalizer = LocalizerProvider<ErrorMessage>.GetLocalizer();
                _labelLocalizer = LocalizerProvider<Label>.GetLocalizer();
            
                // cfg.CascadeMode = CascadeMode.Stop;
                var trimedPropertyName = cfg.PropertyName.Trim()?.Replace(" ", "");
                var localizedLabel = _labelLocalizer[trimedPropertyName];
                var localizedPropertyDisplayName = !string.IsNullOrWhiteSpace(localizedLabel) ? localizedLabel : trimedPropertyName;
                string localizedMessage = _messageLocalizer[messageCode.GetDescription()];

                //add field name dynamically which being validated and update in localization resx file
                if (labels != null && labels.Count() > 0)
                {
                    Array.Resize(ref labels, labels.Length + 1);
                    labels[labels.Length - 1] = trimedPropertyName;
                    labels = labels.PrefSuffArray("[", "]");
                }

                localizedMessage = labels == null || labels.Count() == 0 ? localizedMessage : string.Format(localizedMessage, labels);
                cfg.Current.ErrorCode = messageCode.GetDescription();
                cfg.Current.SetErrorMessage(localizedMessage);
            });

            return ruleBuilder;
        }

        public static IRuleBuilderOptions<T, TProperty> NotEmpty<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, MessageCode messageCode, string message = "")
        {
            return ruleBuilder.NotEmpty().SmartResponse(messageCode, message);
        }
    }
}
