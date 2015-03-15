using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class gameScript : MonoBehaviour {
	
	public int round, points, player, record;
	public int balls, max_balls, combo;
	public bool sfx, music, multi, new_record, power;
	public AudioClip net, rim_hit, rim_hit2, ground, ground2;
	public float sfxV = 1.0f, musicV = 1.0f;
	
	public string username;
	public string[] achvNames, score_names = new string[10];
	public int[][] achv;
	public int[] mpoints;
	public double[] score_points = new double[10];
	
	void Awake() {
		DontDestroyOnLoad(this.gameObject);	
		Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;
		Time.timeScale = 1.0f;
	}
	
	void Start () {
		if(!PlayerPrefs.HasKey("achivmentInfo1"))
			gameObject.GetComponent<aManager>().reSetAchivments();
		Reset();
		Load ();
		AutoFade.LoadLevel(1,0.5f,0.5f,Color.black);
		
	}
	
	//scoreScript callbacks
	public void FinishRound(int lpoints, int lcombo, int lballs){
		if(!multi)
			points += lpoints;
		else
			mpoints[player] += lpoints;
			
		combo = lcombo;
		balls = lballs;
		
		if(points > record){
			new_record = true;
			record = points;
			Save();
		}
		if(balls > 0)
			GameObject.Find("round_menu").GetComponent<roundEnd>().Set(lpoints, points, record, false, multi, mpoints, player);
		else{
			GameObject.Find("round_menu").GetComponent<roundEnd>().Set(lpoints, points, record, true, multi, mpoints, player);				
			GameObject.Find("ChartBoostManager").GetComponent<CBads>().ShowAd("game_over");		
		}
		
		if(balls <= 0 && new_record){
			gameObject.GetComponent<leaderboard>().SendRecord();
			new_record = false;
		}
		
		GameObject.Find("round_menu").GetComponent<roundEnd>().enabled = true;
		
	}
	
	public void NextRound(){
		round++;
		Save();
		Load();
		AutoFade.LoadLevel(2,0.5f,0.5f,Color.black);	
	}
	
	public void Restart(){
		Save();
		Reset();
		Load();
		AutoFade.LoadLevel(2,0.5f,0.5f,Color.black);
		
	}
	
	public void Sound(int id){
		if(sfx)
			switch(id){
				case 1: audio.PlayOneShot(net, sfxV);  break;
				case 2: if(Random.Range(0,2) > 1) audio.PlayOneShot(rim_hit, sfxV);
						else audio.PlayOneShot(rim_hit2, sfxV);  break;
				case 3: if(Random.Range(0,2) > 1) audio.PlayOneShot(ground, sfxV);
						else audio.PlayOneShot(ground2, sfxV);  break;
				case 4: if(Random.Range(0,2) > 1) audio.PlayOneShot(ground, sfxV/2);
						else audio.PlayOneShot(ground2, sfxV/2);  break;
				case 5: audio.PlayOneShot(ground, sfxV);  break;
		}
		
		
	}
	//
	//Multiplayer
	public void MultiRestart(int inplay = 0){
		Save();
		MultiReset(inplay);
		Load();
		AutoFade.LoadLevel(2,0.5f,0.5f,Color.black);
		
	}
	
	public void MultiNextRound(){
		round++;
		AutoFade.LoadLevel(2,0.5f,0.5f,Color.black);	
	}
	
	void MultiReset(int inplay){
		if(inplay == 0){
			mpoints = new int[2];
			player = 0;
		}
		else
			player = 1;
		multi = true;
		new_record = false;
		points = round = combo = 0;	
		balls = max_balls = 10;	
	
	}
	
	
	//
	
	void Load(){
		
		record = PlayerPrefs.GetInt("totalRecord", 0);
		gameObject.GetComponent<aManager>().GetAchivments();
		achv = gameObject.GetComponent<aManager>().achivments;
		
		for(int i = 0; i<3; i++)
			achvNames[i] = PlayerPrefs.GetString("achivmentInfo"+achv[i][0]);

		
		
		sfx = PlayerPrefs.GetInt("sfx", 1) == 1 ? true : false; 
		music = PlayerPrefs.GetInt("music", 1) == 1 ? true : false; 
		power = PlayerPrefs.GetInt("power", 0) == 1 ? true : false; 
		username = PlayerPrefs.GetString("name", "player");
	}
	
	void Save(){
		
		achv = gameObject.GetComponent<aManager>().achivments;
		PlayerPrefs.SetInt("totalRecord", record);
		
		if(achv != null)
		for(int i = 0; i < 3; i++)
			PlayerPrefs.SetInt("achivment"+achv[i][0], achv[i][1]);
		
		if(sfx) PlayerPrefs.SetInt("sfx", 1);
		else PlayerPrefs.SetInt("sfx", 0);
		
		if(power) PlayerPrefs.SetInt("power", 1);
		else  PlayerPrefs.SetInt("power", 0);
		
		PlayerPrefs.SetString("name", username);
		
		PlayerPrefs.Save();
	}
	
	public void SentScore(){
		PlayerPrefs.SetInt("SentRecord", record);
		PlayerPrefs.Save();	
	}
	
	public void ResetSave(){
		for(int i = 1; i <= 21; i++){
			PlayerPrefs.SetInt("achivment"+i, 0);
		}
		PlayerPrefs.SetInt("totalRecord", 0);
		PlayerPrefs.SetString("name", "player");
		PlayerPrefs.Save();
		
		Load();
	
	}	
	
	void Reset(){
		achv = new int[3][];
		for(int i = 0; i < 3; i++)
			achv[i] = new int[2];
		
		multi = new_record = false;
		achvNames = new string[3];
		points = round = combo = 0;	
		balls = max_balls = 10;	
		
	}
	
}
