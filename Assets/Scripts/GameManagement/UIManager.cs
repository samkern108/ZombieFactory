using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager self;
	private static GameObject menu;

	public void Start()
	{
		self = this;
		menu = transform.Find ("Menu").gameObject;
	}

	public void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
			menu.SetActive (!menu.activeInHierarchy);
	}

	public static void OpenStatsPanel(EntityStats hs)
	{
		StatsPanel.self.ShowPanel (hs);
	}

	public static void CloseStatsPanel()
	{
		StatsPanel.self.ClosePanel ();
	}

	public void ShowVictoryPanel()
	{
		transform.Find ("VictoryPanel").gameObject.SetActive (true);
		GameManager.PauseGame (true);
	}

	public void ShowDefeatPanel()
	{
		transform.Find ("DefeatPanel").gameObject.SetActive (true);
		GameManager.PauseGame (true);
	}

	public void RetryButton()
	{
		GameManager.PauseGame (false);
		GameManager.RestartLevel ();
		menu.SetActive (false);
	}

	public void QuitButton()
	{
		GameManager.PauseGame (false);
		GameManager.QuitToMenu ();
	}
}
