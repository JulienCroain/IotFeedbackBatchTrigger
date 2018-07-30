using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Host.Executors;
using Microsoft.Azure.WebJobs.Host.Listeners;

namespace IotFeedbackTrigger
{
    internal class IotFeedbackListener : IListener
    {
        private ITriggeredFunctionExecutor _executor;
        private IotFeedbackTriggerAttribute _attribute;
        private bool _listening = false;

        public IotFeedbackListener(ITriggeredFunctionExecutor executor, IotFeedbackTriggerAttribute attribute)
        {
            _executor = executor;
            _attribute = attribute;
        }

        public void Cancel()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var connectionString = AmbientConnectionStringProvider.Instance.GetConnectionString(_attribute.IotHubConnectionString);

            var serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

            var feedbackReceiver = serviceClient.GetFeedbackReceiver();

            _listening = true;
            
            while (_listening)
            {
                var feedbackBatch = await feedbackReceiver.ReceiveAsync();
                if (feedbackBatch == null) continue;

                var triggerData = new TriggeredFunctionData
                {
                    TriggerValue = feedbackBatch
                };

                await _executor.TryExecuteAsync(triggerData, CancellationToken.None);

                await feedbackReceiver.CompleteAsync(feedbackBatch);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _listening = false;

            return Task.CompletedTask;
        }
    }
}