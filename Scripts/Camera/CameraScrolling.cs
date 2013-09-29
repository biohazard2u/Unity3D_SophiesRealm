using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Version: 2.0</para>
/// <para>Date created: 14/11/2012</para>
/// <para>Last modified: 13/03/2013 17:40</para> 
/// <para>Author: Marcos Zalacain </para>
/// CameraScrolling:   
///    -This script has been fully modified on Version 2.0. Also, camera Animations have been added.
///    -It's now monitoring one target only and is good on right to left player rotations and vice versa. 
///    -ZoomOut functionality has been added!
/// </summary>
public class CameraScrolling : MonoBehaviour {

	// The object in our scene that our camera is currently tracking. Sophie only.
	public Transform target;
	// How far back should the camera be from the target?
	public float distance= 15.0f;
	// How strict should the camera follow the target?  Lower values make the camera more lazy.
	public float springiness= 4.0f;
	
	// Keep handy references to our level's attributes.  We set up these references in the Awake () function.
	// This also is very slightly more performant, but it's mostly just convenient.
	private LevelAttributes levelAttributes;
	private Rect levelBounds;
	
	// If there are no  target attributes attached, we have the following defaults.	
	// How high in world space should the camera look above the target?
	private float heightOffset = 0.0f;
	private float widthOffset = 4.0f;
	// How much should we zoom the camera based on this target?
	private float distanceModifier = 1.0f;
	// By default, we won't account for any target velocity in our calculations;
	private float velocityLookAhead = 0.0f;
	private Vector2 maxLookAhead = new Vector2(0.0f, 0.0f);
	
	// To retrieve camera attributes.
	private CameraTargetAttributes cameraTargetAttributes;
	// To modify widthOffSet when Sophie turns right or left.
	private int widthOffSetModifier;
	
	private bool doEndStageAnimation = false;
	private bool doTipsPrefab = false;
	
	void  Awake (){	
		// Set up our convenience references.
		levelAttributes = LevelAttributes.GetInstance ();
		levelBounds = levelAttributes.bounds;
		
		cameraTargetAttributes = target.GetComponent<CameraTargetAttributes>();		
		// If our target has special attributes, use these instead of our above defaults.
		if  (cameraTargetAttributes) {
			heightOffset = Globals.heightOffsetG;
			widthOffset = Globals.widthOffsetG;
			distanceModifier = cameraTargetAttributes.distanceModifier;
			velocityLookAhead = cameraTargetAttributes.velocityLookAhead;
			maxLookAhead = cameraTargetAttributes.maxLookAhead;
		}
		// By default, Sophie is looking right.
		widthOffSetModifier = 1;
	}
	
	// To modify widthOffSet when Sophie turns right or left, this method is set on Sophie controller scripts.
	public void SetwidthOffSetModifier(int num){
		if (num == 1) {
			widthOffSetModifier = 1;
		}else{
			widthOffSetModifier = -1;
		}
	}	
	
	// You almost always want camera motion to go inside of LateUpdate (), so that the camera follows
	// the target _after_ it has moved.  Otherwise, the camera may lag one frame behind.
	void LateUpdate (){
		// Where should our camera be looking right now?
		Vector3 goalPosition = GetGoalPosition ();
		
		// Interpolate between the current camera position and the goal position.
		transform.position = Vector3.Lerp (transform.position, goalPosition, Time.deltaTime * springiness);	
	}
	
	// Where the camera should move to.
	public Vector3 GetGoalPosition (){
		
		// First do a rough goalPosition that simply follows the target at a certain relative height and distance.
		Vector3 goalPosition = target.position + new Vector3(widthOffset * widthOffSetModifier, heightOffset, -distance * distanceModifier);
		
		// Next, we refine our goalPosition by taking into account our target's current velocity.
		// This will make the camera slightly look ahead to wherever the character is going.
		
		// First assume there is no velocity.
		// This is so if the camera's target is not a Rigidbody, it won't do any look-ahead calculations because everything will be zero.
		Vector3 targetVelocity = Vector3.zero;
		
		// If we find a Rigidbody on the target, that means we can access a velocity!
		Rigidbody targetRigidbody= target.GetComponent<Rigidbody>();
		if (targetRigidbody)
			targetVelocity = targetRigidbody.velocity;
		
		// If we find a PlatformerController on the target, we can access a velocity from that!
		//SophieControllerKeyboard targetPlatformerController = target.GetComponent<SophieControllerKeyboard>();
		SophieController targetPlatformerController = target.GetComponent<SophieController>();
		if (targetPlatformerController)
			targetVelocity = targetPlatformerController.GetVelocity();
		
		// If you've had a physics class, you may recall an equation similar to: position = velocity * time;
		// Here we estimate what the target's position will be in velocityLookAhead seconds.
		Vector3 lookAhead= targetVelocity * velocityLookAhead;
		
		// We clamp the lookAhead vector to some sane values so that the target doesn't go offscreen.
		// This calculation could be more advanced (lengthy), taking into account the target's viewport position,
		// but this works pretty well in practice.
		lookAhead.x = Mathf.Clamp (lookAhead.x, -maxLookAhead.x, maxLookAhead.x);
		lookAhead.y = Mathf.Clamp (lookAhead.y, -maxLookAhead.y, maxLookAhead.y);
		// We never want to take z velocity into account as this is 2D.  Just make sure it's zero.
		lookAhead.z = 0.0f;
		
		// Now add in our lookAhead calculation.  Our camera following is now a bit better!
		goalPosition += lookAhead;
		
		// To put the icing on the cake, we will make so the positions beyond the level boundaries
		// are never seen.  This gives your level a great contained feeling, with a definite beginning
		// and ending.
		
		Vector3 clampOffset= Vector3.zero;
		
		// Temporarily set the camera to the goal position so we can test positions for clamping.
		// But first, save the previous position.
		Vector3 cameraPositionSave= transform.position;
		transform.position = goalPosition;
		
		// Get the target position in viewport space.  Viewport space is relative to the camera.
		// The bottom left is (0,0) and the upper right is (1,1)
		// @TODO Viewport space changing in Unity 2.0f?
		Vector3 targetViewportPosition= camera.WorldToViewportPoint (target.position);
		
		// First clamp to the right and top.  After this we will clamp to the bottom and left, so it will override this
		// clamping if it needs to.  This only occurs if your level is really small so that the camera sees more than
		// the entire level at once.
		
		// What is the world position of the very upper right corner of the camera?
		Vector3 upperRightCameraInWorld = camera.ViewportToWorldPoint (new Vector3(1.0f, 1.0f, targetViewportPosition.z));
		
		// Find out how far outside the world the camera is right now.
		clampOffset.x = Mathf.Min (levelBounds.xMax - upperRightCameraInWorld.x, 0.0f);
		clampOffset.y = Mathf.Min ((levelBounds.yMax - upperRightCameraInWorld.y), 0.0f);
		
		// Now we apply our clamping to our goalPosition.  Now our camera won't go past the right and top boundaries of the level!
		goalPosition += clampOffset;
		
		// Now we do basically the same thing, except clamp to the lower left of the level.  This will override any previous clamping
		// if the level is really small.  That way you'll for sure never see past the lower-left of the level, but if the camera is
		// zoomed out too far for the level size, you will see past the right or top of the level.
		
		transform.position = goalPosition;
		Vector3 lowerLeftCameraInWorld= camera.ViewportToWorldPoint (new Vector3(0.0f, 0.0f, targetViewportPosition.z));
		
		// Find out how far outside the world the camera is right now.
		clampOffset.x = Mathf.Max ((levelBounds.xMin - lowerLeftCameraInWorld.x), 0.0f);
		clampOffset.y = Mathf.Max ((levelBounds.yMin - lowerLeftCameraInWorld.y), 0.0f);
		
		//Debug.Log("goalPosition1 = " + goalPosition);
		// Now we apply our clamping to our goalPosition once again.  Now our camera won't go past the left and bottom boundaries of the level!
//		goalPosition += clampOffset;
		/* Note to myself, I just comented the above line so the camera works due to clampOffset was launching my camera off 
		 * the right possition */
		
		// Now that we're done calling functions on the camera, we can set the position back to the saved position;
		transform.position = cameraPositionSave;
		
		// Send back our spiffily calculated goalPosition back to the caller!
		//Debug.Log("goalPosition2 = " + goalPosition);
		return goalPosition;
	}
	
	// This Update is for EndOfStage anima-simulations.
	void Update() {
		if (doEndStageAnimation) {
			distance = distance - Time.deltaTime * 1.2f;
			widthOffset =- Time.deltaTime * 2;
			transform.Rotate(Vector3.right * Time.deltaTime);
    	    transform.Rotate(Vector3.up * Time.deltaTime * 7, Space.World);	
		}
		if (doTipsPrefab) {
			distance = distance - Time.deltaTime * 1.2f;
			heightOffset =  1.2f;
			transform.Rotate(-Vector3.right * Time.deltaTime);
    	    transform.Rotate(Vector3.up * Time.deltaTime * -7, Space.World);	
		}
    }
	
	// Launch EndOfStage anima-simulations.		StartCoroutine(animationEndOfStage(10.0f));
	 public IEnumerator animationEndOfStage(float waitTime) {
		doEndStageAnimation = true;
		yield return new WaitForSeconds(waitTime);
		doEndStageAnimation = false;
	}
	
	// Launch TipsObjectL1 anima-simulations.		
	public IEnumerator animationTipsPrefab(float waitTime) { 
		doTipsPrefab = true;
		yield return new WaitForSeconds(waitTime);
		doTipsPrefab = false;
	}
	
	public void resetCametaValuesToDefault(){
		doTipsPrefab = false;
		distance = 15.0f;
		widthOffset = Globals.widthOffsetG;;
		heightOffset = Globals.heightOffsetG;
		transform.eulerAngles = new Vector3(0.3793328f, 357.6969f, -0.001935306f);
	}
	
	// V 2.0 - ZoomOut button
	public void zoomOutNow(){
		distance = 30.0f;
	}
	public void zoomNormalNow(){
		distance = 15.0f;
	}
}