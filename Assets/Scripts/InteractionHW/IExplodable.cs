using UnityEngine;

public interface IExplodable
{
	public void ApplyExplosion(Vector3 explosionCenter, float explosionRadius, float explosionForce);
}