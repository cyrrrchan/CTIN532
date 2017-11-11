using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAgainstWill : MonoBehaviour {
	UnityStandardAssets.Characters.FirstPerson.FirstPersonController _firstPersonControllerScript;

	[SerializeField] Vector3 _goalPosition;
	Vector3 _originPosition;
	[SerializeField] float _duration = 10.0f;
	[SerializeField] AnimationCurve _forceMoveCurve;
	bool _isMovingAgainstWill = false;
	float _cnt = 0.0f;

	// Use this for initialization
	void Start () {
		_firstPersonControllerScript = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (_isMovingAgainstWill) {
			_cnt += Time.deltaTime;
			transform.position = Vector3.Lerp (_originPosition, _goalPosition, _forceMoveCurve.Evaluate(_cnt/_duration));
			if (_cnt >= _duration) {
				_isMovingAgainstWill = false;
				DoYourEndStuff ();
			}
		}
	}

	public void TriggerMoveAgainstWill(){
		// first make the player Stop moving
		_goalPosition.y = transform.position.y;
		_firstPersonControllerScript.StopWalk (true);
		StartCoroutine (DelayUntilForcedMove ());

	}

	void DoYourEndStuff(){
		
	}

	IEnumerator DelayUntilForcedMove(){
		// is storing the position of the player upon trigger for use in a lerp later
		_originPosition = transform.position;
		// waiting for however many seconds
		yield return new WaitForSeconds (2.0f);
		_cnt = 0.0f;
		_isMovingAgainstWill = true;
	}
}
