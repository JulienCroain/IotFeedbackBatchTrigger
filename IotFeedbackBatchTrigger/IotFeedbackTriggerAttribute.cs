using System;
using Microsoft.Azure.WebJobs.Description;

namespace IotFeedbackBatchTrigger
{
    [AttributeUsage(AttributeTargets.Parameter)]
    [Binding]
    public sealed class IotFeedbackTriggerAttribute : Attribute
    {
        /// <summary>
        /// Gets the name of the connection string to use.
        /// </summary>
        public string IotHubConnectionString { get; set; } = "IotHubConnectionString";

    }
}