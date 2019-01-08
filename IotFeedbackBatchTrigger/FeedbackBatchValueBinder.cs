using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;

namespace IotFeedbackBatchTrigger
{
    internal class FeedbackBatchValueBinder : IValueBinder
    {
        private object _value;

        public FeedbackBatchValueBinder(ParameterInfo parameter, object argument)
        {
            _value = argument;
            Type = parameter.ParameterType;
        }

        public Task<object> GetValueAsync()
        {
            return Task.FromResult(_value);
        }

        public string ToInvokeString()
        {
            return $"{_value}";
        }

        public Type Type { get; }

        public Task SetValueAsync(object value, CancellationToken cancellationToken)
        {
            _value = value;
            return Task.CompletedTask;
        }
    }
}