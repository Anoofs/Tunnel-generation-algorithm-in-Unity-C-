using UnityEngine;
using System.Collections;

public class Avatar : MonoBehaviour {

    public ParticleSystem shape, trail, burst;

    private Player player;

    public float deathCountDown = -1f;

    private void Awake() {
        player = transform.root.GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider collider) {
       // Debug.Log("Inside collision");

        if (deathCountDown < 0f) {
       //     Debug.Log("Inside deathcountdown");
            shape.enableEmission = false;
            trail.enableEmission = false;
            burst.Emit(burst.maxParticles);
            deathCountDown = burst.startLifetime;
        }
    }

    private void Update() {
        if (deathCountDown >= 0f) {
            deathCountDown -= Time.deltaTime;
            if (deathCountDown <= 0f) {
                deathCountDown = -1f;
                shape.enableEmission = true;
                trail.enableEmission = true;
                player.Die();
            }
        }
    }
}
