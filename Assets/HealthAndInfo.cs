using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthAndInfo : MonoBehaviour {

    public Transform redBar;
    public Transform greenBar;
    public Transform lockedBar;
    public int maxHealth = 100;
    public int cHealth = 100;
    public int health = 100;
    private AudioClip death;
    private GameObject deathPart;
    private EndScene gOver;

    public void Damage(int dam)
    {
        if (health > 0)
            health -= dam;
        float healthRatio = (float) health / maxHealth;
        
        greenBar.localScale = new Vector3(healthRatio, greenBar.localScale.y);
        

        if (health <= 0)
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(death);
            var deathSpawner = Instantiate(deathPart, transform.position, transform.rotation);

            if (tag == "Player")
                gOver.GameOver(false);

            if (tag == "King")
                gOver.GameOver(true);

            Destroy(deathSpawner,3f);
            Destroy(transform.parent.gameObject);
        }
    }

    void Update()
    {
        Color barColor = new Color();
        if (health > 100)
        {
            if (tag != "Player")
                ColorUtility.TryParseHtmlString("#37792EFF", out barColor);
            else
                barColor = Color.blue;
        } else
        {
            if (tag != "Player")
                barColor = Color.green;
            else
                ColorUtility.TryParseHtmlString("#7BE9FFFF", out barColor);
        }
        greenBar.GetComponent<Image>().color = barColor;
    }

	// Use this for initialization
	void Awake () {
        lockedBar = transform.parent.FindChild("Canvas").FindChild("Locked").transform;
        death = Resources.Load("Sounds/Male_Shout-of-Pain_132") as AudioClip;
        //death = Resources.Load("Sounds/(Praise) Fuck") as AudioClip;
        deathPart = Resources.Load<GameObject>("Death");
        gOver = Camera.main.GetComponent<EndScene>();
    }
	
    void Start ()
    {
        if (health > maxHealth)
            return;
        float lockedHealth = (float)(maxHealth - cHealth) / maxHealth;
        lockedBar.localScale = new Vector3(lockedHealth, lockedBar.localScale.y);
    }

}
