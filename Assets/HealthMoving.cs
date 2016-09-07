using UnityEngine;
using System.Collections;

public class HealthMoving : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void FixedUpdate()
    {
        transform.position = transform.parent.GetComponentInChildren<Rigidbody2D>().transform.position + new Vector3(0, 0.25f);
    }
}
