namespace Volunteer_API.Data
{
    public class BackgroundWorkerService : BackgroundService
    {
        readonly ILogger<BackgroundWorkerService> _logger;
        readonly IServiceProvider _serviceProvider;
        public BackgroundWorkerService(ILogger<BackgroundWorkerService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {

                _logger.LogInformation("Worker Running at: {time}", DateTimeOffset.Now);
                using (var scope = _serviceProvider.CreateScope())
                {
                    var emailRepository = scope.ServiceProvider.GetRequiredService<IEmailRepository>();
                    emailRepository.sendEmailToAllUsers();                    
                }
                await Task.Delay(300000, stoppingToken);
            }
        }
    }
}