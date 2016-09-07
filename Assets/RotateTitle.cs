using UnityEngine;
using System.Collections;

public class RotateTitle : MonoBehaviour {

    public Vector3 from = new Vector3(0f, 0f, 135f);
    public Vector3 to = new Vector3(0f, 0f, 225f);
    public float speed = 1.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.PingPong(Time.time * speed * 2.0f, 1.0f);
        transform.eulerAngles = Vector3.Lerp(from, to, t);

    }
}
