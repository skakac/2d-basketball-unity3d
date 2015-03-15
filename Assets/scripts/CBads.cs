using UnityEngine;
using System.Collections;
using Chartboost;

public class CBads : MonoBehaviour {

	public bool shown = false;
	// Use this for initialization
	void Start () {
		#if UNITY_ANDROID
		CBBinding.init();
		#endif
		
		CBBinding.cacheInterstitial(null);
		CBBinding.showInterstitial(null);
	}
	public void ShowAd(string location = null){
		CBBinding.showInterstitial(location);
	}

	void OnEnable()
	{
		// Listen to all impression-related events
		CBManager.didFailToLoadInterstitialEvent += didFailToLoadInterstitialEvent;
		CBManager.didDismissInterstitialEvent += didDismissInterstitialEvent;
		CBManager.didCloseInterstitialEvent += didCloseInterstitialEvent;
		CBManager.didClickInterstitialEvent += didClickInterstitialEvent;
		CBManager.didShowInterstitialEvent += didShowInterstitialEvent;
	}
	
	
	void OnDisable()
	{
		// Remove event handlers
		CBManager.didFailToLoadInterstitialEvent -= didFailToLoadInterstitialEvent;
		CBManager.didDismissInterstitialEvent -= didDismissInterstitialEvent;
		CBManager.didCloseInterstitialEvent -= didCloseInterstitialEvent;
		CBManager.didClickInterstitialEvent -= didClickInterstitialEvent;
		CBManager.didShowInterstitialEvent -= didShowInterstitialEvent;
	}
	void didFailToLoadInterstitialEvent( string location ){
		shown = false;
	}
	
	void didDismissInterstitialEvent( string location ){
		CBBinding.cacheInterstitial(location);
		shown = true;
	}
	
	void didCloseInterstitialEvent( string location ){
		shown = true;
	}
	
	void didClickInterstitialEvent( string location ){
		shown = true;
	}
		
	void didShowInterstitialEvent( string location ){
		shown = true;
	}
	



	
}
