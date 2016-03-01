using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Player player;

    public Text scoreLabel;

	public void StartGame (int mode) {
        player.StartGame(mode);
        gameObject.SetActive(false);
	}

    public void EndGame(float distanceTraveled) {
        scoreLabel.text = ((int)(distanceTraveled * 10f)).ToString();
        gameObject.SetActive(true);
    }

}
