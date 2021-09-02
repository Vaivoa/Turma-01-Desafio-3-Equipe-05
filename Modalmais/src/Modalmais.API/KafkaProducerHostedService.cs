using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Modalmais.Business.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Modalmais.API
{
    public class KafkaProducerHostedService
    {
        private readonly ProducerConfig config = new ProducerConfig
        { BootstrapServers = "localhost:9092" };
        private readonly string topic = "CADASTRO_CONTA_CORRENTE_ATUALIZADO";

        public Object SendToKafka(Cliente message)
        {
            using (var producer =
                 new ProducerBuilder<Null, Cliente>(config).Build())
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
}
