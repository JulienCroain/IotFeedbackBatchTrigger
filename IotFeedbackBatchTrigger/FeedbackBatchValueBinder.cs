using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.Bindings;

namespace IotFeedbackTrigger
{
    internal class FeedbackBatchValueBinder : ValueBinder
    {
        private readonly object _value;

        public FeedbackBatchValueBinder(ParameterInfo parameter, object argument) : base(parameter.ParameterType)
        {
            _value = argument;
        }

        public override Task<object> GetValueAsync()
        {
            return Task.FromResult(_value);
        }

        public override string ToInvokeString()
        {
            return $"{_value}";
        }
    }
}