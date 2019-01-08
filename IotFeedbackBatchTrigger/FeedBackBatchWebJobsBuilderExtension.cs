using System;
using Microsoft.Azure.WebJobs;

namespace IotFeedbackBatchTrigger
{
    public static class FeedBackBatchWebJobsBuilderExtension
    {
        public static IWebJobsBuilder AddFeedBackBatch(this IWebJobsBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.AddExtension<FeedbackBatchExtensionConfig>();
            return builder;
        }
    }
}
