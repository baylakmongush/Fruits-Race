using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private float speed = 250.0f;
	[SerializeField]
	private float jumpForce = 5.0f;
	PhotonView photonView;
	Rigidbody2D rigidBody;
	protected PolygonCollider2D boxCollider;
	protected Vector2 velocity;
	protected bool valuesReceived = false;
	protected bool isGrounded = false;
	protected bool jumped = false;
	protected float horiz;
	protected Animator getAnimator;
	int score;
	Text scoreText;

	void Start()
	{
		DontDestroyOnLoad(this);
		getAnimator = GetComponent<Animator>();
		photonView = GetComponent<PhotonView>();
		rigidBody = GetComponent<Rigidbody2D>();
		boxCollider = GetComponent<PolygonCollider2D>();
		CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();
		scoreText = GameObject.FindWithTag("Score").GetComponent<Text>();
		score = PlayerPrefs.GetInt("score_temp");


		if (_cameraWork != null)
		{
			if (photonView.IsMine)
			{
				_cameraWork.OnStartFollowing();
			}
		}
		else
		{
			Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
		}
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.CompareTag("Ground"))
		{
			isGrounded = true;
		}

		if (collision.gameObject.CompareTag("Heart"))
		{
			score++;
			PlayerPrefs.SetInt("score_temp", score);
			scoreText.text = "Счёт: " + score;
		}

		if (collision.gameObject.tag == "Enemy")
			transform.position = Vector2.zero;

		if (collision.gameObject.tag == "Teleport")
		{
			PhotonNetwork.LoadLevel("Level2");
			transform.position = Vector2.zero;
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
		//DetectedHit();
		if (SpaceEnter() && isGrounded)
		{
			isGrounded = false;
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
		deltaX = horiz * speed * Time.fixedDeltaTime;
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

		if (!valuesReceived)
		{
			WalkPlayer();
			JumpPlayer();
		}
	}
}
