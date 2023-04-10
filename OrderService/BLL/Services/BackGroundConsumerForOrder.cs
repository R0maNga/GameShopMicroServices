using System.ComponentModel;
using System.Text;
using BLL.Models.Input.OrderInput;
using BLL.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using IModel = RabbitMQ.Client.IModel;


namespace OrderService
{  
    public class BackGroundConsumerForOrder : BackgroundService
    {
        private  IConnection _connection;
        private  IModel _channel;
        private readonly IOrderHostedService _orderHostedService;
        
        public BackGroundConsumerForOrder(IOrderHostedService orderHostedService)
        {
            _orderHostedService = orderHostedService;
            Init();
        }   
        private void  Init()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received +=  (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _orderHostedService.ProcessEvent(message, stoppingToken);
                Console.WriteLine("Received message: {0}", message);
            };
            
            _channel.BasicConsume(queue: "hello",
                autoAck: true,
                consumer: consumer);

            
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
        public override void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
            base.Dispose();
        }
    }
}
