using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	private string instructionText = "Instructions: \nPress Left and Right Arrow to move.\nPress Spacebar to fire.";
	private int buttonWidth =200;
	private int buttonHeight = 50;
	public Texture backgroudTexture;

	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI () 
	{
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),backgroudTexture);
		GUI.Label(new Rect(10,10,250,200),instructionText);	
		if(Input.anyKeyDown)
			Application.LoadLevel(1);


	}

}
