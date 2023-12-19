using System;
using UniRx;
using UnityEngine;
using UnityEngine.Advertisements;

namespace ObservableUnityAdsListener
{
    public class ObservableUnityAdsInitializationListener : IUnityAdsInitializationListener, IDisposable
    {
        private readonly Subject<Unit> onInitializationComplete = new();
        public IObservable<Unit> OnInitializationComplete => onInitializationComplete;
        
        private readonly Subject<OnInitializationFailedResult> onInitializationFailed = new();
        public IObservable<OnInitializationFailedResult> OnInitializationFailed => onInitializationFailed;
        
        void IUnityAdsInitializationListener.OnInitializationComplete()
        {
            onInitializationComplete.OnNext(Unit.Default);
        }

        void IUnityAdsInitializationListener.OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            onInitializationFailed.OnNext(new OnInitializationFailedResult(error, message));
        }
        
        public void Dispose()
        {
            onInitializationComplete?.Dispose();
            onInitializationFailed?.Dispose();
        }
    }
    
    #region EventArgs
    public readonly struct OnInitializationFailedResult
    {
        public readonly UnityAdsInitializationError Error;
        public readonly string Message;

        public OnInitializationFailedResult(UnityAdsInitializationError error, string message)
        {
            Error = error;
            Message = message;
        }
    }
    #endregion
}