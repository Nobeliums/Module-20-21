using UnityEngine;
using Random = UnityEngine.Random;

public class WindDirectionChanger : MonoBehaviour
{
	[SerializeField] private WindSimulation _simulation;
	[SerializeField] private int _rotationSpeed;
	[SerializeField] private int _maxAngleDegree;
	[SerializeField] private int _directionLifetime;

	private float _currentDirectionDegree;
	private float _timePassed;

	private void Awake()
	{
		_currentDirectionDegree = _simulation.transform.eulerAngles.y;
	}

	private void Update()
	{
		_timePassed += Time.deltaTime;

		if (IsTimePassed())
		{
			UpdateCurrentDirectionDegree();
		}

		ProcessRotation();
	}

	private bool IsTimePassed()
	{
		return _timePassed >= _directionLifetime;
	}

	private void ProcessRotation()
	{
		Vector3 newDegree = new Vector3(0, _currentDirectionDegree, 0);
		Quaternion newDirection = Quaternion.RotateTowards(_simulation.Transform.rotation,
			Quaternion.Euler(newDegree), Time.deltaTime * _rotationSpeed);
		
		_simulation.RotateTo(newDirection);
	}

	private void UpdateCurrentDirectionDegree()
	{
		int newDegree = RandomizeDirectionDegree();
		_timePassed = 0.0f;
		_currentDirectionDegree = newDegree;
	}

	private int RandomizeDirectionDegree()
	{
		return Random.Range(0, _maxAngleDegree);
	}
}