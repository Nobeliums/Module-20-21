using UnityEngine;

public class Sail : MonoBehaviour, IRotatable
{
	[SerializeField] private WindSimulation _wind;
	[SerializeField] private float _maxRotationAngle;

	public float WindImpact { get; private set; }
	public Transform Transform => transform;

	private void Update()
	{
		WindImpact = Vector3.Dot(_wind.WindDirection, transform.forward);
	}

	public void RotateTo(Quaternion target)
	{
		if (Quaternion.Angle(Quaternion.identity, target) >= _maxRotationAngle)
			return;

		transform.localRotation = target;
	}
}
