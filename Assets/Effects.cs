using UnityEngine;
using System.Collections;

public class Effects : MonoBehaviour {

    private bool wooshing = false;
    private bool unwooshing = false;
    public float rate = 1f;
    private Camera cam;

    public void WooshTime()
    {
        wooshing = true;
    }


    void Awake ()
    {
        cam = GetComponent<Camera>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate()
    {
        if (wooshing)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0.1f, rate * Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 35f, rate * Time.deltaTime);
        } else if (unwooshing)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1.0f, rate * Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60f, rate * Time.deltaTime);
        }

        if (Time.timeScale < 0.5f && wooshing)
        {
            wooshing = false;
            unwooshing = true;
        }
        if (Time.timeScale > 0.99f)
        {
            wooshing = false;
            unwooshing = false;
        }

    }
}
