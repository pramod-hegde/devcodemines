namespace Services.Data.AzureServiceBus
{
    public enum ImmutableMessageTargetTypes
    {
        Queue,
        Topic,
        TopicWithBrokeredMessageSupport,
        QueueWithBrokeredMessageSupport
    }
}
