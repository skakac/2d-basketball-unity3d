using UnityEngine;
using System.Collections;

public class loading_txt : MonoBehaviour {
	
	public GUISkin newSkin;
	string text;
	void Start(){
		switch(Random.Range(1, 5)){
			case 1: text = "Gathering the balls.."; break;
			case 2: text = "Making the game..";		break;
			case 3: text = "Jump through hoops..";	break;
			case 4: text = "Doing nothing..";		break;
			case 5: text = "Gathering crowd.."; 	break;			
		}
	
	}
	void OnGUI(){
		AutoResize(800, 480);	
		GUI.skin = newSkin;	
		GUI.skin.label.fontSize = 24;
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		GUI.Label(new Rect(230,200, 300, 80), text);
		
	}
	static void AutoResize(int screenWidth, int screenHeight)
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}
}
