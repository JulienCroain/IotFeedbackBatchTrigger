using IotFeedbackBatchTrigger;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;

[assembly: WebJobsStartup(typeof(FeedBackBacthWebJobsStartup))]
namespace IotFeedbackBatchTrigger
{
    class FeedBackBacthWebJobsStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddFeedBackBatch();
        }
    }
}
