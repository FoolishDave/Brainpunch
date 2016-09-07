using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{

    public Transform target;
    public float speed = 3f;
    private Animator anim;
    public float maxCooldown = 5f;
    private float curTime = 5f;
    public float observance = 1.72f;

    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        anim.SetBool("Walking", false);
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        curTime += Time.deltaTime;

        //if (target == null)
          //  return;

        if (target == null || target.tag != "Player")
        {
            var player = GameObject.FindWithTag("Player");
            if (player == null)
                return;

            target = player.transform;
        }

        if (Vector3.Distance(transform.position, target.position) < observance)
        {
            var dir = target.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            
            if (Vector3.Distance(transform.position, target.position) > .5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                anim.SetBool("Walking", true);
            }
            else
            {
                anim.SetBool("Walking", false);
                if (curTime >= maxCooldown)
                {
                    anim.Play("Punching");
                    target.GetComponent<HealthAndInfo>().Damage(10);
                    curTime = 0f;
                    //Debug.Log("anim over");
                }
            }
        }
    }
}
