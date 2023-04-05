using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace Services.Data.AzureServiceBus
{
    public static class ServiceBusMessageExtensions
    {
        public static T GetBody<T>(this ServiceBusMessage message)
        {
            if (message == null || message.Body == null)
            {
                return default(T);
            }

            return GetInternalBody<T>(message.Body.ToArray());
        }

        public static T GetBody<T>(this ServiceBusReceivedMessage message)
        {
            if (message == null || message.Body == null)
            {
                return default(T);
            }

            string messageBody = message.Body.ToString();

            if (messageBody.StartsWith("@") && messageBody.Contains(@"http://schemas.microsoft.com/2003/10/Serialization"))
            {
                var deserializer = new DataContractSerializer(typeof(string));
                XmlDictionaryReader reader = XmlDictionaryReader.CreateBinaryReader(message.Body.ToStream(), XmlDictionaryReaderQuotas.Max);
                
                messageBody = (string)deserializer.ReadObject(reader);
            }

            return GetInternalBody<T>(messageBody);
        }

        private static T GetInternalBody<T>(string body)
        {
            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(body, typeof(T));
            }

            object _t = JsonConvert.DeserializeObject(body);

            if (_t.GetType() != typeof(T))
            {
                throw LocalErrors.CannotCastBodyToGeneric<T>();
            }

            return (T)Convert.ChangeType(_t, typeof(T));
        }

        private static T GetInternalBody<T>(byte[] body)
        {
            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(Encoding.UTF8.GetString(body), typeof(T));
            }

            object _t = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(body));

            if (_t.GetType() != typeof(T))
            {
                throw LocalErrors.CannotCastBodyToGeneric<T>();
            }

            return (T)Convert.ChangeType(_t, typeof(T));
        }
    }
}
