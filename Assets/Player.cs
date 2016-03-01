using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public PipeSystem pipeSystem;
	//public float velocity;

	private Pipe currentPipe;

	private float distanceTraveled;

	private float deltaToRotation;
	private float systemRotation;

	private Transform world, Rotater;

	private float worldRotation, avatarRotation;

	public float rotationVelocity;

    public MainMenu mainMenu;

    public float startVelocity;

    public float[] accelarations;

    private float accelaration, velocity;

    public HUD hud;

	public void StartGame(int accelarationMode){
        distanceTraveled = 0f;
        avatarRotation = 0f;
        systemRotation = 0f;
        worldRotation = 0f;
        accelaration = accelarations[accelarationMode];
        velocity = startVelocity;
		currentPipe = pipeSystem.SetupFirstPipe ();
		deltaToRotation = 360f / (2f * Mathf.PI * currentPipe.CurveRadius);
		SetupCurrentPipe ();
        gameObject.SetActive(true);

        currentPipe.setDifficulty(distanceTraveled);

        hud.SetValues(distanceTraveled, velocity, currentPipe.DifficultyFactor);
	}

    private void Awake() {
        world = pipeSystem.transform.parent;
        Rotater = transform.GetChild(0);
        gameObject.SetActive(false);
    }

	private void Update(){
        velocity += accelaration * Time.deltaTime;
		float delta = velocity * Time.deltaTime;
		distanceTraveled += delta;
		systemRotation += delta * deltaToRotation;

		if (systemRotation >= currentPipe.CurveAngle) {
			delta = (systemRotation - currentPipe.CurveAngle) / deltaToRotation;
			currentPipe = pipeSystem.SetupNextPipe();
			deltaToRotation = 360f / (2f * Mathf.PI * currentPipe.CurveRadius);
			SetupCurrentPipe();
			systemRotation = delta * deltaToRotation;
		}

		pipeSystem.transform.localRotation = 
			Quaternion.Euler (0f, 0f, systemRotation);

        currentPipe.setDifficulty(distanceTraveled);

        UpdateAvatarRotation();

        hud.SetValues(distanceTraveled, velocity, currentPipe.DifficultyFactor);
	}

	private void SetupCurrentPipe(){
		deltaToRotation = 360f / (2f * Mathf.PI * currentPipe.CurveRadius);
		worldRotation += currentPipe.RelativeRotation;
		if (worldRotation < 0f) {
			worldRotation += 360f;
		} else if (worldRotation >= 360f) {
			worldRotation -= 360f;
		}
		world.localRotation = Quaternion.Euler (worldRotation, 0f, 0f);
	}

	public void UpdateAvatarRotation(){
        float rotationInput = 0f;
        if (Application.isMobilePlatform)
        {
            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).position.x < Screen.width * 0.5f)
                {
                    rotationInput = -1f;
                }
                else
                    rotationInput = 1f;
            }
        }
        else
            rotationInput = Input.GetAxis("Horizontal");
		avatarRotation += 
			rotationVelocity * Time.deltaTime * rotationInput;
		if (avatarRotation < 0)
			avatarRotation += 360f;
		if (avatarRotation > 360f)
			avatarRotation -= 360f;
		Rotater.localRotation = Quaternion.Euler (avatarRotation, 0f, 0f);
	}

    public void Die() {
        mainMenu.EndGame(distanceTraveled);
        gameObject.SetActive(false);
    }

}

