using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour {

	private SpriteRenderer spriteRenderer;

	public bool stationary = false;

	private float heightOffset;

	// I played around with offset percentages to find the perfect number 
	// at which objects disappear behind each other.
	// This might have to change with units of different sizes/with different overlay rules,
	// but for now, this magic number looks fine.
	private const float kMagicHeightMultiplier = .11f;

	void Start() {
		spriteRenderer = GetComponent <SpriteRenderer>();

		heightOffset = transform.localScale.y * kMagicHeightMultiplier;

		// If the object never moves, update the sorting layer once and then deactivate this.
		if (stationary) {
			UpdateSortingLayer();
			this.enabled = false;
		}
	}
		
	private void UpdateSortingLayer()
	{
		int pos = Mathf.RoundToInt((transform.position.y - heightOffset) * 100);
		pos /= 3;
		spriteRenderer.sortingOrder = (pos * -1);
	}
		
	void Update () {
		UpdateSortingLayer ();
	}
}
