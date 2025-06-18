using PaymentApp.API.Services;

namespace PaymentApp.API.BackGround_Worker
{
    public class PaymentConfirmationWorker : BackgroundService
    {
        private readonly ILogger<PaymentConfirmationWorker> _logger;
        private readonly IPaymentTransactionStore _transactionStore;

        public PaymentConfirmationWorker(ILogger<PaymentConfirmationWorker> logger, IPaymentTransactionStore transactionStore)
        {
            _logger = logger;
            _transactionStore = transactionStore;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Payment confirmation worker started.");
            //Console.WriteLine("Payment confirmation worker started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var now = DateTime.UtcNow;

                    var toConfirm = _transactionStore.GetUnconfirmedTransactions()
                        .Where(tx => tx.CreatedAt.Date < now.Date)
                        .ToList();

                    foreach (var tx in toConfirm)
                    {
                        tx.IsConfirmed = true;
                        _logger.LogInformation($"Auto-confirmed Transaction: {tx.TransactionId}");
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
