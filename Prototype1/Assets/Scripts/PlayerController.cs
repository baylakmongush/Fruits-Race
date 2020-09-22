using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private float speed = 250.0f;
	[SerializeField]
	private float jumpForce = 5.0f;
	PhotonView photonView;
	Rigidbody2D rigidBody;
	protected BoxCollider2D boxCollider;
	protected Vector2 velocity;
	protected bool valuesReceived = false;
	protected bool isGrounded = false;
	protected bool jumped = false;
	protected float horiz;
	protected Animator getAnimator;

	void Start()
	{
		getAnimator = GetComponent<Animator>();
		LoadAnim();
		photonView = GetComponent<PhotonView>();
		rigidBody = GetComponent<Rigidbody2D>();
		boxCollider = GetComponent<BoxCollider2D>();
	}

	public virtual void LoadAnim() { ;}

	protected void DetectedHit()
	{
		Vector3 min = boxCollider.bounds.min;
		Vector3 max = boxCollider.bounds.max;
		Vector2 pointA = new Vector2(max.x, min.y - 0.1f);
		Vector2 pointB = new Vector2(min.x, min.y - 0.2f);
		Collider2D hit = Physics2D.OverlapArea(pointA, pointB);
		isGrounded = false;
		if (hit != null)
		{
			isGrounded = true;
		}
	}

	bool SpaceEnter()
    {
		return ((Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space)));
    }

	void AnimationController()
	{
		if (horiz > 0 && !jumped)
		{
			getAnimator.SetBool("walkRight", true);
			getAnimator.SetBool("walkLeft", false);
		}
		else if (horiz < 0 && !jumped)
		{
			getAnimator.SetBool("walkRight", false);
			getAnimator.SetBool("walkLeft", true);
		}
		else if (horiz == 0)
		{
			getAnimator.SetBool("walkRight", false);
			getAnimator.SetBool("walkLeft", false);
		}
		if (horiz > 0 && jumped)
		{
			getAnimator.SetBool("jumpRight", true);
			getAnimator.SetBool("jumpLeft", false);
		}
		else if (horiz < 0 && jumped)
		{
			getAnimator.SetBool("jumpRight", false);
			getAnimator.SetBool("jumpLeft", true);
		}
		else if (horiz == 0 && jumped)
		{
			getAnimator.SetBool("jumpRight", true);
		}
	}
	void JumpPlayer()
	{
		DetectedHit();
		if (SpaceEnter() && isGrounded)
		{
			rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			jumped = true;
			getAnimator.SetBool("walkRight", false);
			getAnimator.SetBool("walkLeft", false);
		}
		else if (isGrounded)
		{
			getAnimator.SetBool("jumpRight", false);
			getAnimator.SetBool("jumpLeft", false);
			jumped = false;
		}
	}

	void WalkPlayer()
	{
		float deltaX;
		horiz = Input.GetAxis("Horizontal");
		deltaX = horiz * speed * Time.deltaTime;
		velocity = new Vector2(deltaX, rigidBody.velocity.y);
		rigidBody.velocity = velocity;
		AnimationController();
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			stream.SendNext(rigidBody.velocity);
		}
		else
		{
			velocity = (Vector3)stream.ReceiveNext();

			valuesReceived = true;
		}
	}
	void FixedUpdate()
	{
		if (!photonView.IsMine) return;

		Debug.Log(getAnimator.gameObject.activeSelf);

		if (!valuesReceived)
		{
			WalkPlayer();
			JumpPlayer();
		}
	}
}
