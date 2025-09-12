
namespace CP.VPOS.Enums
{
    public enum SaleQueryResponseStatu
    {
        Error = 0,
        Found = 1,
        NotFound = 2
    }

    public enum SaleQueryTransactionStatu
    {
        Paid = 1,
        Refunded = 2,
        PartialRefunded = 3,
        Voided = 4
    }
}