using UnityEngine;
using System.Collections;
using Pathfinding;


public class Movement : MonoBehaviour {

	private float speed = 0;
	private Transform target;
	private Rigidbody2D self;
	
	public Path path;
	public float nextWaypointDistance = 3;
	private int currentWaypoint = 0;
	
	void Awake()
	{
		self = this.GetComponent<Rigidbody2D>();
	}
	
	public void OnPathComplete (Path p) 
	{
		Debug.Log ("Yay, we got a path back. Did it have an error? " +p.error);
		
		if (!p.error) {
			path = p;
			//Reset the waypoint counter
			currentWaypoint = 0;
		}
	}
	
	public void AssignSpeedAndDirection(float s, Transform t)
	{
		speed = s;
		target = t;
		
		Seeker seeker = GetComponent<Seeker>();
		seeker.pathCallback += OnPathComplete;
		seeker.StartPath (transform.position,target.position);
	}
	
	public void FixedUpdate () {
		if (path == null) {
			//We have no path to move after yet
			return;
		}
		
		if (currentWaypoint >= path.vectorPath.Count) {
			Debug.Log ("End Of Path Reached");
			return;
		}
		
		//Direction to the next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;
		self.MovePosition (dir);
		
		//Check if we are close enough to the next waypoint
		//If we are, proceed to follow the next waypoint
		if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
	}
}
