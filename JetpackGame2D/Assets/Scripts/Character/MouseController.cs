using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;



public class MouseController : MonoBehaviour
{
    public Text coinsCollectedLabel;
    private uint coins = 0;
    public float forwardMovementSpeed = 3.0f;
    public float jetpackForce = 75f;
    private Rigidbody2D playerbody;
    private bool isDead = false;
    public TextManager textManager;
    public ParallaxController parallax;

    void CollectCoin(Collider2D coinCollider)
    {
        coins++;
        Destroy(coinCollider.gameObject);
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
        isDead = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerbody = GetComponent<Rigidbody2D>();
        textManager = FindObjectOfType<TextManager>();
    }

    // Update is called once per frame
    void Update()
    {
        bool jetpackActive = Input.GetButton("Fire1");
        if (jetpackActive)
        {
            playerbody.AddForce(new Vector2(0, jetpackForce));
        }

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
        parallax.offset = transform.position.x;
    }

    void UpdateGroundedStatus()
    {
        // TODO: Implement your logic for updating grounded status here
    }

    void AdjustJetpack(bool jetpackActive)
    {
        // TODO: Implement your logic for adjusting the jetpack here
    }
}