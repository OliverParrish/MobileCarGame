using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{ 
    public BannerAdExample bannerAd;

    public void Awake()
    {
        bannerAd.LoadBanner();
    }
    public void Start()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        bannerAd.ShowBannerAd();
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("GameScene");
        bannerAd.HideBannerAd();
    }
}
