using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 09/04/2013</para>
/// <para>Last modified: 09/04/2013 18:30</para> 
/// <para>Author: Marcos Zalacain </para>
/// FireBallController:   
///    -This easy script is to manages its sphere collider and send messages to LifeManager.cs
///    -It also self destroys FireBall after x seconds. 
/// </summary>
public class FireBallController : MonoBehaviour {
	
	public float timeToSelfDestroy = 5F;
	
	void Update () {
		
		timeToSelfDestroy -= Time.deltaTime;
		
		if (timeToSelfDestroy <= 0) {
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter(Collider col){
		
		if(col.gameObject.tag == "Player"){
			
			col.gameObject.SendMessage("LifeDown", SendMessageOptions.DontRequireReceiver);	
			Destroy(gameObject);
		}
	}
}
