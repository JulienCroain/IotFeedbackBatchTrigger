using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.WebJobs.Host.Triggers;

namespace IotFeedbackTrigger
{
    public class FeedbackBatchTriggerAttributeBindingProvider : ITriggerBindingProvider
    {
        private readonly FeedbackBatchExtensionConfig _feedbackBatchExtensionConfig;

        public FeedbackBatchTriggerAttributeBindingProvider(FeedbackBatchExtensionConfig feedbackBatchExtensionConfig)
        {
            _feedbackBatchExtensionConfig = feedbackBatchExtensionConfig;
        }

        public Task<ITriggerBinding> TryCreateAsync(TriggerBindingProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var parameter = context.Parameter;

            var attribute = parameter.GetCustomAttribute<IotFeedbackTriggerAttribute>(false);

            if (attribute == null)
                return Task.FromResult<ITriggerBinding>(null);

            if (!IsSupportBindingType(parameter.ParameterType))
                throw new InvalidOperationException($"Can't bind IotFeedbackTriggerAttribute to type '{parameter.ParameterType}'.");

            return Task.FromResult<ITriggerBinding>(new IotFeedbackTriggerBinding(parameter));
        }

        private bool IsSupportBindingType(Type t)
        {
            return t == typeof(FeedbackBatch);
        }
    }
}