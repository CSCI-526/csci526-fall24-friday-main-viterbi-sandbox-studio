using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class AnalyticsManager : MonoBehaviour
{
    private async void Awake()
    {
        // Initialize Unity Services (which includes Analytics)
        await InitializeServices();

        // Once initialized, check for player consent
        if (PlayerHasConsented())
        {
            // Start data collection for Analytics if the player has consented
            AnalyticsService.Instance.StartDataCollection();
        }
    }

    // Initialize Unity Services
    private async Task InitializeServices()
    {
        try
        {
            await UnityServices.InitializeAsync();
            Debug.Log("Unity Services Initialized successfully.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error initializing Unity Services: " + ex.Message);
        }
    }

    // Mock function to check for player consent (implement your own consent logic)
    private bool PlayerHasConsented()
    {
        // Replace this with actual logic to check if the player has given consent
        return true;  // For now, assume consent is given
    }
}
