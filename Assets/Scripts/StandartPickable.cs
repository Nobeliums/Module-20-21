
using UnityEngine;public class StandartPickable : MonoBehaviour, IPickable
{
	public GameObject GameObject => gameObject;

	[SerializeField] private ParticleSystem _pickedEffect;
	public void OnPicked()
	{
		_pickedEffect.Play();
	}

	public void OnUnPicked()
	{
		_pickedEffect.Stop();
	}
}