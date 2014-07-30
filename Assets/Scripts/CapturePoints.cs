using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using PDollarGestureRecognizer;
using PDollarDemo;

public class CapturePoints : MonoBehaviour {

	public Transform gestureOnScreen;

	private List<Gesture> trainingSet = new List<Gesture>();

	private List<Point> points = new List<Point>();
	private int strokeId = -1;
	private Vector3 virtualKeyPosition = Vector2.zero;

	private RuntimePlatform platform;

	// Use this for initialization
	void Start () {

		platform = Application.platform;

		TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
		
		foreach (TextAsset gestureXml in gesturesXml)
			trainingSet.Add(GestureIO.ReadGesture(gestureXml.text));
	}
	
	// Update is called once per frame
	void Update () {

		if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer) {
			if (Input.touchCount > 0) {
				virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
			}
		} else {
			if (Input.GetMouseButton(0)) {
				virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
			}
		}

		if (Input.GetMouseButtonDown(0)) {
			points.Clear();
			++strokeId;
		}
		
		if (Input.GetMouseButton(0)) {
			points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));
			gestureOnScreen.position = WorldCoordinateForGesturePoint(virtualKeyPosition);
		}
		
		if (Input.GetMouseButtonUp(0)) {

			Gesture candidate = new Gesture(points.ToArray());
			
			Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());
			
			Debug.Log(gestureResult.GestureClass);
			Debug.Log(gestureResult.Score);

			strokeId = -1;
		}
	}

	private Vector3 WorldCoordinateForGesturePoint(Vector3 gesturePoint) {

		Vector3 worldCoordinate = new Vector3(gesturePoint.x, gesturePoint.y, 10);

		return Camera.main.ScreenToWorldPoint(worldCoordinate);
	}
}
