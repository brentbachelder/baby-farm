using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Transform target;
	public float cameraSpeed = 0.01f;
	private int currentScene;
	[SerializeField] private GameObject[] sceneObjects;

	private void Start()
	{
		currentScene = 1;
	}
	
	private void LateUpdate()
	{
		target = sceneObjects[currentScene].transform;
		if(Mathf.Abs(transform.position.x - target.position.x) > cameraSpeed) SmoothFollow();
	}

	public void SmoothFollow()
	{
		Vector3 smoothFollow = Vector3.Lerp(transform.position, target.position, cameraSpeed);
		transform.position = new Vector3(smoothFollow.x, transform.position.y, transform.position.z);
		if(Mathf.Abs(transform.position.x - target.position.x) < cameraSpeed)
			transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
	}

	public void ChangeTarget(int direction)
	{
		if(currentScene + direction >= 0 && currentScene + direction < sceneObjects.Length)
			currentScene += direction;
	}
}