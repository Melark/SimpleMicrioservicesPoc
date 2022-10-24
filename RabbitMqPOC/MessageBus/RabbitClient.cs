using System;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace MessageBus
{
    public class RabbitClient : IMessageBus, IDisposable
    {
        private ConnectionFactory _factory;
        private IConnection? _connection;
        private IModel _channel;
        private IConfiguration _configuration;

        public RabbitClient(IConfiguration configuration)
        {
            try
            {
                _configuration = configuration;
                _factory = new ConnectionFactory
                {
                    HostName = _configuration["RabbitMQHost"],
                    Port = int.Parse(_configuration["RabbitMQPort"])
                };

                _connection = _factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: _configuration["RabbitMQChannel"], type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMqConnectionShutdown;

                Console.WriteLine("--> Connected to RabbitMQ");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void RabbitMqConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> RabbitMQ connection shutdown");
        }


        public void SendMessage<T>(T message)
        {
            var serializedMessage = JsonSerializer.Serialize(message);

            if (_connection?.IsOpen ?? false)
            {
                Console.WriteLine("--> RabbitMQ Connection Open, sending message...");
                Publish(serializedMessage);
            }
            else
            {
                Console.WriteLine("--> RabbitMQ Connection closed, not sending");
            }
        }

        private void Publish(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(
                exchange: _configuration["RabbitMQChannel"], 
                routingKey: "", 
                basicProperties: null,
                body: body);
            
            Console.WriteLine($"--> We sent message {message}");
        }

        public void Dispose()
        {
            Console.WriteLine("MessageBus disposed");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection?.Close();
            }
        }
    }
}