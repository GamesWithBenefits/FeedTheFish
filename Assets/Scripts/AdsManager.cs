using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;


public class AdsManager : MonoBehaviour
{
    private BannerView _banner1, _banner2, _banner3;
    private RewardedAd _rewardedAd;
    private string[] _adUnitId;
    public static AdsManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        MobileAds.Initialize(initStatus => { });
    }

    public void Start()
    {
#if UNITY_ANDROID
        string[] adUnitId =
        {
           "ca-app-pub-4174137669541969/4918983053",
            "ca-app-pub-4174137669541969/3737763326",
            "ca-app-pub-4174137669541969/3527038008" 
           
           /* "ca-app-pub-3940256099942544/6300978111",
           "ca-app-pub-3940256099942544/6300978111",
           "ca-app-pub-3940256099942544/6300978111" */
        };
        string rewardAdId = "ca-app-pub-4174137669541969/5186576363";
        //string rewardAdId = "ca-app-pub-3940256099942544/5224354917";
#else
        _adUnitId = "unexpected_platform";
#endif
        _adUnitId = adUnitId;
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            ShowAds(0);
        }
        else
        {
            _rewardedAd = new RewardedAd(rewardAdId);
            _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            _rewardedAd.OnAdLoaded += (o, args) => {_rewardedAd.Show(); };

        }
    }

    private BannerView RequestBanner(string adUnit, AdPosition position)
    {
        BannerView banner = new BannerView(adUnit, AdSize.Banner, position);
        banner.LoadAd(new AdRequest.Builder().Build());
        banner.OnAdLoaded += (o, args) => { banner.Show();};
        return banner;
    }

    public void HideAds(int i)
    {
        switch (i)
        {
            case 0: _banner1.Hide();
                _banner1.Destroy();
                break;
            case 1:
                if (_banner2 != null)
                {
                    _banner2.Hide();
                    _banner2.Destroy();
                }
                break;
            case 2:
                if (_banner3 != null)
                {
                    _banner3.Hide();
                    _banner3.Destroy();
                }
                
                break;
        }
    }
    
    public void ShowAds(int i)
    {
        switch (i)
        {
            case 0: _banner1 = RequestBanner(_adUnitId[0], AdPosition.Top);
                break;
            case 1: _banner2 = RequestBanner(_adUnitId[1], AdPosition.Bottom);
                break;
            case 2: _banner3 = RequestBanner(_adUnitId[2], AdPosition.Bottom);
                break;
        }
    }

    void OnDestroy()
    {
        _banner1?.Destroy();
        _banner2?.Destroy();
        _banner3?.Destroy();
    }

    public void RewardedVideo()
    {
        _rewardedAd.LoadAd(new AdRequest.Builder().Build());
    }
    
    private void HandleUserEarnedReward(object sender, Reward args)
    {
       GameManager.Instance.Continue();
    }
}