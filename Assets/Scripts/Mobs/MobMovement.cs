using UnityEngine;
using System.Collections;

public class MobMovement : MonoBehaviour
{
		Vector3 startPos;
		Vector3 target;

		public void SetTarget(Vector3 target)
		{
				startPos = transform.position;
				this.target = target;
		}

		public void Update()
		{
				if (target != null) {

						//TODO figure out why the zombies jump when they start moving.
						transform.position = Vector3.Lerp (startPos, target, Time.time/100);
				}
		}
}
