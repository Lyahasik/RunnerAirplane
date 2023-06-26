using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

namespace RunnerAirplane
{
    public class AdsHandler : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private const string _keyAdsPurchased = "AdsPurchased";
        private const float _attemptDelayShow = 0.5f;
        private const float _delayNextVideoShow = 300f;

        [SerializeField] private string _gameIdIos;
        [SerializeField] private string _bannerUnitNameIos;
        [SerializeField] private string _videoUnitNameIos;

        [Space]
        [SerializeField] private string _gameIdAndroid;
        [SerializeField] private string _bannerUnitNameAndroid;
        [SerializeField] private string _videoUnitNameAndroid;
        
        [Space]
        [SerializeField] private bool _showBanner = true;
        [SerializeField] private bool _isTestMode = true;

        private string _gameId;
        
        private static string _bannerUnitName;
        private static string _videoUnitName;

        private static float _nextVideoShowTime;
        
        public static bool IsPurchased;

        private void Awake()
        {
            IsPurchased = PlayerPrefs.HasKey(_keyAdsPurchased);

            _nextVideoShowTime = Time.time;
            
#if UNITY_IOS
    InitIosValues();
#else
    InitAndroidValues();
#endif
        }

        private void Start()
        {
            if (IsPurchased)
                return;
            
            InitAds();

            StartCoroutine(ShowBanner());
        }

        private void InitAds()
        {
            Advertisement.Initialize(_gameId, _isTestMode, this);
            
            Advertisement.Load(_videoUnitName, this);
            
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Load(_bannerUnitName);
        }

        private void InitIosValues()
        {
            _gameId = _gameIdIos;

            _bannerUnitName = _bannerUnitNameIos;
            _videoUnitName = _videoUnitNameIos;
        }

        private void InitAndroidValues()
        {
            _gameId = _gameIdAndroid;

            _bannerUnitName = _bannerUnitNameAndroid;
            _videoUnitName = _videoUnitNameAndroid;
        }

        private IEnumerator ShowBanner()
        {
            if (!_showBanner)
                yield break;
            
            var delay = new WaitForSeconds(_attemptDelayShow);
            
            while (!Advertisement.isInitialized
                || !Advertisement.Banner.isLoaded)
            {
                yield return delay;
            }
            
            if (IsPurchased)
                yield break;
            
            Advertisement.Banner.Show(_bannerUnitName);
        }

        public void ShowAdsVideo()
        {
            if (IsPurchased
                || _nextVideoShowTime > Time.time)
                return;
            
            StartCoroutine(TryShowAds(_videoUnitName));
            _nextVideoShowTime = Time.time + _delayNextVideoShow;
        }

        private IEnumerator TryShowAds(string unitName)
        {
            var delay = new WaitForSeconds(_attemptDelayShow);

            while (!Advertisement.isInitialized)
            {
                yield return delay;
            }
            
            Advertisement.Show(unitName, this);
        }

        public static void TryToBuy()
        {
            if (IsPurchased)
                return;
            
            PlayerPrefs.SetInt(_keyAdsPurchased, 0);

            IsPurchased = true;
            Advertisement.Banner.Hide();
        }
        
        public void OnInitializationComplete() {}

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.LogError($"Init ads failed: {message}");
        }
        
        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log($"{placementId} loaded");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.LogError($"{placementId} failed to load: {message}");
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.LogError($"{placementId} show failure: {message}");
        }

        public void OnUnityAdsShowStart(string placementId) {}

        public void OnUnityAdsShowClick(string placementId) {}

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState) {}
    }
}
