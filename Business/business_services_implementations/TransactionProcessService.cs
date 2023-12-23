using Business.business_services;
using CsvHelper;
using DtoModel;
using System.Globalization;
using System.Text;

namespace Business.business_services_implementations
{
    public class TransactionProcessService : ITransactionProessService
    {
        private readonly string _baseDirectory = "G:\\NetCore\\Pioneers Technology\\";

        public async Task<Summary> GetSummaryByGoodIdAndDateRange(int goodId, DateTime startDate, DateTime endDate)
        {
            var transactions = await GetTransactionsByGoodIdAndDateRange(goodId, startDate, endDate);

            if (transactions == null||!transactions.Any())
            return null;

            int numberOfTransactions = transactions.Count();
            decimal totalAmount = transactions.Sum(t => t.Amount);
            decimal remainingAmount = CalculateRemainingAmount(transactions);

            return new Summary(numberOfTransactions, totalAmount, remainingAmount);
        }

        public async Task<IEnumerable<TransactionProcessDto>> GetTransactionsByGoodIdAndDateRange(int goodId, DateTime startDate, DateTime endDate)
        {
            var transactions = await ReadCsvFile("output_5401.csv");
            return transactions
                .Where(t => t.GoodID == goodId && t.TransactionDate >= startDate && t.TransactionDate <= endDate);
        }

        private async Task<List<TransactionProcessDto>> ReadCsvFile(string fileName)
        {
            var filePath = Path.Combine(_baseDirectory, fileName);
            var transactions = new List<TransactionProcessDto>();
            var errors = new StringBuilder();

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"The data file {fileName} does not exist in the directory {_baseDirectory}.");

            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecordsAsync<TransactionProcessDto>();
            await foreach (var record in records)
            {
                if (record.GoodID != null && record.TransactionDate != null)
                {
                    transactions.Add(record);
                }
                else
                {
                    errors.AppendLine($"Record with TransactionID {record.TransactionID} has missing data.");
                }
            }

            if (errors.Length > 0)
            {
                var errorLogPath = Path.Combine(_baseDirectory, "errors.log");
                await File.WriteAllTextAsync(errorLogPath, errors.ToString());
                throw new ApplicationException($"Errors occurred while processing the CSV file. Check {errorLogPath} for details.");
            }

            return transactions;
        }
        private decimal CalculateRemainingAmount(IEnumerable<TransactionProcessDto> transactions)
        {
            return transactions.Where(t => t.Direction == "In").Sum(t => t.Amount) -
                   transactions.Where(t => t.Direction == "Out").Sum(t => t.Amount);
        }
    }
}
