using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class MouseController : MonoBehaviour
{
    public Transform groundCheckTransform;
    public LayerMask groundCheckLayerMask;

    public Text coinsCollectedLabel;

    private int coins = 0;

    public float forwardMovementSpeed = 3.0f;
    public float jetpackForce = 75f;
    private Rigidbody2D playerbody;
    private bool isDead = false;
    public TextManager textManager;
    public ParallaxController parallax;
    Animator animator;
    public Button restartButton;
    private bool isGrounded;

    public AudioClip coinCollectSound;
    public AudioSource jetpackAudio;
    public AudioSource footstepsAudio;

    public void RestartGame()
    {
        SceneManager.LoadScene("RocketMouse");
    }
    void CollectCoin(Collider2D coinCollider)
    {
        coins++;
        Destroy(coinCollider.gameObject);
        AudioSource.PlayClipAtPoint(coinCollectSound, transform.position);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //HitByLaser(collider);
        if (collider.gameObject.CompareTag("Coins"))
        {
            textManager.IncreaseScore();
            CollectCoin(collider);
        }
        else
        {
            HitByLaser(collider);
        }

    }

    void HitByLaser(Collider2D laserCollider)
    {
        if (!isDead)
        {
            laserCollider.gameObject.GetComponent<AudioSource>().Play();
        }

        isDead = true;
        animator.SetBool("dead", true);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerbody = GetComponent<Rigidbody2D>();
        textManager = FindObjectOfType<TextManager>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //Vector3 pos = transform.position;
        //pos.x += forwardMovementSpeed * Time.deltaTime;
        //transform.position = pos;
        bool jetpackActive = Input.GetButton("Fire1");
        jetpackActive = jetpackActive && !isDead;

        if (jetpackActive)
        {
            playerbody.AddForce(new Vector2(0, jetpackForce));
        }

        if (!isDead)
        {
            Vector2 newVelocity = playerbody.velocity;
            newVelocity.x = forwardMovementSpeed;
            playerbody.velocity = newVelocity;
        }

        UpdateGroundedStatus();
        AdjustJetpack(jetpackActive);
        if (isDead && isGrounded)
        {
            restartButton.gameObject.SetActive(true);
        }
        parallax.offset = transform.position.x;
    }

    void UpdateGroundedStatus()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckLayerMask);
        animator.SetBool("grounded", isGrounded);
    }

    void AdjustJetpack(bool jetpackActive)
    {
        footstepsAudio.enabled = !isDead && isGrounded;

        jetpackAudio.enabled = !isDead && !isGrounded;
        jetpackAudio.volume = jetpackActive ? 1.0f : 0.5f;
    }
}