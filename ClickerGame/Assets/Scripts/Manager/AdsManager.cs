using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Manager
{
    public class AdsManager
    {
        private RewardedAd rewardedAd;

        private readonly string TestRewardID = "ca-app-pub-3940256099942544/5224354917";
        
        public void Initialize()
        {
            MobileAds.Initialize(initStatus =>
                CreateAndLoadRewardedAd());
        }

        public void LoadAd(EventHandler<Reward> reward)
        {
            if (rewardedAd.IsLoaded())
            {
                rewardedAd.OnUserEarnedReward += reward;
                rewardedAd.Show();
            }
        }

        private void HandleRewardedAdClosed(object sender, EventArgs args)
        {
            CreateAndLoadRewardedAd();
        }
        
        private void CreateAndLoadRewardedAd()
        {
            rewardedAd = new RewardedAd(TestRewardID);

            
            rewardedAd.OnAdClosed += HandleRewardedAdClosed;
                
            AdRequest request = new AdRequest.Builder().Build();
            rewardedAd.LoadAd(request);
        }
    }
}