using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float MinSpeed;
	public float MaxSpeed;
	public GameObject powerBuff;
	//public GameObject Invisible;
	private float currentSpeed; 
	private float x,y,z;

	private float MinRotateSpeed = 60f;
	private float MaxRotateSpeed = 120f;
	private float MinScale = 0.8f;
	private float MaxScale = 2f;
	private float currentRotationSpeed;
	private float currentScaleX;
	private float currentScaleY;
	private float currentScaleZ;
	
	//private float _OffsetX = 0.5f;

	void Start () 
	{
	      SetPositionAndSpeed();
	}
	
	// Update is called once per frame
	void Update () {
	     float amtToMove=currentSpeed*Time.deltaTime;
		 transform.Translate(Vector3.down*amtToMove,Space.World);
		 if(transform.position.y<-5)
		   {
		    SetPositionAndSpeed();
		    
		   }
		float rotationSpeed = currentRotationSpeed * Time.deltaTime;
		transform.Rotate(new Vector3(-1,0,0) * rotationSpeed);

	}

	public void SetPositionAndSpeed(){
		currentRotationSpeed = Random.Range(MinRotateSpeed,MaxRotateSpeed);
		currentScaleX = Random.Range(MinScale,MaxScale);
		currentScaleY = Random.Range(MinScale,MaxScale);
		currentScaleZ = Random.Range(MinScale,MaxScale);

	    currentSpeed = Random.Range(MinSpeed,MaxSpeed);
		x = Random.Range(-6f,6f);
		y = 7.0f;
		z = 0.0f;
		transform.position = new Vector3(x,y,z);

		transform.localScale = new Vector3(currentScaleX,currentScaleY,currentScaleZ);

	}
	
	void ApplyDamage(bool isBuff)
	{
		if(isBuff)
		{
			if(powerBuff!=null)
			{
				if(transform.position.y>4.4f)
					Instantiate(powerBuff,new Vector3(transform.position.x,4.4f,0f),Quaternion.identity);
				else
					Instantiate(powerBuff,transform.position,Quaternion.identity);
			}
		}
		if(MinSpeed<=7)
		{
			MinSpeed += 0.03f;
		}
		if(MaxSpeed<=9)
		{
		    MaxSpeed += 0.05f;
		}
		SetPositionAndSpeed();
	}
	
	/*void aInvisible()
	{
		Vector3 pos = new Vector3(transform.position.x+_OffsetX,transform.position.y);
		Instantiate(Invisible,pos,Quaternion.identity);
	}*/
	
}
