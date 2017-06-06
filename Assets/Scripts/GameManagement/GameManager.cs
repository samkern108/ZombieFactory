using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum Tag { Hero, Selectable, Enemy, Friendly, Radius }

public class GameManager : MonoBehaviour {

	public static int lives;
	public static bool paused = false;
	private Level level;

	/**
	 * 
	 *** GAME IDEAS ***
	 * 
	 * Your special abilities *could* come from how many civilians you've killed... dunno if I like that.
	 * But yeah, you should be able to customize your special abilities based on *something.* There should be some skill tree, semi-random, imo.
	 * 
	 **/

	public void Awake()
	{
		//TODO - IOManager should be initialized on the main menu screen, NOT here.
		//GameManager should probably be renamed to levelmanager or something.
		IOManager.Initialize ();
		LoadLevel ();
	}

	private void LoadLevel()
	{
		//GameState g = IOManager.LoadGameState ();
		//level = IOManager.LoadLevel (g.currentlevel);
		//lives = level.lives;
	}

	public void Start()
	{
		InitializeStaticObjects ();
		//AudioManager.PlayMusic (level.uid);
	}

	private void InitializeStaticObjects()
	{
		Direct.directFlag = ResourceLoader.LoadGameObject("DirectFlag");
	}

	public static void GameOver()
	{
		if (Enemy.enemyFailCounter < lives) {
			UIManager.self.ShowVictoryPanel ();
		}
		else 
			UIManager.self.ShowDefeatPanel ();	
	}

	// TODO
	public static void RestartLevel()
	{
		SceneManager.LoadScene ("Game");
	}

	// TODO
	public static void QuitToMenu()
	{
		SceneManager.LoadScene ("Game");
	}
		
	public static void PauseGame(bool pause)
	{
		paused = pause;
		Notifications.self.SendPauseNotification(pause);
	}
}
