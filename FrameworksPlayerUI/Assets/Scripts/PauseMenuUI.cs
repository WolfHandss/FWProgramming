using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
	[SerializeField]
	private GameObject pauseMenuUI;

	[SerializeField]
	private bool isPaused;

	void Pause()
	{
		pauseMenuUI.SetActive(true);
	}

	void Unpause()
	{
		pauseMenuUI.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			isPaused = !isPaused;
		}

		if (isPaused)
		{
			Pause();
		}
		else
		{
			Unpause();
		}
    }
}
