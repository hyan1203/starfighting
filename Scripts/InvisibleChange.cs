using UnityEngine;
using System.Collections;

public class InvisibleChange : MonoBehaviour {
	 private float blinkRate = 0.1f;
     private int numberOfTimesToBlink = 20;
     private int blinkCount;
	// Use this for initialization
	void Start () {
	   
	  
	   StartCoroutine(DestroyShip());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void ApplyMessage()
	{
		
		Destroy(gameObject);
	}
	IEnumerator DestroyShip()
	{
		while(blinkCount<numberOfTimesToBlink)
			{
				gameObject.renderer.enabled = !gameObject.renderer.enabled;
				if(gameObject.renderer.enabled)
				{
					blinkCount ++;
				}
				yield return new WaitForSeconds(blinkRate);
			}
		blinkCount =0;
		Destroy(gameObject);
		
	}
}
