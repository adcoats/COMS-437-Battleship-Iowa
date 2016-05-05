using UnityEngine;
using System.Collections;

public class EnemyBattleship : MonoBehaviour
{
    public GameObject deathExplosion;

    public float speed = 2.0f;
    public float maxTurnSpeed = 20.0f;
    public float followDist = 3.0f;

    public GameObject target;


	private AudioSource[] damageSoundSources;
	private float damageFlashTime;
	private SpriteRenderer renderer;
	private float damageTaken;

	// Use this for initialization
	void Start ()
    {
		damageSoundSources = GetComponents<AudioSource> ();
		renderer = GetComponent<SpriteRenderer> ();
		damageTaken = 0;
	}

    void Awake()
    {
        // default to targetting the battleship
        if (target == null)
            target = GameObject.Find("Battleship");
    }
	
	// Update is called once per frame
	void Update ()
	{
		damageTaken = Mathf.Clamp (damageTaken, 0, 5);
		renderer.color = Color.Lerp (Color.white, Color.red, damageTaken / 5);
		damageTaken -= 10 * Time.deltaTime;
	}

    void FixedUpdate()
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        Vector2 forward = body.transform.rotation * Vector2.up;

        Vector2 targetForward = target.transform.rotation * Vector2.up;
        Vector2 targetRight = target.transform.rotation * Vector2.right;
        
        Vector2 targetPos = target.transform.position;

        // choose which side to go towards
        int directionSwap = (Mathf.Abs(Util.VectorAngleWithSign(forward, targetForward)) < 90) ? 1 : -1;
        if (((Vector2) transform.position - (targetPos + targetRight)).magnitude <
            ((Vector2) transform.position - (targetPos - targetRight)).magnitude)
        {
            targetPos += targetRight * 2 * directionSwap;
        } else
        {
            targetPos -= targetRight * 2 * directionSwap;
        }

        // predict forward
        targetPos += targetForward * target.GetComponent<Rigidbody2D>().velocity.magnitude;

        Debug.DrawLine(targetPos, targetPos + targetForward);

        Vector2 targetDir = targetPos - (Vector2) transform.position;
        float dist = targetDir.magnitude;
        targetDir.Normalize();

        float targetAngle;
        if (dist >= followDist)
        {
            targetAngle = Util.VectorAngleWithSign(forward, targetDir);
            body.velocity = forward * speed / (1 + Mathf.Abs(body.angularVelocity / 180.0f));
        } else {
            targetAngle = target.GetComponent<Rigidbody2D>().rotation - body.rotation;
            print("in follow range, angle diff: " + targetAngle);
            body.velocity = forward * target.GetComponent<Rigidbody2D>().velocity.magnitude;
        }
        
        //print("Target angle: " + targetAngle);
        body.angularVelocity = Mathf.Clamp(targetAngle, -maxTurnSpeed, maxTurnSpeed);
    }

	void OnDamaged(float amount)
	{
		int i = Random.Range (0, 3);
		if (amount > 3) // random number
		{ 
			damageSoundSources [i].pitch = Random.Range (0.3f, 0.4f);
		} else 
		{
			damageSoundSources [i].pitch = Random.Range (0.6f, 0.7f);
		}
		damageSoundSources[i].PlayOneShot (damageSoundSources[i].clip);


		damageTaken += amount;
	}

    void OnDestroyed()
    {
        if (deathExplosion)
            Instantiate(deathExplosion, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
