using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Spark;
using Microsoft.Spark.Sql;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace BLL.Services
{
    public class BackgroundConsumer: BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private readonly IGamesHostedService _gameService;

        public BackgroundConsumer(IGamesHostedService gameService)
        {
            _gameService = gameService;
            Init();
        }

        private void Init()
        {
            var factory = new ConnectionFactory { HostName = "localhost" , Port = 5672 };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "games",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _gameService.AddGameEvent(message, stoppingToken);
                Console.WriteLine("Received message: {0}", message);

                /*var sparkConf = new SparkConf().SetAppName("GameService");
                var sparkContext = new SparkContext(sparkConf);*/
                var asfsf = SparkSession.Builder().AppName("GameService").GetOrCreate();
                //var sqlContext = new SQLContext(sparkContext);

                var body1 = ea.Body.ToArray();
                var data = Encoding.UTF8.GetString(body1);
                var row = data.Split(','); // Предполагается, что данные разделены запятыми

                // Создание DataFrame из строки данных
                var df = asfsf.Read().Json(row.ToString());

                // Обработка данных с помощью Spark SQL
                var result = df.SelectExpr("avg(column1)", "max(column2)");

                // Вывод результатов обработки
                result.Show();
            };

            _channel.BasicConsume(queue: "games",
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
