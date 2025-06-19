using PaymentApp.API.Repositories;
using PaymentApp.API.Services;

namespace PaymentApp.API.BackGround_Worker
{
    public class PaymentConfirmationWorker : BackgroundService
    {
        private readonly ILogger<PaymentConfirmationWorker> _logger;
        //private readonly ITransactionRepository _transactionRepository;
        private readonly IServiceScopeFactory _scopeFactory;

        public PaymentConfirmationWorker(ILogger<PaymentConfirmationWorker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Payment confirmation worker started.");
            //Console.WriteLine("Payment confirmation worker started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var transactionRepository = scope.ServiceProvider.GetRequiredService<ITransactionRepository>();

                    var allUnconfirmed = await transactionRepository.GetUnconfirmedTransactionsAsync();

                    var now = DateTime.UtcNow;

                    var toConfirm = allUnconfirmed
                        .Where(tx => tx.CreatedAt.HasValue && tx.CreatedAt.Value.Date < now.Date)
                        .ToList();

                    foreach (var tx in toConfirm)
                    {
                        tx.IsConfirmed = true;
                        _logger.LogInformation($"Auto-confirmed Transaction: {tx.TransactionId}");
                        await transactionRepository.UpdateAsync(tx);
                    }

                    if (toConfirm.Any())
                    {
                        await transactionRepository.SaveChangesAsync();
                        //await _transactionRepository.SaveChangesAsync();
                        _logger.LogInformation("Done");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error confirming transactions.");
                }

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken); // run every 10 mins
            }
        }
    }
}
