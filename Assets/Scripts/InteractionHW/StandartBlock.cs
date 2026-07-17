
using System;
using UnityEngine;

public class StandartBlock : MonoBehaviour, IPickable, IExplodable
{
	[SerializeField] private ParticleSystem _pickedEffect;

	private Rigidbody _rb;

	public GameObject GameObject => gameObject;

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
	}

	public void OnPicked()
	{
		_pickedEffect.Play();
	}

	public void OnUnPicked()
	{
		_pickedEffect.Stop();
	}

	public void ApplyExplosion(Vector3 explosionCenter, float explosionRadius, float explosionForce)
	{
		_rb.AddExplosionForce(explosionForce, explosionCenter, explosionRadius);
	}
}