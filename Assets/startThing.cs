using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class startThing : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }


	// Update is called once per frame
	void Update () {
	
	}
}
