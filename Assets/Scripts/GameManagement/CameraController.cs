using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public static Camera _camera;

	public SpriteRenderer background;

	private bool following = false;
	private float followDoublePressTimer = 0f;
	private float followDoublePressTimeout = .5f;
	private Vector3 moveVector;

	private float followSpeed = 3f;
	private float cameraSpeed = .1f;

	private float max_x, min_x, max_y, min_y;

	void Start () {
		_camera = GetComponent<Camera>();

		Bounds b = background.bounds;

		float height = _camera.orthographicSize;
		float width = height * _camera.aspect;

		min_y = b.min.y + height;
		min_x = b.min.x + width;
		max_y = b.max.y - height;
		max_x = b.max.x - width;
	}

	//TODO Instead of doing whatever the fuck i'm doing, I think
	//the camera should take a second to speed up, then follow the target
	//precisely, then take a second to slow down.

	private Vector3 newPosition;

	public void Update()
	{
		bool followPress = Input.GetKeyDown (KeyCode.F);

		if (followDoublePressTimer > 0) {
			followDoublePressTimer -= Time.deltaTime;
			if (following && followPress) {
				followDoublePressTimer = 0;
				SnapToTarget ();
			}
		}
		else if (followPress) {
			following = !following;
			if (following)
				followDoublePressTimer = followDoublePressTimeout;
		}

		if (following) {
			moveVector = (SelectionManager.selected.transform.position - transform.position) * followSpeed * Time.deltaTime;
			moveVector.z = 0;
		} else {
			moveVector = new Vector3 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * cameraSpeed;
		}

		newPosition = transform.position + moveVector;
		newPosition.x = Mathf.Clamp (newPosition.x, min_x, max_x);
		newPosition.y = Mathf.Clamp (newPosition.y, min_y, max_y);
		transform.position = newPosition;
	}

	//TODO should we drift to target immediately upon changing the selected target?

	private void SnapToTarget()
	{
		Vector2 newPos = SelectionManager.selected.transform.position;
		transform.position = newPos;
	}
}
