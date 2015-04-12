using UnityEngine;
using System.Collections;

public class BossBullet : MonoBehaviour {
    private float projectileSpeed = 4f;
	private Transform _myTransform;
	
	void Start()
	{
		_myTransform = transform;
	}
	
	void Update () 
	{
		float amtToMove = projectileSpeed * Time.deltaTime;
		_myTransform.Translate(amtToMove * Vector3.down);
		
		if(_myTransform.position.y<-4.25f)
			Destroy(gameObject);
	}
	
	void SetPositionAndSpeed()
	{
		Destroy(gameObject);
	}
}
