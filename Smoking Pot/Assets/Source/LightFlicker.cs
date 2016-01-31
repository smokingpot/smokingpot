using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {

    public float minIntensity = 0.25f;
    public float maxIntensity = 2.5f;
    public float speed = 0.05f;
    double accumulator = 0;

    Light _light;

    // Use this for initialization
	void Start () {
        _light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!_light)
            return;

        accumulator += Time.deltaTime;
        while (accumulator > speed)
        {
            _light.intensity = Random.Range(minIntensity, maxIntensity);
            accumulator -= speed;
        }

    }

}
