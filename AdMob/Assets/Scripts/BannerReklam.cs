using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerReklam : MonoBehaviour
{
#if UNITY_EDITOR
    string adUnitID = "ca-app-pub-3940256099942544/6300978111";
#else
    string adUnityID = "unused";
#endif
    BannerView banner;
    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {

        });

        BannerYukle();


    }

    private void BannerOlustur()
    {
         if(banner != null)
        {
            banner.Destroy();
            banner = null;
        }

        banner = new BannerView(adUnitID, AdSize.Banner, AdPosition.Bottom);
    }

    private void BannerYukle()
    {
        if(banner == null ) {
            BannerOlustur();

            AdRequest adRequest = new AdRequest.Builder().Build();

            banner.LoadAd(adRequest);
            ReklamOlaylariDinle();
        }
    }

    private void ReklamOlaylariDinle()
    {
        banner.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner Yüklendi");
        };

        banner.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.Log("Banner Yüklenemedi \n HATA: " + error);
            BannerYukle();
        };
    }
}
