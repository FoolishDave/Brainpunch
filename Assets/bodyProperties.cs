using UnityEngine;
using System.Collections;

public class bodyProperties : MonoBehaviour {
    public static int health;
    public Transform redBar;
    public Transform greenBar;
    healthScript healthBar;
    void onMouseDown(){
        healthBar.takeDamage(20f);
        Debug.Log("Hit logged");
        
    }

    // Use this for initialization
    void Start () {
        health = 100;
        healthBar = greenBar.GetComponent<healthScript>();
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
