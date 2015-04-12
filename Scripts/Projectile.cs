using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
     public float projectileSpeed;
	 private Transform myTransform;
	 public GameObject ExplosionPrefab;
	 public GameObject ExplosionPrefabBoss;
	// public GameObject Invisible;
	 private Enemy enemy;
	 public GameObject BossPrefab;
	 
	 private float amtToMoveVert;
	 //private float _OffsetX = 0.5f;
     
	// Use this for initialization
	void Start () {
		myTransform=transform;
	    enemy=(Enemy)GameObject.Find("Enemy").GetComponent("Enemy");
	
	}
	
	// Update is called once per frame
	void Update () {
		
		amtToMoveVert = projectileSpeed * Time.deltaTime;
		myTransform.Translate(Vector3.up*amtToMoveVert);
		if(myTransform.position.y>=6.3f||myTransform.position.x>=6.9||myTransform.position.x<=-6.3)
		{
		   Destroy(gameObject);
		   
		}
	 
	}
	void OnTriggerEnter(Collider oll)
	{
		if(oll.tag == "enemy")
		{
			
			
			if(Player.Score%5000==100&&Boss.alive==false)
				Instantiate(BossPrefab);
			
			if(string.Equals(oll.gameObject.name,"Enemy"))
			{
				Player.Score += 100;
				Instantiate(ExplosionPrefab,
					myTransform.position,
						Quaternion.identity);
					
			/*    if(Random.Range(0f,1f)<0.8f)
				{
					Vector3 pos = new Vector3(transform.position.x+_OffsetX,transform.position.y);
		            Instantiate(Invisible,pos,Quaternion.identity);
				}*/
			
			}
			else if(string.Equals(oll.gameObject.name,"Boss(Clone)"))
			{
				Instantiate(ExplosionPrefabBoss,
					myTransform.position,
						Quaternion.identity);
			}
			else
			{
				Destroy(gameObject);
				return;
			}

			if(Random.Range(0f,1f)<0.1f)
		         oll.gameObject.SendMessage("ApplyDamage",true);
			  //  oll.gameObject.SendMessage("AppearInvisible");
			
			else
				oll.gameObject.SendMessage("ApplyDamage",false);
			
		
			
			
			Destroy(gameObject);
		}
			
			if(Player.Score >=100000)
			  Application.LoadLevel(3);

			
		
	}
}
