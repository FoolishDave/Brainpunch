using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour {

    private Fader fadeScript;
    public CanvasGroup lose;

	// Use this for initialization
	void Start () {
        
	}
	
    void Awake ()
    {
        fadeScript = GameObject.Find("Fader").GetComponent<Fader>();
       // lose = GameObject.Find("LoseScreen").GetComponent<CanvasGroup>();
    }

    public void GameOver(bool win)
    {
        lose.gameObject.SetActive(true);

        if (win)
        {
            lose.transform.FindChild("LoseOrWin").GetComponent<Text>().text = "Fine. You won.";
            lose.transform.FindChild("NextLevel").GetComponent<Button>().interactable = true;
        }

        fadeScript.doFade(0.25f, lose);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
            SceneManager.LoadScene(0);

        Debug.Log("Loading scene: " + SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
