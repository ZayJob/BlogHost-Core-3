using BlogHost.services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlogHost.BackgroundService
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<TimedHostedService> _logger;
        private Timer _timer;
        public IConfiguration EmailConfiguration { get; set; }

        public TimedHostedService(ILogger<TimedHostedService> logger)
        {
            _logger = logger;
            var builder = new ConfigurationBuilder().AddJsonFile("mailconfig.json");

            EmailConfiguration = builder.Build();
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromHours(24));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(EmailConfiguration["Email:mail"], "New posts", $"Вы давно не заходили проверьте нове посты, перейдя по ссылке:<a href='https://localhost:44307/'>link</a> ");
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
