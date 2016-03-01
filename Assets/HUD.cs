using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public Text distanceLabel, debugLabel;

    public void SetValues(float distanceTraveled, float velocity, float difficulty) {
        distanceLabel.text = ((int)(distanceTraveled * 10f)).ToString();
        debugLabel.text = "Debug: \n Velocity:" + ((int)(velocity * 10f)).ToString() + "\nDifficultyFactor: "+ difficulty;        
    }

}
