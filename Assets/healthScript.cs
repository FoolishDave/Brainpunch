using UnityEngine;
using System.Collections;

public class healthScript : MonoBehaviour {
    int health;
    // Use this for initialization
    void Start () {


    }
	
   

	// Update is called once per frame
	void Update () {


        
	}

    public void takeDamage(float damage)
    {
        float currentHealth = (float)bodyProperties.health / 100;
        bodyProperties.health-= (int)damage;
        float dmgLength = damage / 100;
        
        
        transform.localScale = new Vector3(currentHealth-dmgLength, transform.localScale.y, transform.localScale.z);
    }
}
