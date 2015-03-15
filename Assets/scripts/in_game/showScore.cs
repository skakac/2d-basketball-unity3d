using UnityEngine;
using System.Collections;

public class showScore : MonoBehaviour {
	
	public int balls, round, ball, score, record, player, tempscore;
	public int[] mpoints = new int[2];
	public float cooldown = 0;
	public bool multi, achv;
	public GUISkin newSkin;
	public Texture ball_img;
	
	void Update(){
		if(achv && cooldown > 0) cooldown -= Time.deltaTime;
		else if(cooldown <= 0) achv = false;
	}
	
	// Use this for initialization
	void Start(){
		balls = ball = round = score = record = player = 0;
		multi = achv = false;
			
	}
	public void Set(int balls, int ball, int round, int score, int record, bool multi, int player, int[] mpoints){
		this.balls = balls;
		this.ball = ball;
		this.round = round;
		this.score = score;
		this.record = record;
		this.player = player+1;
		this.mpoints = mpoints;	
		this.multi = multi;
	}
	public void AchivmentDone(){achv = true; cooldown = 5f;}
	
	void OnGUI ()		
	{			
		AutoResize(800, 480);	
		GUI.skin = newSkin;	
		GUI.skin.box.alignment = TextAnchor.MiddleRight;
			
		GUI.Box(new Rect(10, 5, 60, 30), "x"+balls);		
		GUI.DrawTexture(new Rect(12.5f, 7.5f, 24f, 24f), ball_img, ScaleMode.ScaleToFit);
		
		GUI.skin.box.alignment = TextAnchor.MiddleLeft;
		if(multi) GUI.Box(new Rect(290-90-1 ,5, 90, 30), "Player "+player);		
		
		GUI.Box(new Rect(290 ,5, 90, 30), "Round:  "+round);		
		GUI.Box(new Rect(290+90+1 ,5, 90, 30), "Ball:  "+ball);		
		
		if(achv)
			GUI.Box(new Rect(10, 50, 140, 30), "Achivment get");	
			
		if(multi){
			tempscore = mpoints[player-1]+score;	
			GUI.Box(new Rect(510 ,5, 140, 30), "Score:  "+tempscore);
		}
		else
			GUI.Box(new Rect(510 ,5, 140, 30), "Score:  "+score);
		
		if(multi && player == 2)
			GUI.Box(new Rect(510+140+1 ,5, 140, 30), "Player1:  "+mpoints[0]);		
		else
			GUI.Box(new Rect(510+140+1 ,5, 140, 30), "Record:  "+record);
			
			
	}
	static void AutoResize(int screenWidth, int screenHeight)
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}
}