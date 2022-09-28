using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendColor : MonoBehaviour {

    Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        rend.material.SetColor("_Color", Color.white);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
