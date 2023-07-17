using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdulluReklam : MonoBehaviour
{
#if UNITY_EDITOR
    string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#else
    string adUnitId = "unused";
#endif

    RewardedAd odulluReklam;

    void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {

        });

        OdulluReklamOlustur();
    }

    private void OdulluReklamOlustur()
    {
        if (odulluReklam != null)
        {
            odulluReklam.Destroy();
            odulluReklam = null;
        }

        AdRequest adRequest = new AdRequest.Builder().Build();





        RewardedAd.Load(adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("Ödüllü reklam yüklenirken hata oluştu \n Hata: " + error);
                    return;
                }
                odulluReklam = ad;
            });

        ReklamOlaylariniDinle(odulluReklam);
    }


    private void ReklamOlaylariniDinle(RewardedAd ad)
    {
        ad.OnAdPaid += (AdValue AdValue) =>
        {
            Debug.Log(string.Format("Ödüllü reklam {0} {1}", AdValue.Value, AdValue.CurrencyCode));
        };

        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Ödüllü reklam göstertildi");
        };

        ad.OnAdClicked += () =>
        {
            Debug.Log("Ödüllü reklam tıklandı");
        };

        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Ödüllü reklam tam ekran açıldı");
        };

        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Ödüllü reklam tam ekran kapandı");
            OdulluReklamOlustur();
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.Log("Ödüllü reklam tam ekran açılamadı \n Hata:" + error);
            OdulluReklamOlustur();
        };
    }

    public void ReklamYokEt()
    {
        odulluReklam.Destroy();
    }

    public void OdulluReklamGoster()
    {
        if (odulluReklam != null && odulluReklam.CanShowAd())
        {
            odulluReklam.Show((Reward reward) =>
            {
                string odulMesaji = $"Ödül Kazanıldı. Ürün: {reward.Type}, Değer {reward.Amount}";
                Debug.Log(odulMesaji);
            });
        }
        else
        {
            Debug.Log("Ödüllü Reklam henüz hazır değil");
        }
    }
}
