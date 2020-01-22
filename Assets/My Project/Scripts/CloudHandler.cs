using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using GoogleMobileAds.Api;
using System;


public class CloudHandler : MonoBehaviour {
	public static CloudHandler stats;

	public float clickValue;
	public float totalClouds;
	public float cps;
	public Text totalCloudsText;
	public Text cpsText;
	public int x = 0;
	public string Achievement1=  "CgkI99TKipAJEAIQAQ";
	public string Achievement2 = "CgkI99TKipAJEAIQAg";
	public string Leaderboard = "CgkI99TKipAJEAIQBg";
	public shopHandler shopHandler;
	private InterstitialAd interstitial;


	public float saveCounter;
	public float updateTime;
	public float counter;
	public float adCounter;
	public bool minimumUpdateTime;
	public string Username;
	public bool LoggedIn;

	public Text GPlay;
	private bool done;
	void Start () {
		clickValue = 1;
		PlayGamesPlatform.Activate();
		RequestInterstitial ();
		adCounter = 100;
		LoginGooglePlay ();
	}	


	void Update () {
		if (cps > 1000) {
			if(cps > 1000000){
				if(cps > 1000000000){
					if(cps > 1000000000000){
						cpsText.text = (cps / 1000000000000).ToString ("F2") + " T" + " K/S";
					}else{
						cpsText.text = (cps / 1000000000).ToString ("F2") + " B" + " K/S";}
				}else{
					cpsText.text = (cps / 1000000).ToString ("F2") + " M" + " K/S";}
			}else{
				cpsText.text = (cps / 1000).ToString ("F2")  + " K" + " K/S";}
		} else {
			cpsText.text = cps + " K/S";
		}
		if (totalClouds > 1000) {
			if(totalClouds > 1000000){
				if(totalClouds > 1000000000){
					if(totalClouds > 1000000000000){
						totalCloudsText.text = (totalClouds / 1000000000000).ToString("F2") + " T" + " Killed";
					}else{
						totalCloudsText.text = (totalClouds / 1000000000).ToString("F2") + " B" + " Killed";}}
				else{
					totalCloudsText.text = (totalClouds / 1000000).ToString("F2") + " M" + " Killed";}
			}else{
				totalCloudsText.text = (totalClouds / 1000).ToString("F2") + " K" + " Killed";}
	 } else {
			totalCloudsText.text = totalClouds.ToString ("F2") + " Killed";
	
	}
		if( saveCounter > 30){ReportToLeaderboard(); saveCounter = 0;}
		if (adCounter > 180){ adCounter = 0; LoadAd ();}
		adCounter = adCounter + Time.deltaTime;
		saveCounter = saveCounter + Time.deltaTime;
		if (cps > 9000) {
			Social.ReportProgress ("CgkI99TKipAJEAIQBA", 100.0f, (bool success) => {
			
			});
		}
		if (minimumUpdateTime) {
			updateTime = Time.deltaTime;
		}

			totalClouds+= cps*updateTime; 
			counter = 0;
	}
	public float ScreenTapped(){

		totalClouds += clickValue;


		return clickValue;
	}

	public void SignOut(){

		PlayGamesPlatform.Instance.SignOut();
		GPlay.text = "Please Login to Google Play";
		LoadAd ();
	}
	public void Login(){
		bool SLogin = LoginGooglePlay ();

	}
	public bool LoginGooglePlay(){
		GPlay.text = "Attempting to log in" +
			"..........";
		Social.localUser.Authenticate ((bool success) => 
		   {
			if (success)
			{Username = ((PlayGamesLocalUser)Social.localUser).userName;
				GPlay.text = Username;
				LoggedIn = true;
				UnlockAchievement1 ();
				done = true;
				LoadAd ();
			}

			else{
				LoggedIn = false;
				GPlay.text = "Failed to log in," +
					"Tap to retry";

			}

		   });

		if (done == true) {
			return true;}
		else{return false;}

	}
	

	public void UnlockAchievement1(){
		Social.ReportProgress (Achievement1, 100.0f,(bool success) => {

		});



	}

	private void RequestInterstitial()
	{
		#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-2627657376746744/5417870314";
		#elif UNITY_IPHONE
		string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
		#else
		string adUnitId = "ca-app-pub-2627657376746744/5417870314";
		#endif
		
		// Initialize an InterstitialAd.
		interstitial = new InterstitialAd(adUnitId);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		interstitial.LoadAd(request);

	}
	public void LoadAd()
	{
		interstitial.Show();
		interstitial.Destroy ();
		RequestInterstitial ();
	}



	public void ReportToLeaderboard(){
		long arg2 = (long)cps;
		Social.ReportScore (arg2, Leaderboard, (bool success) => {

		});


	}

	public void ShowLeaderboard(){
		Social.ShowLeaderboardUI ();
		LoadAd ();
	}

	public void ShowAchievements(){
		Social.ShowAchievementsUI ();
	}
}
