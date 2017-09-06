using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Player
{
	public Image panel;
	public Text text;
	public Button button;
}

[System.Serializable]
public class PlayerColor
{
	public Color panelColor;
	public Color textColor;
}

public class GameController : MonoBehaviour {

	public Text[] buttonList;

	public GameObject gameOverPanel;
	public Text gameOverText;

	public GameObject restartButton;

	public Player playerX;
	public Player playerO;
	public PlayerColor activePlayerColor;
	public PlayerColor inactivePlayerColor;
	public GameObject startInfo;

	private string playerSide;
	private int moveCnt;
	private string computerSide;

	public bool playerMove;
	public float delay;

	private int value;

	public Text scoreWinX;
	public Text scoreWinO;
	public Text scoreDrow;

	private int winX = 0;
	private int winO = 0;
	private int drow = 0;



	void Awake()
	{
		gameOverPanel.SetActive (false);
		SetGameControllerRefOnButtons ();
		//playerSide = "X";
		moveCnt = 0;
		restartButton.SetActive (false);
		playerMove = true;
		//SetPlayersColors (playerX, playerO);
	}

	void Update()
	{
		if (playerMove == false) 
		{
			delay += delay * Time.deltaTime;
			if (delay >=100) 
			{
				value = Random.Range (0, 8);
				if (buttonList[value].GetComponentInParent<Button>().interactable == true) 
				{
					buttonList [value].text = GetComputerSide ();
					buttonList [value].GetComponentInParent<Button> ().interactable = false;
					EndTurn ();
				}
			}
		}
	}

	void SetGameControllerRefOnButtons()
	{
		for (int i = 0; i < buttonList.Length; i++) 
		{
			buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerRef(this);
		}
	}

	public void SetStartigSide(string startingSide)
	{
		playerSide = startingSide;
		if (playerSide == "X") 
		{
			computerSide = "O";
			SetPlayersColors (playerX, playerO);
		} 
		else 
		{
			computerSide = "X";
			SetPlayersColors (playerO, playerX);
		}
		StartGame ();
	}

	void StartGame()
	{
		SetBoardInteractable (true);
		SetPlayerButton (false);
		startInfo.SetActive (false);
	}

	public string GetPlayerSide()
	{
		return playerSide;
	}

	public string GetComputerSide()
	{
		return computerSide;
	}

	public void EndTurn()
	{
		moveCnt++;
		if (buttonList [0].text == playerSide && buttonList [1].text == playerSide && buttonList [2].text == playerSide) {
			GameOver (playerSide);
		} else if (buttonList [3].text == playerSide && buttonList [4].text == playerSide && buttonList [5].text == playerSide) {
			GameOver (playerSide);
		} else if (buttonList [6].text == playerSide && buttonList [7].text == playerSide && buttonList [8].text == playerSide) {
			GameOver (playerSide);
		} else if (buttonList [0].text == playerSide && buttonList [3].text == playerSide && buttonList [6].text == playerSide) {
			GameOver (playerSide);
		} else if (buttonList [1].text == playerSide && buttonList [4].text == playerSide && buttonList [5].text == playerSide) {
			GameOver (playerSide);
		} else if (buttonList [2].text == playerSide && buttonList [5].text == playerSide && buttonList [8].text == playerSide) {
			GameOver (playerSide);
		} else if (buttonList [0].text == playerSide && buttonList [4].text == playerSide && buttonList [8].text == playerSide) {
			GameOver (playerSide);
		} else if (buttonList [2].text == playerSide && buttonList [4].text == playerSide && buttonList [6].text == playerSide) {
			GameOver (playerSide);
		} else if (buttonList [0].text == computerSide && buttonList [1].text == computerSide && buttonList [2].text == computerSide) {
			GameOver (computerSide);
		} else if (buttonList [3].text == computerSide && buttonList [4].text == computerSide && buttonList [5].text == computerSide) {
			GameOver (computerSide);
		} else if (buttonList [6].text == computerSide && buttonList [7].text == computerSide && buttonList [8].text == computerSide) {
			GameOver (computerSide);
		} else if (buttonList [0].text == computerSide && buttonList [3].text == computerSide && buttonList [6].text == computerSide) {
			GameOver (computerSide);
		} else if (buttonList [1].text == computerSide && buttonList [4].text == computerSide && buttonList [5].text == computerSide) {
			GameOver (computerSide);
		} else if (buttonList [2].text == computerSide && buttonList [5].text == computerSide && buttonList [8].text == computerSide) {
			GameOver (computerSide);
		} else if (buttonList [0].text == computerSide && buttonList [4].text == computerSide && buttonList [8].text == computerSide) {
			GameOver (computerSide);
		} else if (buttonList [2].text == computerSide && buttonList [4].text == computerSide && buttonList [6].text == computerSide) {
			GameOver (computerSide);
		} else if (moveCnt > 9) {
			GameOver ("draw");
		}
		else {
			ChangeSide ();
		}


	}

	void SetPlayersColors(Player newPlayer, Player oldPlayer)
	{
		newPlayer.panel.color = activePlayerColor.panelColor;
		newPlayer.text.color = activePlayerColor.textColor;
		oldPlayer.panel.color = inactivePlayerColor.panelColor;
		oldPlayer.text.color = inactivePlayerColor.textColor;
	}

	void GameOver(string winningPlayer)
	{
		SetBoardInteractable (false);

		if (winningPlayer == "draw") 
		{
			drow++;
			GameOverPanel ("И такое бывает!");
			SetPlayerColorsInactiv ();
		}
		else 
		{
			if (winningPlayer == "X") 
			{
				winX++;
			}
			else 
			{
				winO++;
			}
			GameOverPanel (winningPlayer + " выиграл!!");
		}
		SetScore (winX, winO, drow);
		restartButton.SetActive (true);
	}

	void ChangeSide()
	{
		//playerSide = (playerSide == "X") ? "O" : "X";
		playerMove = (playerMove == true) ? false : true;
		//if (playerSide == "X") 
		if(playerMove == true)
		{
			//computerSide = "O";
			SetPlayersColors (playerX, playerO);
		} else {
			//computerSide = "X";
			SetPlayersColors (playerO, playerX);
		}
	}

	/// <summary>
	/// Games the over panel.
	/// </summary>
	/// <param name="msg">Message.</param>
	void GameOverPanel(string msg)
	{
		gameOverPanel.SetActive (true);
		gameOverText.text = msg;
	}

	public void RestartGame()
	{
		moveCnt = 0;
		gameOverPanel.SetActive (false);
		restartButton.SetActive (false);
		SetBoardInteractable (true);
		SetPlayerButton (true);
		SetPlayerColorsInactiv ();
		startInfo.SetActive (true);
		playerMove = true;
		delay = 10;

		for (int i = 0; i < buttonList.Length; i++) 
		{
			buttonList [i].text = "";
		}
		//SetPlayersColors (playerX, playerO);

	}

	void SetBoardInteractable(bool toggle)
	{
		for (int i = 0; i < buttonList.Length; i++) 
		{
			buttonList [i].GetComponentInParent<Button> ().interactable = toggle;
		}
	}

	void SetPlayerButton(bool toggle)
	{
		playerO.button.interactable = toggle;
		playerX.button.interactable = toggle;
	}

	void SetPlayerColorsInactiv()
	{
		playerX.panel.color = inactivePlayerColor.panelColor;
		playerX.text.color = inactivePlayerColor.textColor;
		playerO.panel.color = inactivePlayerColor.panelColor;
		playerO.text.color = inactivePlayerColor.textColor;

	}

	void SetScore(int winX, int winO, int drow)
	{
		scoreWinO.text = "О выиграл: " + winO.ToString () + " раз";
		scoreWinX.text = "Х выиграл: " + winX.ToString () + " раз";
		scoreDrow.text = "Ничья: " + drow.ToString () + " раз";
	}

	public void LoadMenu()
	{
		SceneManager.LoadScene ("menu",LoadSceneMode.Single);
	}
}
