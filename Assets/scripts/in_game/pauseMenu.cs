using UnityEngine;
using System.Collections;

public class pauseMenu : MonoBehaviour
{	

	public GUISkin newSkin;	
	public GameObject background;
	public bool multi;
	
	string[] achvNames = new string[3], achvText;
	int[][] achv;
	
	
	public void LoadAchivments(){
		
		achv = new int[3][];
		for(int i = 0; i < 3; i++)
			achv[i] = new int[2];
			
		achvText = new string[3];
		
		multi = GameObject.Find("gameScript").GetComponent<gameScript>().multi;
		if(multi)
			return;
			
		achvNames = GameObject.Find("gameScript").GetComponent<gameScript>().achvNames;
		achv = GameObject.Find("gameScript").GetComponent<gameScript>().achv;
		for(int i = 0; i<3; i++)
			if(achv[i][1] == 1) achvText[i] = "// Done";
	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Time.timeScale = 1.0f;				
			this.enabled = false; 
			background.renderer.enabled = false;
		}
	
	}
	
	void thePauseMenu()
	{			
		float centerx = 400;
		float centery = 240;
		//background 		
		background.renderer.enabled = true;
		
		GUI.skin.label.alignment = TextAnchor.UpperCenter;
		GUI.skin.label.fontSize = 40;
		GUI.Label(new Rect(centerx -150, centery -25 -65, 300, 50), "Pause menu");
		
		
		GUI.skin.label.alignment = TextAnchor.UpperLeft;
		GUI.skin.label.fontSize = 28;
		if(!multi){
			GUI.Label(new Rect(centerx -200 -20, centery -30, 400, 50), "- "+achvNames[0]+" "+achvText[0]);
			GUI.Label(new Rect(centerx -200 -20, centery -30 +30, 400, 50), "- "+achvNames[1]+" "+achvText[1]);
			GUI.Label(new Rect(centerx -200 -20, centery -30 +30*2, 400, 50), "- "+achvNames[2]+" "+achvText[2]);
		}
		else GUI.Label(new Rect(centerx -200 -20, centery -30 +30, 400, 50), "- Player "+(GameObject.Find("gameScript").GetComponent<gameScript>().player+1));
		
		
		
		GUI.skin.button.alignment = TextAnchor.MiddleCenter;		
		if(GUI.Button(new Rect(centerx -150 -90, centery -20 +110, 180, 40), "Main menu") )
		{				
			Time.timeScale = 1.0f;		
			AutoFade.LoadLevel(1,0.5f,0.5f,Color.black);		
		}	
		
		if(GUI.Button(new Rect(centerx -150 +180, centery -20 +110, 180, 40), "Resume") )
		{					
			Time.timeScale = 1.0f;				
			this.enabled = false; 
			background.renderer.enabled = false;
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