using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchStuff : MonoBehaviour {

	public CloudHandler CHandler;
	int i;
	public int maxFinger;
	int fingerCount;
	public Text score;
	private AudioSource ClickSound;
	bool screenPressed;
	public AudioClip cSound;
	public GameObject plusKills;
	Vector3 worldPos;
	private int inScore;
	private bool once = false;

	// Use this for initialization
	void Start () {
		ClickSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
			if (screenPressed == true) {
			fingerCount = 0;
			foreach (Touch touch in Input.touches) {
				if (touch.phase == TouchPhase.Began && fingerCount < maxFinger) {
					i++;
					CHandler.ScreenTapped ();
					this.GetComponent<Animator> ().SetBool ("Tapped", true);

					worldPos = Camera.main.ScreenToWorldPoint(touch.position);
					worldPos.z =0;
					GameObject plusK = (GameObject) Instantiate(plusKills,worldPos,Quaternion.identity);

					plusK.GetComponentInChildren<Text>().text = "+" + (CHandler.ScreenTapped() + 1) ; 
				}
				fingerCount++;
				screenPressed = false;

			}
		}
			score.text = i.ToString ();
			if (i > 1000) {

			if (once == false){
			CHandler.LoadAd ();
			once = true;
			Social.ReportProgress ("CgkI99TKipAJEAIQAw", 100.0f,(bool success) => {
				
				});}
		}
		
	}

	public void AcceptScore(int aScore){
		i = aScore;
		score.text = i.ToString ();
		
	}
	public int returnScore(){

		return i;
	}
public void TappedFalse()
	{
		this.GetComponent<Animator>().SetBool ("Tapped", false);

	}

public void CheckFinger(){

		screenPressed = true;
		Debug.Log ("TRUE!");

	}
	public void PlaySound(){

		ClickSound.Play();
	}
}

