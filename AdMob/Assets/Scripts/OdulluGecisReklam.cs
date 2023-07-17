using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdulluGecisReklam : MonoBehaviour
{
#if UNITY_EDITOR
    string adUnitId = "ca-app-pub-3940256099942544/5354046379";
#else
    string adUnitId = "unused";
#endif

    RewardedInterstitialAd odulluGecisReklam;

    void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {

        });

        OdulluGecisReklamOlustur();
    }

    private void OdulluGecisReklamOlustur()
    {
        if (odulluGecisReklam != null)
        {
            odulluGecisReklam.Destroy();
            odulluGecisReklam = null;
        }

        AdRequest adRequest = new AdRequest.Builder().Build();





        RewardedInterstitialAd.Load(adUnitId, adRequest,
            (RewardedInterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("Ödüllü geçiş reklamı yüklenirken hata oluştu \n Hata: " + error);
                    return;
                }
                odulluGecisReklam = ad;
            });

        ReklamOlaylariniDinle(odulluGecisReklam);
    }


    private void ReklamOlaylariniDinle(RewardedInterstitialAd ad)
    {
        ad.OnAdPaid += (AdValue AdValue) =>
        {
            Debug.Log(string.Format("Ödüllü geçiş reklamı {0} {1}", AdValue.Value, AdValue.CurrencyCode));
        };

        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Ödüllü geçiş reklamı göstertildi");
        };

        ad.OnAdClicked += () =>
        {
            Debug.Log("Ödüllü geçiş reklamı tıklandı");
        };

        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Ödüllü geçiş reklamı tam ekran açıldı");
        };

        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Ödüllü geçiş reklamı tam ekran kapandı");
            OdulluGecisReklamOlustur();
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.Log("Ödüllü geçiş reklamı tam ekran açılamadı \n Hata:" + error);
            OdulluGecisReklamOlustur();
        };
    }

    public void ReklamYokEt()
    {
        odulluGecisReklam.Destroy();
    }

    public void OdulluGecisReklamGoster()
    {
        if (odulluGecisReklam != null && odulluGecisReklam.CanShowAd())
        {
            odulluGecisReklam.Show((Reward reward) =>
            {
                string odulMesaji = $"Ödüllü Geçiş Kazanıldı. Ürün: {reward.Type}, Değer {reward.Amount}";
                Debug.Log(odulMesaji);
            });
        }
        else
        {
            Debug.Log("Ödüllü geçiş reklamı henüz hazır değil");
        }
    }














}
