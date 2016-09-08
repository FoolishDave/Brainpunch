using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

    public int rays = 24;
    public float degrees = 106f;
    private ArrayList enemiesHit = new ArrayList();
    private bool rC = false;
    public float cooldown = 0.25f;
    public int damage = 5;
    private float countup;

    private AudioClip stab;
    private AudioClip whoosh;
    private AudioSource aSource;
    private ParticleSystem redP;

    private Animator anim;

    public Sprite player;
    public Sprite enemy;

	// Use this for initialization
	void Start () {
        countup = 0;
	}

    void Awake ()
    {
        anim = GetComponent<Animator>();
        aSource = GetComponent<AudioSource>();
        stab = Resources.Load("Sounds/Stab_Punch_Hit_136") as AudioClip;
        whoosh = Resources.Load("Sounds/Whoosh_Rod_Pole_022") as AudioClip;
        redP = transform.FindChild("Red Particle").GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        countup += Time.deltaTime;

        if (countup < cooldown)
            return;

        

        if (Input.GetAxisRaw("Swap") > 0)
            rC = true;

        if (Input.GetAxisRaw("Attack") <= 0 && !rC)
            return;

        countup = 0f;
        anim.Play("Punching");

        Vector2 rayPos = new Vector2(transform.position.x, transform.position.y);

        float degreesSide = degrees / 2;
        float degreeInc = degrees / rays;
        for (int i = 0; i < rays/2; i++)
        {
            Vector3 dir = Quaternion.AngleAxis(-1f * degreesSide + i * degreeInc, Vector3.forward)*transform.up;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 0.75f, 1 << 9);
            if (hit.collider != null)
            {
                Debug.DrawRay(rayPos, dir * 0.75f, Color.green);
                Debug.Log("hit " + hit.collider.tag);
                if (!enemiesHit.Contains(hit.collider.transform))
                    enemiesHit.Add(hit.collider.transform);
            }
            else //if (enemiesHit.Count==0)
            {
                Debug.DrawRay(rayPos, dir * 0.75f, Color.red);
            }
        }
        for (int i = 0; i < rays / 2; i++)
        {
            Vector3 dir = Quaternion.AngleAxis(degreesSide - i * degreeInc, Vector3.forward) * transform.up;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 0.75f, 1 << 9);
            if (hit.collider != null)
            {
                Debug.DrawRay(rayPos, dir * 0.75f, Color.green);
                Debug.Log("hit " + hit.collider.tag);

                Vector2 knockbackDir = hit.transform.position - transform.position;
                knockbackDir = knockbackDir.normalized;

                hit.transform.GetComponent<Rigidbody2D>().AddForce(knockbackDir*25, ForceMode2D.Impulse);
                //transform.GetComponent<Rigidbody2D>().AddForce(-knockback.normalized * 25, ForceMode2D.Impulse);
                if (!enemiesHit.Contains(hit.collider.transform))
                    enemiesHit.Add(hit.collider.transform);
            }
            else //if (enemiesHit.Count == 0)
            {
                Debug.DrawRay(rayPos, dir * 0.75f, Color.red);
            }
        }

        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, transform.up, 0.75f, 1 << 9);

        if (rayHit.collider != null)
        {
            Debug.DrawRay(rayPos, transform.up * 0.75f, Color.green);
            Debug.Log("hit " + rayHit.collider.tag);
            if (!enemiesHit.Contains(rayHit.collider.transform))
                enemiesHit.Add(rayHit.collider.transform);
        } else
        {
            Debug.DrawRay(rayPos, transform.up * 0.75f, Color.red);
        }

        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 5.0f));


        foreach (Transform t in enemiesHit)
        {
            if (rC)
            {
                t.GetComponent<HealthAndInfo>().Damage(damage + 10);
            }
            else
            {
                t.GetComponent<HealthAndInfo>().Damage(damage);
            }
        }


        float closestDistance = Mathf.Infinity;
        Transform closest = null;

        if (enemiesHit.Count > 0 && !rC)
        {
            aSource.pitch = Random.Range(0.75f, 1.25f);
            aSource.PlayOneShot(stab);
        } else if (rC && enemiesHit.Count>0)
        {
            aSource.PlayOneShot(whoosh);
        }

        if (rC && enemiesHit.Count > 0)
        {
            foreach (Transform t in enemiesHit)
            {
                float distance = Vector3.Distance(screenPos, t.position);
                if (distance < closestDistance)
                {
                    closest = t;
                    closestDistance = distance;
                }
            }
            if (closest.tag == "King")
            {
                Camera.main.GetComponent<EndScene>().GameOver(true);
            }

            Camera.main.GetComponent<Effects>().WooshTime();

            transform.tag = "Enemy";
            gameObject.layer = 9;
            GetComponent<Movement>().enabled = false;
            GetComponent<Attack>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = enemy;
            //transform.parent.GetComponentInChildren<Canvas>().enabled = true;
            GetComponent<EnemyMovement>().enabled = true;
            redP.Emit(500);

            transform.FindChild("greencircle").gameObject.SetActive(false);
   

            closest.tag = "Player";
            closest.gameObject.layer = 8;
            closest.GetComponent<Movement>().enabled = true;
            closest.GetComponent<Attack>().enabled = true;
            closest.GetComponent<SpriteRenderer>().sprite = player;
            //closest.parent.GetComponentInChildren<Canvas>().enabled = false;
            closest.GetComponent<EnemyMovement>().enabled = false;
            closest.FindChild("Green Particle").GetComponent<ParticleSystem>().Emit(500);

            closest.FindChild("greencircle").gameObject.SetActive(true);
            

            Camera.main.GetComponent<Follow>().target = closest;
        }


        enemiesHit.Clear();
        rC = false;
	}
}
