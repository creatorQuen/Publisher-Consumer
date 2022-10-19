﻿using RabbitMQ.Client;
using System;
using System.Reflection;
using System.Text;

namespace Publisher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "dev-queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "Message from publisher";

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "dev-queue",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine("Message is sent into Default Exchange");
                Console.ReadKey();
            }
        }
    }
}
