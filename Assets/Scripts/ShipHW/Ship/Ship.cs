using System;
using UnityEngine;

public class Ship : MonoBehaviour, IRotatable
{
	[SerializeField] private float _maxVelocity;
	[SerializeField] private Sail _sail;

	private float _currentVelocity;

	public Transform Transform => transform;

	private void Update()
	{
		float sailDot = Vector3.Dot(transform.forward, _sail.transform.forward);
		_currentVelocity = _sail.WindImpact > 0.0f ? _maxVelocity * _sail.WindImpact : 0.0f;
		_currentVelocity = _currentVelocity * sailDot;
		transform.position += transform.forward * _currentVelocity * Time.deltaTime;
	}

	public void RotateTo(Quaternion target)
	{
		transform.rotation = target;
	}
}
