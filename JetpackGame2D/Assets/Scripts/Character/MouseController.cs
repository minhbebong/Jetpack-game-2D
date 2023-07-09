using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class MouseController : MonoBehaviour
{

    public Transform groundCheckTransform;
    private bool isGrounded;
    public LayerMask groundCheckLayerMask;
    private Animator mouseAnimator;

    public ParticleSystem jetpack;

    public Text coinsCollectedLabel;

    private uint coins = 0;

    public float forwardMovementSpeed = 3.0f;
    public float jetpackForce = 75f;
    private Rigidbody2D playerbody;
    private bool isDead = false;
    public TextManager textManager;
    public ParallaxController parallax;
    public Button restartButton;

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
        //AudioSource.PlayClipAtPoint(coinCollectSound, transform.position);
    }

    /*void OnTriggerEnter2D(Collider2D collider)
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
        mouseAnimator.SetBool("dead", true);
    }*/

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.CompareTag("Coins"))
        {
            CollectCoin(collider);
        }
        else
        {
            HitByLaser(collider);
        }

    }

    void HitByLaser(Collider2D laserCollider)
    {
        isDead = true;
        mouseAnimator.SetBool("isDead", true);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerbody = GetComponent<Rigidbody2D>();
        textManager = FindObjectOfType<TextManager>();
        mouseAnimator = GetComponent<Animator>();
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
        
        mouseAnimator.SetBool("isGrounded", isGrounded);
    }

    void AdjustJetpack(bool jetpackActive)
    {
        var jetpackEmission = jetpack.emission;
        jetpackEmission.enabled = !isGrounded;
        if (jetpackActive)
        {
            jetpackEmission.rateOverTime = 300.0f;
        }
        else
        {
            jetpackEmission.rateOverTime = 75.0f;
        }
    }
}