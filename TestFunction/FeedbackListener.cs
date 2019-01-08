using IotFeedbackBatchTrigger;
using Microsoft.Azure.Devices;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace FunctionApp2
{
    public static class FeedbackListener
    {
        [FunctionName("FeedbackListener")]
        public static void Run([IotFeedbackTrigger] FeedbackBatch req, TraceWriter log)
        {
            if (req == null)
                return;

            foreach (var record in req.Records)
            {
                log.Info($"Device : {record.DeviceId}, MessageId: {record.OriginalMessageId}, Status: {record.StatusCode}");
            }
        }
    }
}
