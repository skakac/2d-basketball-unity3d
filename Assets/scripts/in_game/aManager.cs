using UnityEngine;
using System.Collections;

public class aManager : MonoBehaviour {
	
	public int[][] achivments;
	public int num_achivments = 15, cleansT;
	public bool clean = false; 
			
	void Awake(){
		Reset();
	}
	
	void Reset(){
		achivments = new int[3][];
		for(int i = 0; i < 3; i++)
			achivments[i] = new int[2];
		
		cleansT = 0;
		clean = false;	
	}
	
	public void GetAchivments(){
		for(int i = 1, num = 0; i <= 21 && num < 3; i++){
			if(PlayerPrefs.GetInt("achivment"+i) == 0){			
				achivments[num][0] = i;
				achivments[num][1] = 0;
				num++;
			}
		}
	}
	
	public int GetDone(){
		int done = 0;
		for(int i = 1; i<=21; i++){
			if(PlayerPrefs.GetInt("achivment"+i) == 1)
				done++;
		}
		return done;
	}
	
	public bool Check(int inrow, int cleans, int tricks, bool cleanb) {
		bool done = false;
		if(gameObject.GetComponent<gameScript>().multi)
			return false;
		int points = gameObject.GetComponent<gameScript>().points;
		int round = gameObject.GetComponent<gameScript>().round;
		
		if(cleanb) cleansT++;
		bool check = false;
		if(cleans > 0)
			clean = true;
			
		for(int i = 0; i < 3; i++){
			if(achivments[i][1] == 1) break;
			
			switch(achivments[i][0]){
				case 1:  if(inrow >= 5) 				  	check = true; break;					
				case 2:  if(tricks >= 1) 				  	check = true; break;					
				case 3:  if(cleans >= 5)				  	check = true; break;					
				case 4:  if(points >= 7000) 			  	check = true; break;					
				case 5:  if(!clean && round >= 3) 		  	check = true; break;					
				case 6:  if(points >= 5000 && round <= 4) 	check = true; break;					
				case 7:  if(cleansT >= 15 && round <= 3)  	check = true; break;					
				case 8:  if(inrow >= 10) 					check = true; break;					
				case 9:  if(tricks >= 3) 					check = true; break;					
				case 10: if(round >= 10) 					check = true; break;					
				case 11: if(points >=  15000) 				check = true; break;					
				case 12: if(cleans >= 9) 					check = true; break;
				case 13: if(round >= 12 && points >= 16000)	check = true; break;
				case 14: if(cleansT >= 20 && round <= 3)	check = true; break;
				case 15: if(!clean && round >= 5)			check = true; break;
				case 16: if(cleans >= 10) 					check = true; break;
				case 17: if(points >=  18000)				check = true; break;
				case 18: if(cleans >= 12) 					check = true; break;
				case 19: if(inrow >= 15) 					check = true; break;
				case 20: if(points >=  20000)				check = true; break;
				case 21: if(round >= 20)	 				check = true; break;
			}
				
			if(check){
				PlayerPrefs.SetInt("achivment"+achivments[i][0], 1);
				achivments[i][1] = 1;
				done = true;
			}
			check = false;
		}
		
		return done;
		
	}
		
	public void reSetAchivments(){
		PlayerPrefs.SetString("achivmentInfo1", "Get 5 shots in a row");
		PlayerPrefs.SetString("achivmentInfo2", "Score a trick shot");
		PlayerPrefs.SetString("achivmentInfo3", "Get 5 clean shots in one round");		
		PlayerPrefs.SetString("achivmentInfo4", "Get 7000 points");		
		PlayerPrefs.SetString("achivmentInfo5", "Get to 3rd round without clean shot");
		PlayerPrefs.SetString("achivmentInfo6", "Get 5000 points before 4th round");		
		PlayerPrefs.SetString("achivmentInfo7", "Get 15 clean shots before 3rd round");
		PlayerPrefs.SetString("achivmentInfo8", "Score 10 shots in a row");
		PlayerPrefs.SetString("achivmentInfo9", "Score 3 trick shots");
		PlayerPrefs.SetString("achivmentInfo10", "Get to round 10");
		PlayerPrefs.SetString("achivmentInfo11", "Get 15000 points");
		PlayerPrefs.SetString("achivmentInfo12", "Score 9 cleans in a row");
		PlayerPrefs.SetString("achivmentInfo13", "Get 16000 points before 12th round");
		PlayerPrefs.SetString("achivmentInfo14", "Get 20 clean shots before 3rd round");
		PlayerPrefs.SetString("achivmentInfo15", "Get to 5th round without clean shot");
		PlayerPrefs.SetString("achivmentInfo16", "Get 10 clean shots in a row");	
		PlayerPrefs.SetString("achivmentInfo17", "Get 18000 points");
		PlayerPrefs.SetString("achivmentInfo18", "Score 12 cleans in a row");
		PlayerPrefs.SetString("achivmentInfo19", "Get 15 shots in a row");
		PlayerPrefs.SetString("achivmentInfo20", "Get 20000 points");
		PlayerPrefs.SetString("achivmentInfo21", "Get to 20th round");
		
		PlayerPrefs.SetInt("sfx", 1);
		
		for(int i = 1; i <= 21; i++){
			PlayerPrefs.SetInt("achivment"+i, 0);
		}
		PlayerPrefs.Save();
	}
	
}
