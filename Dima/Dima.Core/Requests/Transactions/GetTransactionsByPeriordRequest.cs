namespace Dima.Core.Requests.Transactions;

public class GetTransactionsByPeriordRequest : PagedRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}