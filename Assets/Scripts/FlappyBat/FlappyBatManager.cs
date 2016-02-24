using UnityEngine;
using System.Collections;

public class FlappyBatManager : MonoBehaviour 
{
	public GameObject obstacle;
	public GameObject flower;

	// Use this for initialization
	void Start()
	{
		InvokeRepeating("CreateObstacle", 1f, 1f);
		InvokeRepeating("CreateFlower", 1.5f, 1.75f);
	}

	void CreateObstacle()
	{
		GameObject obstacleGO = Instantiate(obstacle) as GameObject;
		obstacleGO.transform.position = new Vector3(obstacleGO.transform.position.x, Random.Range(-2, 2), obstacleGO.transform.position.z);
	}

	void CreateFlower()
	{
		GameObject flowerGO = Instantiate(flower) as GameObject;
		flowerGO.transform.position = new Vector3(flowerGO.transform.position.x, Random.Range(-3, 4), flowerGO.transform.position.z);
	}
}
