using UnityEngine;
using System.Collections;

public class ObstacleManager : MonoBehaviour 
{
	public GameObject obstacle;

	// Use this for initialization
	void Start()
	{
		InvokeRepeating("CreateObstacle", 1f, 1f);
	}

	void CreateObstacle()
	{
		GameObject obstacleGO = Instantiate(obstacle) as GameObject;
		obstacleGO.transform.position = new Vector3(obstacleGO.transform.position.x, Random.Range(-2, 2), obstacleGO.transform.position.z);
	}
}
