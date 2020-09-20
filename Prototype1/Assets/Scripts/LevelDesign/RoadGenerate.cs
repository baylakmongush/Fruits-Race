using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerate : MonoBehaviour
{
	public Transform Pos;
	public GameObject[] PrefabsRoad;
	float step = 0;
	// Update is called once per frame
	void Update()
	{
		step += 10;
		Instantiate(PrefabsRoad[0], new Vector2(Pos.position.x + step, Pos.position.y), Quaternion.identity);
	}
}
