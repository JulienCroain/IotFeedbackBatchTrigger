using System;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs.Host.Protocols;

namespace IotFeedbackBatchTrigger
{
    internal class IotFeedbackTriggerParameterDescriptor : TriggerParameterDescriptor
    {
        public override string GetTriggerReason(IDictionary<string, string> arguments)
        {
            return $"Iot Feedback trigger fired at {DateTime.Now:o}";
        }
    }
}