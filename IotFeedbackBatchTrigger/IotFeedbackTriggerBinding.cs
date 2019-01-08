using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Listeners;
using Microsoft.Azure.WebJobs.Host.Protocols;
using Microsoft.Azure.WebJobs.Host.Triggers;

namespace IotFeedbackBatchTrigger
{
    public class IotFeedbackTriggerBinding : ITriggerBinding
    {
        private readonly Dictionary<string, Type> _bindingContract;
        private readonly ParameterInfo _parameter;

        public IotFeedbackTriggerBinding(ParameterInfo parameter)
        {
            _parameter = parameter;

            _bindingContract = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
            {
                { "data", typeof(FeedbackBatch) }
            };
        }

        public Type TriggerValueType => typeof(FeedbackBatch);

        public IReadOnlyDictionary<string, Type> BindingDataContract => _bindingContract;

        public Task<ITriggerData> BindAsync(object value, ValueBindingContext context)
        {
            if (value is FeedbackBatch)
            {
                var bindingData = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
                {
                    {"data", value}
                };

                IValueBinder valueBinder = new FeedbackBatchValueBinder(_parameter, value);

                return Task.FromResult<ITriggerData>(new TriggerData(valueBinder, bindingData));
            }

            throw new Exception();
        }

        public Task<IListener> CreateListenerAsync(ListenerFactoryContext context)
        {
            var attribute = GetResolvedAttribute<IotFeedbackTriggerAttribute>(_parameter);
            return Task.FromResult<IListener>(new IotFeedbackListener(context.Executor, attribute));
        }

        public ParameterDescriptor ToParameterDescriptor()
        {
            return new IotFeedbackTriggerParameterDescriptor
            {
                Name = _parameter.Name,
                DisplayHints = new ParameterDisplayHints
                {
                    Prompt = "Iot Feedback",
                    Description = "FeedbackBatch trigger fired"
                }
            };
        }

        internal static TAttribute GetResolvedAttribute<TAttribute>(ParameterInfo parameter)
            where TAttribute : Attribute
        {
            var attribute = parameter.GetCustomAttribute<TAttribute>(true);

            var attributeConnectionProvider = attribute as IConnectionProvider;
            if (attributeConnectionProvider != null && string.IsNullOrEmpty(attributeConnectionProvider.Connection))
            {
                var connectionProviderAttribute =
                    attribute.GetType().GetCustomAttribute<ConnectionProviderAttribute>();
                if (connectionProviderAttribute?.ProviderType != null)
                {
                    var connectionOverrideProvider =
                        GetHierarchicalAttributeOrNull(parameter, connectionProviderAttribute.ProviderType) as
                            IConnectionProvider;

                    if (connectionOverrideProvider != null &&
                        !string.IsNullOrEmpty(connectionOverrideProvider.Connection))
                        attributeConnectionProvider.Connection = connectionOverrideProvider.Connection;
                }
            }

            return attribute;
        }

        internal static Attribute GetHierarchicalAttributeOrNull(ParameterInfo parameter, Type attributeType)
        {
            if (parameter == null)
                return null;

            var attribute = parameter.GetCustomAttribute(attributeType);
            if (attribute != null)
                return attribute;

            var method = parameter.Member as MethodInfo;
            if (method == null)
                return null;

            return GetHierarchicalAttributeOrNull(method, attributeType);
        }

        internal static Attribute GetHierarchicalAttributeOrNull(MethodInfo method, Type type)
        {
            var attribute = method.GetCustomAttribute(type);
            if (attribute != null)
                return attribute;

            attribute = method.DeclaringType.GetCustomAttribute(type);
            if (attribute != null)
                return attribute;

            return null;
        }
    }
}
