using UnityEngine;
using System.Collections;

public class OneDirection : MonoBehaviour {

	public float speed;
	public Vector3 direction = Vector3.up;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += direction * speed;
	}
}
