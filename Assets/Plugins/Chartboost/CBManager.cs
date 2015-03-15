using UnityEngine;
using System;
using System.Collections;


namespace Chartboost {
	public class CBManager : MonoBehaviour {

#if UNITY_ANDROID || UNITY_IPHONE
		
#if UNITY_IPHONE
		[System.Runtime.InteropServices.DllImport("__Internal")]
		private static extern void _chartBoostPauseUnity();
#endif
	
		/// Fired when an interstitial fails to load
		/// First parameter is the location.
		public static event Action<string> didFailToLoadInterstitialEvent;
	
		/// Fired when an interstitial is finished via any method
		/// This will always be paired with either a close or click event
		/// First parameter is the location.
		public static event Action<string> didDismissInterstitialEvent;
		
		/// Fired when an interstitial is closed (i.e. by tapping the X or hitting the Android back button)
		/// First parameter is the location.
		public static event Action<string> didCloseInterstitialEvent;
		
		/// Fired when an interstitial is clicked
		/// First parameter is the location.
		public static event Action<string> didClickInterstitialEvent;
	
		/// Fired when an interstitial is cached
		/// First parameter is the location.
		public static event Action<string> didCacheInterstitialEvent;
	
		/// Fired when an interstitial is shown
		/// First parameter is the location.
		public static event Action<string> didShowInterstitialEvent;
		
		/// Fired when the more apps screen fails to load
		public static event Action didFailToLoadMoreAppsEvent;
	
		/// Fired when the more apps screen is finished via any method
		/// This will always be paired with either a close or click event
		public static event Action didDismissMoreAppsEvent;
		
		/// Fired when the more apps screen is closed (i.e. by tapping the X or hitting the Android back button)
		public static event Action didCloseMoreAppsEvent;
		
		/// Fired when a listing on the more apps screen is clicked
		public static event Action didClickMoreAppsEvent;
	
		/// Fired when the more apps screen is cached
		public static event Action didCacheMoreAppsEvent;
	
		/// Fired when the more app screen is shown
		public static event Action didShowMoreAppsEvent;
	
		
		
		void Awake()
		{
			gameObject.name = "ChartBoostManager";
			DontDestroyOnLoad( gameObject );
		}
	
	
		public void didFailToLoadInterstitial( string location )
		{
			if( didFailToLoadInterstitialEvent != null )
				didFailToLoadInterstitialEvent( location );
		}
	
	
		public void didDismissInterstitial( string location )
		{
			doUnityPause(false);
			
			if( didDismissInterstitialEvent != null )
				didDismissInterstitialEvent( location );
		}
		
		
		public void didClickInterstitial( string location )
		{
			if( didClickInterstitialEvent != null )
				didClickInterstitialEvent( location );
		}

		
		public void didCloseInterstitial( string location )
		{
			if( didCloseInterstitialEvent != null )
				didCloseInterstitialEvent( location );
		}
	
	
		public void didCacheInterstitial( string location )
		{
			if( didCacheInterstitialEvent != null )
				didCacheInterstitialEvent( location );
		}
	
	
		public void didShowInterstitial( string location )
		{
			doUnityPause(true);
#if UNITY_IPHONE
			_chartBoostPauseUnity();
#endif
			
			if( didShowInterstitialEvent != null )
				didShowInterstitialEvent( location );
		}
	
	
		public void didFailToLoadMoreApps( string empty )
		{
			if( didFailToLoadMoreAppsEvent != null )
				didFailToLoadMoreAppsEvent();
		}
		
	
		public void didDismissMoreApps( string empty )
		{
			doUnityPause(false);
			
			if( didDismissMoreAppsEvent != null )
				didDismissMoreAppsEvent();
		}
		
		
		public void didClickMoreApps( string empty )
		{
			if( didClickMoreAppsEvent != null )
				didClickMoreAppsEvent();
		}

		
		public void didCloseMoreApps( string empty )
		{
			if( didCloseMoreAppsEvent != null )
				didCloseMoreAppsEvent();
		}
	
	
		public void didCacheMoreApps( string empty )
		{
			if( didCacheMoreAppsEvent != null )
				didCacheMoreAppsEvent();
		}
	
	
		public void didShowMoreApps( string empty )
		{
			doUnityPause(true);
#if UNITY_IPHONE
			_chartBoostPauseUnity();
#endif
			
			if( didShowMoreAppsEvent != null )
				didShowMoreAppsEvent();
		}
		
		// Utility methods
		
		/// var used internally for managing game pause state
		private static bool isPaused = false;
		
		/// Manages pausing
		private static void doUnityPause(bool pause) {
#if UNITY_ANDROID
			bool useCustomPause = true;
#endif
			if (pause) {
#if UNITY_ANDROID
				if (isPaused) {
					useCustomPause = false;
				}
#endif
				isPaused = true;
#if UNITY_ANDROID
				if (useCustomPause && !CBBinding.getImpressionsUseActivities())
					doCustomPause(pause);
#endif
			} else {
#if UNITY_ANDROID
				if (!isPaused) {
					useCustomPause = false;
				}
#endif
				isPaused = false;
#if UNITY_ANDROID
				if (useCustomPause && !CBBinding.getImpressionsUseActivities())
					doCustomPause(pause);
#endif
			}
		}
		
		public static bool isImpressionVisible() {
			return isPaused;
		}
		
#if UNITY_ANDROID
		/// Var used for custom pause method
		private static float lastTimeScale = 0;
		
		/// Update this method if you would like to change how your game is paused
		///   when impressions are shown.  This method is only called if you call
		///   CBAndroidBinding.setImpressionsUseActivities(false).  Otherwise,
		///   your Unity app is halted at a much higher level
		private static void doCustomPause(bool pause) {
			if (pause) {
				lastTimeScale = Time.timeScale;
				Time.timeScale = 0;
			} else {
				Time.timeScale = lastTimeScale;
			}
		}
#endif

#endif

	}
}

