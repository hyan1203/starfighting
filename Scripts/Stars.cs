using UnityEngine;
using System.Collections;

public class Stars : MonoBehaviour {

	// Use this for initialization
	public float speed;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float amtToMove = speed * Time.deltaTime;
		transform.Translate(Vector3.down * amtToMove,Space.World);
		if(transform.position.y < -10.75)
		{
			transform.position = new Vector3(transform.position.x , 14f, transform.position.z);
		}

	
	}
}
