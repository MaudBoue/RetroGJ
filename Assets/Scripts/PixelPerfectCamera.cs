using UnityEngine;
using System.Collections;

public class PixelPerfectCamera : MonoBehaviour {

	public float textureSize = 100f;
	float unitsPerPixel;

	// Use this for initialization
	void Start () {
		unitsPerPixel = 1f / textureSize;
		Camera.main.orthographicSize = ( (Screen.height / 2f) * unitsPerPixel ) / LoadScript.zoom;
	}

	public static Bounds OrthographicBounds(Camera camera)
	{
		float screenAspect = (float)Screen.width / (float)Screen.height;
		float cameraHeight = camera.orthographicSize * 2;
		Bounds bounds = new Bounds(
			camera.transform.position,
			new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
		return bounds;
	}
}
