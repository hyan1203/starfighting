using UnityEngine;
using System.Collections;

public class Blood : MonoBehaviour {
	public GameObject PrefabLight;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void ApplyMessage()
	{
		Player.Lives++;
		Instantiate(PrefabLight,
					transform.position,
						Quaternion.identity);
		Destroy(gameObject);
	}
	
}
