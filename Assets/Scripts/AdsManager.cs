using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;


public class AdsManager : MonoBehaviour
{
    private BannerView _banner1, _banner2, _banner3, _banner4;
    private RewardedAd _rewardedAd;
    private string[] _adUnitId;
    public static AdsManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        MobileAds.Initialize(initStatus => { });
    }

    public void Start()
    {
#if UNITY_ANDROID
        string[] adUnitId =
        {
            "ca-app-pub-4174137669541969/8858228061",
            "ca-app-pub-4174137669541969/4918983053",
            "ca-app-pub-4174137669541969/3527038008",
            "ca-app-pub-4174137669541969/8147975145",
            "ca-app-pub-4174137669541969/3737763326",
            "ca-app-pub-4174137669541969/6245890056"
        };
        string rewardAdId = "ca-app-pub-4174137669541969/5186576363";
#else
        _adUnitId = "unexpected_platform";
#endif
        _adUnitId = adUnitId;
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            ShowAds(1);
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
        return banner;
    }

    public void HideAds(int i)
    {
        switch (i)
        {
            case 0: _banner1.Hide();
                _banner1.Destroy();
                break;
            case 1: _banner2.Hide();
                _banner2.Destroy();
                break;
            case 2: _banner1.Hide();
                _banner1.Destroy();
                break;
            case 3: _banner2.Hide();
                _banner2.Destroy();
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
            case 2: _banner1 = RequestBanner(_adUnitId[2], AdPosition.Top);
                break;
            case 3: _banner2 = RequestBanner(_adUnitId[3], AdPosition.Bottom);
                break;
            case 4: _banner3 = RequestBanner(_adUnitId[4], AdPosition.Top);
                break;
            case 5: _banner4 = RequestBanner(_adUnitId[5], AdPosition.Bottom);
                break;
        }
    }

    void OnDestroy()
    {
        _banner1?.Destroy();
        _banner2?.Destroy();
        _banner3?.Destroy();
        _banner4?.Destroy();
    }

    public void RewardedVideo()
    {
        _rewardedAd.LoadAd(new AdRequest.Builder().Build());
    }
    
    private void HandleUserEarnedReward(object sender, Reward args)
    {
       GameManager.instance.Continue();
    }
}