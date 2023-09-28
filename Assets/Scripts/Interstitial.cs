using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine;

public class Interstitial : MonoBehaviour
{
 

    public static void InitializeInterstitial()
    {
        MobileAds.Initialize((InitializationStatus initStatus) => { });
    }


    // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
    private static string _adUnitId = "ca-app-pub-4507368160675520/5723968578";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
  private string _adUnitId = "unused";
#endif

    private static InterstitialAd _interstitialAd;

  /// <summary>
  /// Loads the interstitial ad.
  /// </summary>
  public static void LoadLoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                _interstitialAd = ad;
            });
    }

    /// <summary>
    /// Shows the interstitial ad.
    /// </summary>
    public static void ShowInterstitialAd()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            _interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }

    public void OnInterstitialAdClosed()
    {
        // Do not modify Time.timeScale here.
        // Add any logic you need to handle after the ad is closed.
    }

}