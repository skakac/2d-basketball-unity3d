using UnityEngine;
using System.Collections;

public class roundEnd : MonoBehaviour
{	
	public GUISkin newSkin;	
	int points, total, record, player;
	int[] mpoints;
	bool end = false, multi = false;
	
	
	public void Set(int lpoints, int ltotal, int lrecord, bool lend, bool lmulti, int[] lmpoints, int lplayer){
		points = lpoints;
		total = ltotal;
		record = lrecord;
		end = lend;
		multi = lmulti;
		mpoints = lmpoints;
		player = lplayer;			
	}
	
	void thePauseMenu()
	{			
			
		float centerx = 400;
		float centery = 240;
		
		//background
		gameObject.GetComponent<SpriteRenderer>().enabled = true;
		
		
		GUI.skin.label.alignment = TextAnchor.UpperCenter;
		GUI.skin.label.fontSize = 40;
		
		if(end)
			GUI.Label(new Rect(centerx -150, centery -25 -65, 300, 50), "Game Over");
		else 
			GUI.Label(new Rect(centerx -150, centery -25 -65, 300, 50), "Round Score");
			
		GUI.skin.label.alignment = TextAnchor.UpperLeft;
		GUI.skin.label.fontSize = 30;
		if(multi){
			GUI.Label(new Rect(centerx -150 -20, centery -25 -10, 300, 50), "Player 1: "+mpoints[0]);
			GUI.Label(new Rect(centerx -150 -20, centery -25 +30, 300, 50), "Player 2: "+mpoints[1]);
		}
		else{
			GUI.Label(new Rect(centerx -150 -20, centery -25 -10, 300, 50), "Round score: "+points);
			GUI.Label(new Rect(centerx -150 -20, centery -25 +30, 300, 50), "Total score: "+total);
		}
		
		if(!multi){
			GUI.Label(new Rect(centerx +150 +20, centery -25 -10, 300, 50), "Record score:");
			GUI.Label(new Rect(centerx +150 +20, centery -25 +30, 300, 50), ""+record);
		}
		else if(end && player == 1){
			string text = mpoints[0] > mpoints[1] ? "Player 1" : "Player 2";
			text = mpoints[0] == mpoints[1] ? "Tie..." : text;
			
			GUI.Label(new Rect(centerx +150 +20, centery -25 -10, 300, 50), "Winner: ");
			GUI.Label(new Rect(centerx +150 +20, centery -25 +30, 300, 50), ""+text);
		}
	
		GUI.skin.button.fontSize = 24;
		GUI.skin.button.alignment = TextAnchor.MiddleCenter;		
		if(GUI.Button(new Rect(centerx -170 -100, centery -30 +110, 200, 60), "Main menu") )
		{			
			gameObject.GetComponent<SpriteRenderer>().enabled = false;			
			this.enabled = false; 	
			AutoFade.LoadLevel(1,0.5f,0.5f,Color.black);			
				
		}	
		
		if(end && player == 1){	}
		
		else if(end){
			string text = multi && player == 0 ? "Next Player" : "Restart";
			if(GUI.Button(new Rect(centerx -170 +200, centery -30 +110, 200, 60), text) ){			
				gameObject.GetComponent<SpriteRenderer>().enabled = false;
				this.enabled = false; 	
				if(!multi)
					GameObject.Find("gameScript").GetComponent<gameScript>().Restart();
				else
					GameObject.Find("gameScript").GetComponent<gameScript>().MultiRestart(1);
			}	
		}
		else{
			if(GUI.Button(new Rect(centerx -170 +200, centery -30 +110, 180, 60), "Next Round") ){			
				gameObject.GetComponent<SpriteRenderer>().enabled = false;
				this.enabled = false; 	
				if(!multi)
					GameObject.Find("gameScript").GetComponent<gameScript>().NextRound();
				else
					GameObject.Find("gameScript").GetComponent<gameScript>().MultiNextRound();
			}
		}
		

		
		
	}
	
	
	void OnGUI ()		
	{			
		AutoResize(800, 480);	
		GUI.skin = newSkin;					
		Screen.showCursor = true;	
		thePauseMenu();		
	}	
		
	static void AutoResize(int screenWidth, int screenHeight)
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}
}