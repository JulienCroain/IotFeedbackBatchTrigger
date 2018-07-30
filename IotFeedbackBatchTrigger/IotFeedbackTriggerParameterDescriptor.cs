using Microsoft.Azure.WebJobs.Host.Protocols;
using System;
using System.Collections.Generic;

namespace IotFeedbackTrigger
{
    internal class IotFeedbackTriggerParameterDescriptor : TriggerParameterDescriptor
    {
        public override string GetTriggerReason(IDictionary<string, string> arguments)
        {
            return string.Format("Iot Feedback trigger fired at {0}", DateTime.Now.ToString("o"));
        }
    }
}