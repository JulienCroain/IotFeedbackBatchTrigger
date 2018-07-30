using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host.Config;
using System;

namespace IotFeedbackTrigger
{
    public class FeedbackBatchExtensionConfig : IExtensionConfigProvider
    {
        /// <summary>
        ///     Initializes the extension. Initialization should register any extension bindings
        ///     with the <see cref="T:Microsoft.Azure.WebJobs.Host.IExtensionRegistry" /> instance, which can be obtained from the
        ///     <see cref="T:Microsoft.Azure.WebJobs.JobHostConfiguration" /> which is an <see cref="T:System.IServiceProvider" />.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.Azure.WebJobs.Host.Config.ExtensionConfigContext" /></param>
        public void Initialize(ExtensionConfigContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            context.Config.RegisterBindingExtensions(new FeedbackBatchTriggerAttributeBindingProvider(this));
        }
    }
}
