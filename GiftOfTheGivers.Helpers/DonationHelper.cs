namespace GiftOfTheGivers.Helpers;

public static class DonationHelper
{
    public static string FormatCertificateNumber(DateTime date, int sequence)
    {
        if (sequence < 0)
            throw new ArgumentOutOfRangeException(nameof(sequence));

        return $"GOTG-{date:yyyyMMdd}-{sequence:D5}";
    }

    public static decimal CalculateDonationTotal(decimal amount, decimal additionalAmount = 0)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        if (additionalAmount < 0)
            throw new ArgumentOutOfRangeException(nameof(additionalAmount));

        return amount + additionalAmount;
    }
}
