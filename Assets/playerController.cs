using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	CharacterController cc;
	public float moveSpeed = 4f;

	float gravity = 0f;
	float jumpVelocity = 0;
	float jumpSpeed = 100f;
	public float jumpHeight = 16f;

	string state = "Movement";

	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (state == "Movement") {
			Movement ();
		}
		if (state == "Jump") {
			Jump ();
			Movement ();
		}
	}

	void Movement() {
		float x = Input.GetAxisRaw ("Horizontal");
		float z = Input.GetAxisRaw ("Vertical");

		Vector3 direction = new Vector3 (x, 0, z).normalized;
		Vector3 velocity = direction*moveSpeed*Time.deltaTime;

		if (cc.isGrounded) {
			gravity = 0;
		}
		else
		{
			gravity += 0.25f;
			gravity = Mathf.Clamp(gravity, 1f, 20f);
		}
		Vector3 gravityVector = -Vector3.up * gravity * Time.deltaTime;
		Vector3 jumpVector = Vector3.up * jumpSpeed * Time.deltaTime;

		cc.Move (velocity + gravityVector);

		if (velocity.magnitude > 0) {
			float yAngle = Mathf.Atan2 (direction.x, direction.z) * Mathf.Rad2Deg;

			transform.localEulerAngles = new Vector3 (0, yAngle, 0);
		}

		if (Input.GetKeyDown (KeyCode.Space) && cc.isGrounded) {
			jumpVelocity = jumpHeight;
			cc.Move (velocity + jumpVector);
			state = "Jump";
		}
}
	void Jump()
	{
		if (jumpVelocity < 0) { return; }
		jumpVelocity -= 1.2f;
	}
	//HI
}
