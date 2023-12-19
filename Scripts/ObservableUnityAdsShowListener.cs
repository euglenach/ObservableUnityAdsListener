using System;
using UniRx;
using UnityEngine.Advertisements;

namespace ObservableUnityAdsListener
{
    public class ObservableUnityAdsShowListener : IUnityAdsShowListener, IDisposable
    {
        private readonly Subject<OnUnityAdsShowFailureResult> onUnityAdsShowFailure = new();
        public IObservable<OnUnityAdsShowFailureResult> OnUnityAdsShowFailure => onUnityAdsShowFailure;
        private readonly Subject<OnUnityAdsShowStartResult> onUnityAdsShowStart = new();
        public IObservable<OnUnityAdsShowStartResult> OnUnityAdsShowStart => onUnityAdsShowStart;
        private readonly Subject<OnUnityAdsShowClickResult> onUnityAdsShowClick = new();
        public IObservable<OnUnityAdsShowClickResult> OnUnityAdsShowClick => onUnityAdsShowClick;
        private readonly Subject<OnUnityAdsShowCompleteResult> onUnityAdsShowComplete = new();
        public IObservable<OnUnityAdsShowCompleteResult> OnUnityAdsShowComplete => onUnityAdsShowComplete;
        
        void IUnityAdsShowListener.OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            onUnityAdsShowFailure.OnNext(new OnUnityAdsShowFailureResult(placementId, error, message));
        }

        void IUnityAdsShowListener.OnUnityAdsShowStart(string placementId)
        {
            onUnityAdsShowStart.OnNext(new OnUnityAdsShowStartResult(placementId));
        }

        void IUnityAdsShowListener.OnUnityAdsShowClick(string placementId)
        {
            onUnityAdsShowClick.OnNext(new OnUnityAdsShowClickResult(placementId));
        }

        void IUnityAdsShowListener.OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            onUnityAdsShowComplete.OnNext(new OnUnityAdsShowCompleteResult(placementId, showCompletionState));
        }

        public void Dispose()
        {
            onUnityAdsShowFailure?.Dispose();
            onUnityAdsShowStart?.Dispose();
            onUnityAdsShowClick?.Dispose();
            onUnityAdsShowComplete?.Dispose();
        }
    }
    
    #region EventArgs
    public readonly struct OnUnityAdsShowFailureResult
    {
        private readonly string PlacementId;
        private readonly UnityAdsShowError Error;
        private readonly string Message;

        public OnUnityAdsShowFailureResult(string placementId, UnityAdsShowError error, string message)
        {
            PlacementId = placementId;
            Error = error;
            Message = message;
        }
    }
    
    public readonly struct OnUnityAdsShowStartResult
    {
        private readonly string PlacementId;

        public OnUnityAdsShowStartResult(string placementId)
        {
            PlacementId = placementId;
        }
    }
    public readonly struct OnUnityAdsShowClickResult
    {
        private readonly string PlacementId;

        public OnUnityAdsShowClickResult(string placementId)
        {
            PlacementId = placementId;
        }
    }
    
    public readonly struct OnUnityAdsShowCompleteResult
    {
        public readonly string PlacementId;
        public readonly UnityAdsShowCompletionState ShowCompletionState;

        public OnUnityAdsShowCompleteResult(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            PlacementId = placementId;
            ShowCompletionState = showCompletionState;
        }
    }
    
    #endregion
}
