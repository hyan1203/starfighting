using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

	
	public Transform[] wayPoints;
	public float bossSpeed;
	public Transform projectilePrefab;
	public static float bossLife;
	private Transform _nextTarget;
	private Transform _myTransform;
	private float amtToMove;
	private Vector3 _moveVector;
	public static bool alive=false;
	public GameObject ExplosionbigPrefabBoss;
	public GameObject blood;
	
	
	void Start () 
	{
		 alive=true;
		 bossLife = 10000f;
		_myTransform = transform;
		wayPoints[0] = GameObject.Find("point1").transform;
		wayPoints[1] = GameObject.Find("point2").transform;
		wayPoints[2] = GameObject.Find("point3").transform;
		wayPoints[3] = GameObject.Find("point4").transform;
		wayPoints[4] = GameObject.Find("point5").transform;
		wayPoints[5] = GameObject.Find("point5").transform;
		wayPoints[6] = GameObject.Find("point5").transform;
		_nextTarget = wayPoints[Random.Range(0,5)];
		InvokeRepeating("CreateBullet",1f,0.8f);
	}
	
	void Update () 
	{
		if(Vector3.Distance(_myTransform.position,_nextTarget.position)<0.2f)
			_nextTarget = wayPoints[Random.Range(0,5)];
		amtToMove = bossSpeed * Time.deltaTime;
		_moveVector = _nextTarget.position - _myTransform.position;
		_myTransform.Translate(amtToMove * _moveVector,Space.World);
		
		
	}
	
	void ApplyDamage(bool isBuff)
	{
		bossLife -= 200f;
		if(bossLife<=0)
		{ 
			Vector3 pos=new Vector3(_myTransform.position.x,_myTransform.position.y);
			Instantiate(ExplosionbigPrefabBoss,_myTransform.position,Quaternion.identity);
			Destroy(gameObject);
			Instantiate(blood,pos,Quaternion.identity);
			alive=false;
			Player.Score += 20000;
			//Application.LoadLevel("Win");
		}
	}
	
	void SetPositionAndSpeed()
	{
		
	}
	
	void CreateBullet()
	{
		Instantiate(projectilePrefab,_myTransform.position,Quaternion.identity);
	}
}
