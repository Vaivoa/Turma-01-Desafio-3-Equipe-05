using Confluent.Kafka;
using Modalmais.Business.Models;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Modalmais.API
{
    public class KafkaProducerHostedService
    {
        //private readonly ProducerConfig config = new() { BootstrapServers = "localhost:9092" };
        private readonly ProducerConfig config = new();
        private readonly string topic = "CADASTRO_CONTA_CORRENTE_ATUALIZADO";

        public KafkaProducerHostedService(string server)
        {
            config.BootstrapServers = server;
        }

        public Object SendToKafka(Cliente message)
        {

            using (var producer =
                 new ProducerBuilder<Null, Cliente>(config).SetValueSerializer(new GenericSerializer<Cliente>()).Build())
            {
                try
                {
                    return producer.ProduceAsync(topic, new Message<Null, Cliente> { Value = message })
                        .GetAwaiter()
                        .GetResult();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Oops, something went wrong: {e}");
                }
            }
            return null;
        }


    }

    public class GenericSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            var stringobj = JsonConvert.SerializeObject(data, typeof(T), new JsonSerializerSettings());
            if (stringobj == null)
            {
                return null;
            }

            return Encoding.UTF8.GetBytes(stringobj);
        }
    }
}
