using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private float speed = 250.0f;
	[SerializeField]
	private float jumpForce = 1.0f;
	PhotonView photonView;
	Rigidbody2D rigidBody;
	protected BoxCollider2D myCollider;
	protected Vector2 velocity;
	protected bool valuesReceived = false;
	protected bool jumped = false;
	protected float horiz;
	protected Animator getAnimator;
	Text scoreText;
	Canvas canvas;
	private bool isGrounded = false;
	ExitGames.Client.Photon.Hashtable props;
	public static bool isTrue = false;
	int score;
	void Start()
	{
		props = new ExitGames.Client.Photon.Hashtable();
		if (PhotonNetwork.LocalPlayer.CustomProperties["score"] != null)
			props.Add("score", (int)PhotonNetwork.LocalPlayer.CustomProperties["score"]);
		else
			props.Add("score", 0);
		PhotonNetwork.LocalPlayer.SetCustomProperties(props);
		getAnimator = GetComponent<Animator>();
		photonView = GetComponent<PhotonView>();
		rigidBody = GetComponent<Rigidbody2D>();
		myCollider = GetComponent<BoxCollider2D>();
		CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();
		scoreText = GameObject.FindWithTag("Score").GetComponent<Text>();
		canvas = GameObject.FindWithTag("QuizCanvas").GetComponent<Canvas>();
		canvas.enabled = false;

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
	public void SendScore(int scoreReceived)
    {
		score = (int)PhotonNetwork.LocalPlayer.CustomProperties["score"];
		score += scoreReceived;
		scoreText.text = "Счёт: " + score.ToString();
		SetCustomProperties(score);
    }

	void SetCustomProperties(int score)
    {
		props["score"] = score;
		PhotonNetwork.LocalPlayer.SetCustomProperties(props);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
			isGrounded = true;
		if (collision.gameObject.CompareTag("Heart") && photonView.IsMine)
		{
			SendScore(1);
		}

		if (collision.gameObject.tag == "Enemy")
			transform.position = new Vector2(2, transform.position.y);

		if (collision.gameObject.tag == "Teleport")
		{
			if (photonView.IsMine)
			{
				gameObject.transform.position = new Vector3(0, -457, 0);
				Camera.main.transform.position = new Vector3(0, -457, -10);
			}
		}

		if (collision.gameObject.tag == "Quiz" && photonView.IsMine)
		{
			canvas.enabled = true;
			Destroy(collision.gameObject);
		}
	}
    bool SpaceEnter()
    {
		return ((Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space)));
    }

	void FindHit()
    {
		Vector3 max = myCollider.bounds.max;
		Vector3 min = myCollider.bounds.min;
		Vector2 corner1 = new Vector2(max.x, min.y - .1f);
		Vector2 corner2 = new Vector2(min.x, min.y - .2f);
		Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

		isGrounded = false;
		if (hit != null)
			isGrounded = true;
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
		if (SpaceEnter() && isGrounded)
		{
			rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
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
			if (canvas)
			{
				if (canvas.enabled == false && (PhotonNetwork.CurrentRoom.PlayerCount == 2 || GameObject.FindGameObjectsWithTag("MainCharacter").Length == 2))
				{
					FindHit();
					WalkPlayer();
					JumpPlayer();
				}
			}
		}
	}
}
