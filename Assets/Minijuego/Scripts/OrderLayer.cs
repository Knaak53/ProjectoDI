using UnityEngine;
using System.Collections;

public class OrderLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GetComponentInChildren<SpriteRenderer>().sortingOrder = (-(int)transform.position.y);
	}
}
