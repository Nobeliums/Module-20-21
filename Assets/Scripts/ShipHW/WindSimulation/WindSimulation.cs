using UnityEngine;

public class WindSimulation : MonoBehaviour, IRotatable
{
	[field: SerializeField] private Transform Arrow { get; set; }
	public Vector3 WindDirection => transform.forward;

	public Transform Transform => transform;

	private void Update()
	{
		Arrow.rotation = transform.rotation;
	}

	public void RotateTo(Quaternion target)
	{
		transform.rotation = target;
	}
}