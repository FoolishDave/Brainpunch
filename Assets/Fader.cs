using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fader : MonoBehaviour {

    private GUITexture fadeImage;
    public float duration;
    public bool sceneStart = true;
    private float tFade = -1f;
    private CanvasGroup cGroup = null;
	// Use this for initialization
	void Awake () {
        fadeImage = GetComponent<GUITexture>();
        fadeImage.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
    }
	
    void Start ()
    {
       // doFade(255f);
    }

    void Update()
    {
        if (sceneStart || tFade != -1)
        {
            Debug.Log("Fading");
            if (sceneStart)
                doFade(0f);
            else
                if (cGroup != null)
                    doFade(tFade, cGroup);
                else
                    doFade(tFade);

            if ((sceneStart || tFade == 0.0f) && fadeImage.color.a <= 0.05f)
            {
                fadeImage.enabled = false;
                tFade = -1;
                sceneStart = false;
            } else if (Mathf.Abs(fadeImage.color.a-tFade)<0.05f)
            {
                tFade = -1;
            }
        } else if (cGroup != null)
        {
            cGroup = null;
        }
    }
    
	public void doFade(float alpha)
    {
        fadeImage.enabled = true;
        tFade = alpha;
        fadeImage.color = Color.Lerp(fadeImage.color,new Color(0, 0, 0, alpha),duration*Time.deltaTime);
    }

    public void doFade(float alpha, CanvasGroup group)
    {
        tFade = alpha;
        sceneStart = false;
        cGroup = group;
        doFade(alpha);
        cGroup.alpha = Mathf.Lerp(100, cGroup.alpha, duration/10 * Time.deltaTime);
    }
}
