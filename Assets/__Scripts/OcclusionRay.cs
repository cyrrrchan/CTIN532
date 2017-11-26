using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionRay : MonoBehaviour {

    Vector3 _originPoint;
    public Transform _target;
    public float _DistanceToPlayer;
    public float _MaxAttenuation;
    public GameObject _player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        _originPoint = transform.position;
        RaycastHit HitInfo;

        Vector3 targetDirection = _target.position - transform.position;

        _DistanceToPlayer = Vector3.Distance(_target.position, _originPoint);

        _MaxAttenuation = AkSoundEngine.GetMaxRadius(this.gameObject);

        if(_DistanceToPlayer <= _MaxAttenuation)
        {
            Physics.Raycast(_originPoint, targetDirection, out HitInfo, _DistanceToPlayer);

            if(HitInfo.collider == null)
            {
                AkSoundEngine.SetObjectObstructionAndOcclusion(this.gameObject, _player, 0.0f, 0.0f);
                Debug.DrawRay(_originPoint, targetDirection, Color.blue);
            }

            else if (HitInfo.collider.tag == "Wall")
            {
                AkSoundEngine.SetObjectObstructionAndOcclusion(this.gameObject, _player, 0.0f, 0.5f);
                Debug.DrawRay(_originPoint, targetDirection, Color.red);
            }
        }
	}
}
