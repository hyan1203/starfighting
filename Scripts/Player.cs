using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	private string instructionText = "Instructions: \n for win,get 10,0000";
	// Use this for initialization
	public GameObject ProjectilePrefab;
	public GameObject ProjectilePrefab_left;
	public GameObject ProjectilePrefab_right;
	public GameObject ExplosionPrefab;
	
	//public GameObject BossPrefab;
	private float projectileOffset=1.2f;
	public float PlayerSpeed;
	public static int Score = 0;
	public static int Lives = 3;
	
	public static Player Instance;

	private float shipInvisibleTime = 1.5f;
	private float shipMoveOnToScreenSpeed = 5f;
	private float blinkRate = 0.1f;
	private int numberOfTimesToBlink = 10;
	private int blinkCount=0;
	private int numberOfTimesToBlink2 = 15;
	
	private int _powerLevel=1;
	private float _projectileOffset = 1.2f;
	private float _projectileOffsetX = 0.2f;
	private float amtToMove;
	private float amtToMoveVert;
	private float x,y,z;

	enum State
	{
		Playing,
		Explosion,
		Invincible
	}
	private State state = State.Playing;


	// Update is called once per frame
	
				
	void Update () 
	{
		if(state!=State.Explosion)
		{
			BoundCheck();
			
			if(Input.GetKeyDown("space"))
			{
				CreateBullet();
			}
		}
    }
	
	void BoundCheck()
	{
		amtToMove = Input.GetAxisRaw("Horizontal") * PlayerSpeed * Time.deltaTime;
		amtToMoveVert = Input.GetAxisRaw("Vertical") * PlayerSpeed * Time.deltaTime;
		if(amtToMoveVert>0)
		{
			if(transform.position.y>4.9f)
				amtToMoveVert = 0;
		}
		else	
		{
			if(transform.position.y<-3.15f)
				amtToMoveVert = 0;
		}
	    transform.Translate(Vector3.right * amtToMove + Vector3.up * amtToMoveVert);
		
		if(transform.position.x<= -7.4f)
			transform.position = new Vector3(7.3f,transform.position.y,transform.position.z);
		else if(transform.position.x>=7.3f)
			transform.position = new Vector3(-7.4f,transform.position.y,transform.position.z);
	}
	void OnGUI()
	{
		GUI.Label(new Rect(10,10,120,20),"Score: "+Player.Score.ToString());
		GUI.Label(new Rect(10,30,60,20),"Lives: "+Player.Lives.ToString());
		GUI.Label(new Rect(10,50,250,200),instructionText);	
		

	}
	void OnTriggerEnter(Collider otherObject)
	{
		if(otherObject.tag == "enemy"&& state == State.Playing)
		{
			if(string.Equals(otherObject.gameObject.name,"Enemy"))
			{
			Player.Lives --;
			Enemy enemy = (Enemy)otherObject.gameObject.GetComponent("Enemy");
			enemy.SetPositionAndSpeed();
			StartCoroutine(DestroyShip());
			}
			else if(string.Equals(otherObject.gameObject.name,"Boss(Clone)"))
			{
			Player.Lives --;
			Boss.bossLife--;
			StartCoroutine(DestroyShip());	
			}
			
		}
		if(otherObject.tag == "bossBullet"&& state == State.Playing)
		{
			Player.Lives --;
		    Destroy(otherObject.gameObject);
			StartCoroutine(DestroyShip());
		
		}
		if(otherObject.tag == "powerBuff" && state != State.Explosion)
		{
			_powerLevel ++;
			otherObject.gameObject.SendMessage("ApplyMessage",null);
			if(_powerLevel>5)
				_powerLevel = 5;
		}
		/*if(otherObject.tag == "invisible" && state != State.Explosion)
		{
			StartCoroutine(Invisible());
			blinkCount=0;
			otherObject.gameObject.SendMessage("ApplyMessage",null);
				
			
		}*/
		if(otherObject.tag == "blood" && state != State.Explosion)
		{
			otherObject.gameObject.SendMessage("ApplyMessage",null);
		}
	}
	IEnumerator DestroyShip()
	{
		_powerLevel=1;
        state = State.Explosion;
		Instantiate(ExplosionPrefab,transform.position,Quaternion.identity);
		gameObject.renderer.enabled = false;
		transform.position = new Vector3(0f,-5.5f,transform.position.z);
		yield return new WaitForSeconds(shipInvisibleTime);
		if(Player.Lives > 0)
		{
			gameObject.renderer.enabled = true;
		}
		else
		{
			
			Application.LoadLevel(2);
			
		}

		gameObject.renderer.enabled = true;
		while(transform.position.y < -3.2)
         {
			state = State.Invincible;
            //Move the Ship Up
            float amtToMove = shipMoveOnToScreenSpeed * Time.deltaTime;
            transform.position = new Vector3(0f,transform.position.y + amtToMove,transform.position.z);
            yield return 0;
          }
		while(blinkCount<numberOfTimesToBlink)
		{
			gameObject.renderer.enabled = !gameObject.renderer.enabled;
			if(gameObject.renderer.enabled == true)
				blinkCount++;
			yield return new WaitForSeconds(blinkRate);
		}
		blinkCount = 0;
		state = State.Playing;

		

	}
	void CreateBullet()
	{
		switch(_powerLevel)
		{
			case 1:
				Vector3 pos = new Vector3(transform.position.x,transform.position.y+_projectileOffset);
				Instantiate(ProjectilePrefab,pos,Quaternion.identity);
			break;
			case 2:
				Vector3 pos1 = new Vector3(transform.position.x+_projectileOffsetX,transform.position.y+_projectileOffset);
				Vector3 pos2 = new Vector3(transform.position.x-_projectileOffsetX,transform.position.y+_projectileOffset);
				Instantiate(ProjectilePrefab,pos1,Quaternion.identity);
				Instantiate(ProjectilePrefab,pos2,Quaternion.identity);
			break;
			case 3:
				Vector3 pos3 = new Vector3(transform.position.x,transform.position.y+_projectileOffset);
				Vector3 pos4 = new Vector3(transform.position.x-_projectileOffsetX,transform.position.y+_projectileOffset);
			    Vector3 pos5 = new Vector3(transform.position.x+_projectileOffsetX,transform.position.y+_projectileOffset);
			    
				Instantiate(ProjectilePrefab,pos3,Quaternion.identity);
			    
				Instantiate(ProjectilePrefab_left,pos4,Quaternion.identity);
			    
			    Instantiate(ProjectilePrefab_right,pos5,Quaternion.identity);
			break;
		    case 4:
			    Vector3 pos6 = new Vector3(transform.position.x+(2*_projectileOffsetX),transform.position.y+_projectileOffset);
				Vector3 pos7 = new Vector3(transform.position.x-(2*_projectileOffsetX),transform.position.y+_projectileOffset);
			    Vector3 pos8 = new Vector3(transform.position.x+(4*_projectileOffsetX),transform.position.y+_projectileOffset);
			    Vector3 pos9 = new Vector3(transform.position.x-(4*_projectileOffsetX),transform.position.y+_projectileOffset);
				Instantiate(ProjectilePrefab,pos6,Quaternion.identity);
			    Instantiate(ProjectilePrefab,pos7,Quaternion.identity);
				Instantiate(ProjectilePrefab_left,pos9,Quaternion.identity);
			    
			    Instantiate(ProjectilePrefab_right,pos8,Quaternion.identity);
			break;
			 case 5:
			    Vector3 pos10 = new Vector3(transform.position.x,transform.position.y+_projectileOffset);
				Vector3 pos11 = new Vector3(transform.position.x+(3*_projectileOffsetX),transform.position.y+_projectileOffset);
			    Vector3 pos12 = new Vector3(transform.position.x+(8*_projectileOffsetX),transform.position.y+_projectileOffset);
			    Vector3 pos13 = new Vector3(transform.position.x-(3*_projectileOffsetX),transform.position.y+_projectileOffset);
			    Vector3 pos14 = new Vector3(transform.position.x-(8*_projectileOffsetX),transform.position.y+_projectileOffset);
				Instantiate(ProjectilePrefab,pos10,Quaternion.identity);
			    Instantiate(ProjectilePrefab_right,pos11,Quaternion.identity);
			    Instantiate(ProjectilePrefab_right,pos12,Quaternion.identity);
				Instantiate(ProjectilePrefab_left,pos13,Quaternion.identity);
			    
			    Instantiate(ProjectilePrefab_left,pos14,Quaternion.identity);
			break;
		}
		
	}
/*public void ShowBoss()
	{
		x = Random.Range(-6f,6f);
		y = 5.0f;
		z = 0.0f;
		Vector3 pos  = new Vector3(x,y,z);
		Instantiate(BossPrefab,pos,Quaternion.identity);
	}
	*/

	/*IEnumerator Invisible()
	{
		state = State.Invincible;
		while(blinkCount<numberOfTimesToBlink2)
		{
			gameObject.renderer.enabled = !gameObject.renderer.enabled;
			if(gameObject.renderer.enabled == true)
				blinkCount++;
			yield return new WaitForSeconds(blinkRate);
		}
		    blinkCount = 0;
			state = State.Playing;
			
	}
	 */


}
