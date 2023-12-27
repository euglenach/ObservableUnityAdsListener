using System;
using UnityEngine.Advertisements;

namespace ObservableUnityAdsListener
{
    public class ObservableUnityAdsListener : IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener, IDisposable
    {
        public static readonly ObservableUnityAdsListener Default = new();
        
        private readonly ObservableUnityAdsInitializationListener initializationListener = new();
        private readonly ObservableUnityAdsLoadListener loadListener = new();
        private readonly ObservableUnityAdsShowListener showListener = new();
        
        void IUnityAdsInitializationListener.OnInitializationComplete()
        {
            ((IUnityAdsInitializationListener)initializationListener).OnInitializationComplete();
        }

        void IUnityAdsInitializationListener.OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            ((IUnityAdsInitializationListener)initializationListener).OnInitializationFailed(error, message);
        }

        void IUnityAdsLoadListener.OnUnityAdsAdLoaded(string placementId)
        {
            ((IUnityAdsLoadListener)loadListener).OnUnityAdsAdLoaded(placementId);
        }

        void IUnityAdsLoadListener.OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            ((IUnityAdsLoadListener)loadListener).OnUnityAdsFailedToLoad(placementId, error, message);
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            ((IUnityAdsShowListener)showListener).OnUnityAdsShowFailure(placementId, error, message);
        }

        void IUnityAdsShowListener.OnUnityAdsShowStart(string placementId)
        {
            ((IUnityAdsShowListener)showListener).OnUnityAdsShowStart(placementId);
        }

        void IUnityAdsShowListener.OnUnityAdsShowClick(string placementId)
        {
            ((IUnityAdsShowListener)showListener).OnUnityAdsShowClick(placementId);
        }

        void IUnityAdsShowListener.OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            ((IUnityAdsShowListener)showListener).OnUnityAdsShowComplete(placementId, showCompletionState);
        }

        public void Dispose()
        {
            initializationListener?.Dispose();
            loadListener?.Dispose();
            showListener?.Dispose();
        }
    }
}