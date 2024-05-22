using FluentValidation;
using SmartResponse.Enums;
using SmartResponse.Localization;
using System;
using System.Linq;

namespace SmartResponse.FluentValidation
{
    public static class FluentExtension
    {

        /// <summary>
        /// Add error using built-in error codes and labels.
        /// </summary>
        public static IRuleBuilderOptions<T, TProperty> SmartResponse<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                                    MessageCode code,
                                                                                    params object[] labels)
        {
            return SmartResponse<T, TProperty, ErrorMessage, Label>(ruleBuilder, code.GetDescription(), labels);
        }

        /// <summary>
        /// Add error using built-in error codes and custom labels.
        /// </summary>
        public static IRuleBuilderOptions<T, TProperty> SmartResponse<T, TProperty, Label>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                            MessageCode code,
                                                                            params object[] labels)
        {
            return SmartResponse<T, TProperty, ErrorMessage, Label>(ruleBuilder, code.GetDescription(), labels);
        }

        /// <summary>
        /// Add error using custom error codes and built-in labels.
        /// </summary>
        public static IRuleBuilderOptions<T, TProperty> SmartResponse<T, TProperty, Error>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                            string code,
                                                                            params object[] labels)
        {
            return SmartResponse<T, TProperty, Error, Label>(ruleBuilder, code, labels);
        }

        /// <summary>
        /// Add error using custom error codes without labels.
        /// </summary>
        public static IRuleBuilderOptions<T, TProperty> SmartResponse<T, TProperty, Error>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                            string code)
        {
            return SmartResponse<T, TProperty, Error, Label>(ruleBuilder, code);
        }

        /// <summary>
        /// Add error using custom error codes and custom labels.
        /// </summary>
        public static IRuleBuilderOptions<T, TProperty> SmartResponse<T, TProperty, Error, Label>(this IRuleBuilderOptions<T, TProperty> ruleBuilder,
                                                                                                  string code,
                                                                                                  params object[] labels)
        {
            ruleBuilder.Configure(config =>
            {
                var messageLocalizer = LocalizerProvider<Error>.GetLocalizer();
                var labelLocalizer = LocalizerProvider<Label>.GetLocalizer();

                // cfg.CascadeMode = CascadeMode.Stop;
                var trimedPropertyName = config.PropertyName.Trim()?.Replace(" ", "");

                var localizedLabel = labelLocalizer[trimedPropertyName];

                var localizedPropertyDisplayName = !string.IsNullOrWhiteSpace(localizedLabel) ? localizedLabel : trimedPropertyName;

                string localizedMessage = messageLocalizer[code];

                //add field name dynamically which being validated and update in localization resx file
                if (labels != null && labels.Count() > 0)
                {
                    var newLabelLength = labels.Length + 1;
                    var newLabels = new object[newLabelLength];
                    newLabels[0] = localizedPropertyDisplayName;
                    Array.Copy(labels, 0, newLabels, 1, labels.Length);

                    //Array.Resize(ref labels, labels.Length + 1);
                    //labels[labels.Length - 1] = localizedPropertyDisplayName;
                    //labels = newLabels.PrefSuffArray("[", "]");

                    labels = newLabels;
                }

                localizedMessage = labels == null || labels.Count() == 0 ? localizedMessage : string.Format(localizedMessage, labels);

                config.Current.ErrorCode = code;
                config.Current.SetErrorMessage(localizedMessage);
            });

            return ruleBuilder;
        }
    }
}
