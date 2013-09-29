using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 1.0</para>
/// <para>Date created: 05/04/2013</para>
/// <para>Last modified: 09/04/2013 24:30</para> 
/// <para>Author: Marcos Zalacain </para>
/// WitchController:   
///    -This script is to manage all Maeva's animations and accions.
/// </summary>
public class WitchController : MonoBehaviour {
	
	public GameObject fireBallPrefab;		// Double Fireball 4 level 8/9?
	public GameObject smokePrefab;
	public int launchingForce = 60;
	
	private Vector3 fireBallLaunchingPosition1;		// We do 3 balls at three different highs.
	private Vector3 fireBallLaunchingPosition2;
	private Vector3 fireBallLaunchingPosition3;
	private float ballX, ballY, ballZ;

	void Start () {
		
		AnimationState launchinBall = animation["Spell_Cast_B"];
		launchinBall.wrapMode = WrapMode.Once;
		
		StartCoroutine(maevaInAction(1.5F));
		
		ballX = this.transform.position.x;
		ballY = GameObject.FindGameObjectWithTag("Player").transform.position.y + 1;
		ballZ = this.transform.position.z;
		
		fireBallLaunchingPosition1 = new Vector3(ballX, ballY, ballZ);
		fireBallLaunchingPosition2 = new Vector3(ballX, ballY + 9, ballZ);
		fireBallLaunchingPosition3 = new Vector3(ballX, ballY - 8, ballZ);
	}	
	
	void Update () {}
	
	// This function is to do Maeva actions after she has appeared on Scene.
	private IEnumerator maevaInAction(float waitingTime){
		
		// Wait to finish spining animation.
		yield return new WaitForSeconds(waitingTime);	
		// Then do shooting.
		yield return StartCoroutine( maevaShoots(1F) );
		// Finally go out.
		yield return StartCoroutine( maevaOut(2.0F) );
	}
		
	// This is to make Maeva shooting the fireball.
	private IEnumerator maevaShoots(float waitingTime){
		
		// Maevas shooting animation.
		this.transform.Rotate(0, 270, 0);
		animation.Blend("Spell_Cast_B");
		yield return new WaitForSeconds(waitingTime);
		// Instanciate fireBall prefab.
		GameObject fireBallClone1 = Instantiate(fireBallPrefab, fireBallLaunchingPosition1, Quaternion.identity) as GameObject;
		GameObject fireBallClone2 = Instantiate(fireBallPrefab, fireBallLaunchingPosition2, Quaternion.identity) as GameObject;
		GameObject fireBallClone3 = Instantiate(fireBallPrefab, fireBallLaunchingPosition3, Quaternion.identity) as GameObject;
		// Add forece to fireBall rigidbody.
		fireBallClone1.rigidbody.AddForce(launchingForce*Vector3.left);
		fireBallClone2.rigidbody.AddForce(launchingForce*Vector3.left);
		fireBallClone3.rigidbody.AddForce(launchingForce*Vector3.left);
	}
	
	// This is to remove Maeva from Scene.
	private IEnumerator maevaOut(float waitingTime){
		
		// Maevas spining animation.
		animation.Blend("Magic_Helix_Spell");
		// Instanciate smoke prefab.
		Instantiate(smokePrefab, this.transform.position, Quaternion.identity);
		yield return new WaitForSeconds(waitingTime);		
		// Destroy MaevasGameObject
		Destroy(gameObject);
	}
}
