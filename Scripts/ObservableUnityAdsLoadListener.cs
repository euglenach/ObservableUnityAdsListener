using System;
using UniRx;
using UnityEngine;
using UnityEngine.Advertisements;

namespace ObservableUnityAdsListener
{
    public class ObservableUnityAdsLoadListener : IUnityAdsLoadListener, IDisposable
    {
        private readonly Subject<OnUnityAdsAdLoadedResult> onUnityAdsAdLoaded = new();
        public IObservable<OnUnityAdsAdLoadedResult> OnUnityAdsAdLoaded => onUnityAdsAdLoaded;
        private readonly Subject<OnUnityAdsFailedToLoadResult> onUnityAdsFailedToLoad = new();
        public IObservable<OnUnityAdsFailedToLoadResult> OnUnityAdsFailedToLoad => onUnityAdsFailedToLoad;
        void IUnityAdsLoadListener.OnUnityAdsAdLoaded(string placementId)
        {
            onUnityAdsAdLoaded.OnNext(new OnUnityAdsAdLoadedResult(placementId));
        }

        void IUnityAdsLoadListener.OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            onUnityAdsFailedToLoad.OnNext(new OnUnityAdsFailedToLoadResult(placementId, error, message));
        }

        public void Dispose()
        {
            onUnityAdsAdLoaded?.Dispose();
            onUnityAdsFailedToLoad?.Dispose();
        }
    }
    
    #region EventArgs

    public readonly struct OnUnityAdsAdLoadedResult
    {
        public readonly string PlacementId;

        public OnUnityAdsAdLoadedResult(string placementId)
        {
            PlacementId = placementId;
        }
    }
    
    public readonly struct OnUnityAdsFailedToLoadResult
    {
        public readonly string PlacementId;
        public readonly UnityAdsLoadError Error;
        public readonly string Message;
        
        public OnUnityAdsFailedToLoadResult(string placementId, UnityAdsLoadError error, string message)
        {
            this.PlacementId = placementId;
            this.Error = error;
            this.Message = message;
        }
    }
    
    #endregion
}
