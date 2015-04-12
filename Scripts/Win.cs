using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour {

	// Use this for initialization

	public Texture backgroudTexture;
	void Start () {
		Player.Score = 0;
		Player.Lives = 3;
		Boss.alive=false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnGUI () 
	{
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),backgroudTexture);
		if(Input.anyKeyDown)
			Application.LoadLevel(1);
	}
}
