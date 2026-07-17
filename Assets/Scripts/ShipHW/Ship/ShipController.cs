using System;
using System.Timers;
using UnityEngine;

public class ShipController : MonoBehaviour
{
	[SerializeField] private Sail _sail;
	[SerializeField] private Ship _ship;

	[SerializeField] private float _rotationSpeed;

	private InputHandler _input;

	private void Awake()
	{
		_input = new InputHandler();
	}

	private void Update()
	{
		ProcessRotationTo(_sail, _input.GetSailRotationInput());
		ProcessRotationTo(_ship, _input.GetShipRotationInput());
	}

	private void ProcessRotationTo(IRotatable rotatable, float input)
	{
		float targetAngel = rotatable.Transform.localRotation.eulerAngles.y +
		                    (_rotationSpeed * input * Time.deltaTime);
		
		Quaternion targetRotation = Quaternion.Euler(new Vector3(0, targetAngel, 0));
		rotatable.RotateTo(targetRotation);
	}
}