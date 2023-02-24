using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    //start position for the parallax game object
    Vector2 startingPositioin;

    //start z value of the parallax game object
    float startingZ;
    float startingY;


    Vector2 camMoveSinceStart => (Vector2) cam.transform.position - startingPositioin;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    float clipingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clipingPlane;

    // Start is called before the first frame update
    void Start()
    {
        startingPositioin = transform.position;
        startingZ = transform.position.z;
        startingY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startingPositioin + camMoveSinceStart * parallaxFactor;

        transform.position = new Vector3(newPosition.x, startingY, startingZ); 
    }
}
