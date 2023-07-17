using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GecisReklami : MonoBehaviour
{
    // ca-app-pub-2250222834102640/1146620894

#if UNITY_EDITOR
    string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#else
    string adUnitId = "unused";
#endif

    InterstitialAd gecisReklami;

    void Start()
    {
        MobileAds.Initialize((InitializationStatus InitializationStatus) =>
        {
             
        });
        GecisReklamiOlustur();
    }

    private void GecisReklamiOlustur()
    {
        if(gecisReklami != null)
        {
            gecisReklami.Destroy();
            gecisReklami = null;
        }
        //Debug.Log("Reklam Yüklendi");

        AdRequest adRequest = new AdRequest.Builder().Build();

        InterstitialAd.Load(adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
        {
            if(error != null || ad == null) 
            {
                Debug.LogError("Reklam yüklenirken hata oluştu \n Hata: " + error);
                return;
            }
            gecisReklami = ad;
        });

        ReklamOlaylariniDinle(gecisReklami);

    }

    private void ReklamOlaylariniDinle(InterstitialAd ad)
    {
        ad.OnAdPaid += (AdValue AdValue) =>
        {
            Debug.Log(string.Format("Ücretli geçiş reklamı {0} {1}", AdValue.Value, AdValue.CurrencyCode));
        };

        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("geçiş reklamı göstertildi");
        };

        ad.OnAdClicked += () =>
        {
            Debug.Log("geçiş reklamı tıklandı");
        };

        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("geçiş reklamı tam ekran açıldı");
        };

        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("geçiş reklamı tam ekran kapandı");
            GecisReklamiOlustur();
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.Log("geçiş reklamı tam ekran açılamadı \n Hata:" + error);
            GecisReklamiOlustur();
        };
    }


    public void GecisReklamiGoster()
    {
        if (gecisReklami != null && gecisReklami.CanShowAd())
        {
            gecisReklami.Show();
            Debug.Log("Reklam Gösterildi");
        }
        else
            Debug.Log("Geçiş Reklamı Hazır Değil");
    }

    public void ReklamYokEt()
    {
        gecisReklami.Destroy();
    }
}
