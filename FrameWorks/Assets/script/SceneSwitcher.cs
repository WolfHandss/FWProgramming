using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    
	public void LoadStartScreen()
	{
		SceneManager.LoadScene(0);
	}

	public void LoadEndScreen()
	{
		SceneManager.LoadScene(2);
	}

	public void LoadGameScreen()
	{
		SceneManager.LoadScene(1);
	}

	// Update is called once per frame
	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			LoadStartScreen();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			LoadGameScreen();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			LoadEndScreen();
		}
	}
}
