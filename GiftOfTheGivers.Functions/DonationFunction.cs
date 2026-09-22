using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace GiftOfTheGivers.Functions;

public class DonationFunction
{
    [Function("DonationFunction")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")]
        HttpRequestData req)
    {
        try
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var donation = JsonSerializer.Deserialize<DonationRequest>(
                requestBody,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (donation is null)
                return await Error(req, HttpStatusCode.BadRequest, "Invalid donation information.");

            if (string.IsNullOrWhiteSpace(donation.DonorName))
                return await Error(req, HttpStatusCode.BadRequest, "Donor name is required.");

            if (donation.Amount <= 0)
                return await Error(req, HttpStatusCode.BadRequest, "Donation amount must be greater than zero.");

            donation.Currency = string.IsNullOrWhiteSpace(donation.Currency) ? "ZAR" : donation.Currency;
            donation.DonationType = string.IsNullOrWhiteSpace(donation.DonationType) ? "One-Time" : donation.DonationType;

            var certificateNumber = $"GOTG-{DateTime.UtcNow:yyyyMMdd}-{Random.Shared.Next(10000, 99999)}";

            var result = new DonationResponse
            {
                Success = true,
                Message = "Donation processed successfully by the Azure Function.",
                DonorName = donation.DonorName,
                Amount = donation.Amount,
                Currency = donation.Currency,
                DonationType = donation.DonationType,
                CertificateNumber = certificateNumber,
                DonationDate = DateTime.UtcNow
            };

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(result);
            return response;
        }
        catch
        {
            return await Error(
                req,
                HttpStatusCode.InternalServerError,
                "An error occurred while processing the donation.");
        }
    }

    private static async Task<HttpResponseData> Error(
        HttpRequestData req,
        HttpStatusCode statusCode,
        string message)
    {
        var response = req.CreateResponse(statusCode);
        await response.WriteAsJsonAsync(new { success = false, message });
        return response;
    }
}

public class DonationRequest
{
    public string DonorName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "ZAR";
    public string DonationType { get; set; } = "One-Time";
}

public class DonationResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string DonorName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string DonationType { get; set; } = string.Empty;
    public string CertificateNumber { get; set; } = string.Empty;
    public DateTime DonationDate { get; set; }
}
