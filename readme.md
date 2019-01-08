# <img src="./images/AzureIoTHub.svg" title="icon" style="width:30px"/> IotFeedbackBatchTrigger

Simplify creation of Azure Function to catch Complete & Reject message from your Iot Hub client.

You will found a sample project named "TestFunction" in this repository.
It is a project using the first version of the Azure Function SDK.

### Sample
```cs
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
```

You can found documentation about the FeedbackBatch [here](<https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.devices.feedbackbatch?view=azure-dotnet>).

The trigger will search after a setting named 'IotHubConnectionString' to connect to the Iot Hub ([see](/TestFunction/local.settings.json)).

#Version 1.1.0 - [2018-01-08]
## Added
Add support on Azure Functions SDK V2. If you use Azure Functions SDK V1 please keep version 1.0.0

#Version 1.0.0
Initial version