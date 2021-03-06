﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour {

	public GameObject treePrefab;

	public int assetLimit = 80;

	void Start () {
		for (int i = 0; i < assetLimit; i++) {
			PlaceTree ();
		}
	}

	private void PlaceTree() {
		GameObject tree = GameObject.Instantiate (treePrefab);
		tree.transform.position = new Vector3 (Random.Range(-10.0f,10.0f), Random.Range(-10.0f,10.0f), 5);
		Vector3 adjustedScale = tree.transform.localScale;
		float x = Random.Range (0.7f, 1.3f);
		adjustedScale.x *= x;
		adjustedScale.y *= Random.Range (x - .1f, x + .1f);
			tree.transform.localScale = adjustedScale;
		Color adjustedColor = tree.GetComponent <SpriteRenderer> ().color;
		adjustedColor.b *= Random.Range (0.5f, 1.0f);
		adjustedColor.r *= Random.Range (0.5f, 1.0f);
		adjustedColor.g *= Random.Range (0.5f, 1.0f);
		tree.GetComponent <SpriteRenderer>().color = adjustedColor;
		tree.transform.SetParent (transform);
	}
}
