using UnityEngine;

public interface IPickable
{
	GameObject GameObject { get; }

	void OnPicked();
	void OnUnPicked();
}