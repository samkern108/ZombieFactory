using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour {

	private SpriteRenderer spriteRenderer;

	public int sortingOrder;

	void Start() {
		spriteRenderer = GetComponent <SpriteRenderer>();
	}

	void Update () {
		int pos = Mathf.RoundToInt(transform.position.y * 100);
		pos /= 3;
		spriteRenderer.sortingOrder = (pos * -1);// + OrderOffset;
		sortingOrder = spriteRenderer.sortingOrder;
	}
}
