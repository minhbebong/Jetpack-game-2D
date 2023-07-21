using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class MouseController : MonoBehaviour
{
    public Transform groundCheckTransform;
    private bool isGrounded;
    public LayerMask groundCheckLayerMask;
    private Animator mouseAnimator;

    public ParticleSystem jetpack;

    public TextMeshProUGUI coinsCollectedLabel;

    private uint coins = 0;

    public float forwardMovementSpeed = 3.0f;
    public float jetpackForce = 75f;
    private Rigidbody2D playerbody;
    
    public TextManager textManager;
    public ParallaxController parallax;
    public Button restartButton;

    private bool isDead = false;
    public AudioClip coinCollectSound;
    public AudioSource jetpackAudio;
    public AudioSource footstepsAudio;
    public AudioClip explosion;
    public Button resButton;
    public GameObject explosionPrefab;




    public void RestartGame()
    {
        // D?ng �m thanh jetpack
        jetpackAudio.Pause();
        // D?ng �m thanh footsteps
        footstepsAudio.Pause();
        SceneManager.LoadScene("JetpackGame");
    }
    void CollectCoin(Collider2D coinCollider)
    {
        coins++;
        coinsCollectedLabel.text = coins.ToString();
        Destroy(coinCollider.gameObject);
        AudioSource.PlayClipAtPoint(coinCollectSound, transform.position);
    }



    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.CompareTag("Coins"))
        {
            CollectCoin(collider);
        }
        else if (collider.gameObject.CompareTag("missile"))
        {
            HitByMissile(collider);
        }
        else
            HitByLaser(collider);

    }

    void HitByLaser(Collider2D laserCollider)
    {
        if (!isDead)
        {
            laserCollider.gameObject.GetComponent<AudioSource>().Play();
        }
        isDead = true;
        mouseAnimator.SetBool("isDead", true);

        // D?ng �m thanh jetpack
        jetpackAudio.Pause();
        // D?ng �m thanh footsteps
        footstepsAudio.Pause();

    }
    void HitByMissile(Collider2D missileCollider)
    {
        if (!isDead)
        {
            AudioSource.PlayClipAtPoint(explosion, transform.position);

        }
        isDead = true;
        mouseAnimator.SetBool("isDead", true);

        jetpackAudio.Pause();
        
        footstepsAudio.Pause();
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            

        }
        
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
        parallax.offset = transform.position.x;
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
            resButton.gameObject.SetActive(true);
        }

        AdjustFootstepsAndJetpackSound(jetpackActive);
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

    void AdjustFootstepsAndJetpackSound(bool jetpackActive)
    {
        footstepsAudio.enabled = !isDead && !isGrounded;
        jetpackAudio.enabled = !isDead && !isGrounded;
        // N?u nh�n v?t ?� ch?t, d?ng �m thanh jetpack
        if (isDead)
        {
            jetpackAudio.Pause();
        }
        else
        {
            if (jetpackActive)
            {
                jetpackAudio.volume = 1.0f;
            }
            else
            {
                jetpackAudio.volume = 0.5f;
            }
        }

    }
}