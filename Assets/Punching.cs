using UnityEngine;
using System.Collections;

public class Punching : MonoBehaviour {
    private bool toward = true;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void FixedUpdate() {
        if (transform.position.x > 0.1f)
        {
            toward = false;
        } else if (transform.position.x < -4.21)
        {
            toward = true;
        }
        if (!toward)
            transform.Translate(new Vector3(2f, 0, 0) * Time.deltaTime*-1f);
        else
            transform.Translate(new Vector3(2f, 0, 0) * Time.deltaTime);
    }
}
