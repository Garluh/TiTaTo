﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public void NewGame()
	{
		SceneManager.LoadScene ("main", LoadSceneMode.Single);
	}

	public void AppExit()
	{
		Application.Quit ();
	}
}
