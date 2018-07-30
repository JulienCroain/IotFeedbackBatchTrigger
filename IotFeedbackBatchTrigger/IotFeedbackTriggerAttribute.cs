using Microsoft.Azure.WebJobs.Description;
using System;

namespace IotFeedbackTrigger
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