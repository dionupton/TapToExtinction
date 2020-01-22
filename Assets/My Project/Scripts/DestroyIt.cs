using UnityEngine;
using System.Collections;

public class DestroyIt : MonoBehaviour {

	public float timeToDestroy;
	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, timeToDestroy);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
