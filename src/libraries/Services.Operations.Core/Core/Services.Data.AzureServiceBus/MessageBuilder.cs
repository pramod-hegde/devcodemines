using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace Services.Data.AzureServiceBus
{
    sealed class MessageBuilder<T>
    {
        readonly T _data;
        readonly IAzureServiceBusMessageWriterSetting _setting;

        internal MessageBuilder (T data, IAzureServiceBusMessageWriterSetting setting)
        {
            _data = data;
            _setting = setting;
        }

        internal ServiceBusMessage Message
        {
            get
            {
                return BuildMessage();
            }
        }

        private ServiceBusMessage BuildMessage ()
        {
            if (_setting == null)
            {
                return new ServiceBusMessage(_data as byte[]);
            }

            return BuildMessageWithSettings();
        }

        private ServiceBusMessage BuildMessageWithSettings ()
        {
            var message = new ServiceBusMessage(DataAsByteArray());

            if (message == default(ServiceBusMessage))
            {
                throw LocalErrors.CouldNotCreateTopicClientWithSettings();
            }

            new MessageDecorator(message).Decorate(_setting);
            return message;
        }

        private byte[] DataAsByteArray ()
        {
            if (_data == null)
            {
                return null;
            }
            else if (_data is byte[])
            {
                return _data as byte[];
            }
            else if (_data is string)
            {
                return Encoding.UTF8.GetBytes(_data as string);
            }
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_data));
        }
    }
}
