using UnityEngine;

public class Exploder
{
	private float _explosionForce;
	private float _explosionRadius;
	private LayerMask _explorableLayerMask;

	public Exploder(float explosionForce, float explosionRadius, LayerMask explorableLayerMask)
	{
		_explosionForce = explosionForce;
		_explosionRadius = explosionRadius;
		_explorableLayerMask = explorableLayerMask;
	}

	public void CreateExplosion(Vector3 point)
	{
		RaycastHit[] hits = Physics.SphereCastAll(point, _explosionRadius, Vector3.up, 0.0f, _explorableLayerMask);

		foreach (RaycastHit hit in hits)
		{
			IExplodable explodable = hit.collider.GetComponent<IExplodable>();

			if (explodable != null)
			{
				explodable.ApplyExplosion(point, _explosionRadius, _explosionForce);
			}
		}
	}
}