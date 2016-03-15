using UnityEngine;
using System.Collections;

public class UniversalRotator : MonoBehaviour {

    public float XRotation, YRotation, ZRotation;

	void Start () {
	
	}
	
	
	void Update () {

        transform.Rotate(XRotation* Time.deltaTime, YRotation * Time.deltaTime, ZRotation * Time.deltaTime) ;
	
	}
}
