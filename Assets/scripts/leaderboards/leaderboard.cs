using System;
using UnityEngine;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp;  
using com.shephertz.app42.paas.sdk.csharp.game;  

public class leaderboard : MonoBehaviour {
	
	public static string API_KEY = "23c77d0a7397db2489fbe56166dxxxxxxxxxxxxxxxxxx";
	public static string SECRET_KEY = "43f1f411564a718d64ca20ab4xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
	string gameName = "ball_shootout"; 
	ServiceAPI api;
	ScoreBoardService scoreBoardService;
	
	void Start () {
		api = new ServiceAPI(API_KEY, SECRET_KEY);  
		scoreBoardService = api.BuildScoreBoardService();    
	}
	
	public void SendRecord(){
		string username = gameObject.GetComponent<gameScript>().username;
		double record = gameObject.GetComponent<gameScript>().record; 
		
		if(record > 2000)	
		scoreBoardService.SaveUserScore(gameName, username, record, new UnityCallBack());  
	}
	
	public class UnityCallBack : App42CallBack  
	{  
		public void OnSuccess(object response){
			GameObject.Find("gameScript").GetComponent<gameScript>().SentScore();
		
		} 		
		public void OnException(Exception e){App42Log.Console("Exception : " + e);}  
	}
	
	public void GetLeaderBoard(){	
		int max = 10;
		if(PlayerPrefs.GetInt("SentRecord", 0) < gameObject.GetComponent<gameScript>().record){
			SendRecord();
		}
	
		//App42Log.SetDebug(true);        //Print output in your editor console  
		scoreBoardService.GetTopNRankings(gameName, max, new LeaderBoardCallBack());  
		
	}
	
	public class LeaderBoardCallBack : App42CallBack  
	{  
		public void OnSuccess(object response)  
		{  
			string[] score_names = new string[10];
			double[] score_points = new double[10];
			
			Game game = (Game) response;  
			for(int i = 0;i<game.GetScoreList().Count;i++)  
			{  
				score_names[i] = game.GetScoreList()[i].GetUserName();  
				score_points[i] = game.GetScoreList()[i].GetValue();
			}    
			GameObject.Find("gameScript").GetComponent<gameScript>().score_names = score_names;
			GameObject.Find("gameScript").GetComponent<gameScript>().score_points = score_points;
		}  
		
		public void OnException(Exception e){ App42Log.Console("Exception : " + e);}  
	}  
	
	
	
	
}

