using UnityEngine.SceneManagement;
using UnityEngine;

public class CompleteLevel : MonoBehaviour {

	public string menuSceneName = "MainMenu";
	public string nextLevel = "Level03";
	public int levelToUnlock = 3;

	public SceneFader sceneFader;

	public void Continue() {
		PlayerPrefs.SetInt("levelReached", levelToUnlock);
		sceneFader.FadeTo(nextLevel);
	}

	public void Menu() {
		sceneFader.FadeTo(menuSceneName);
	}
}
