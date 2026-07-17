using UnityEngine;

public interface IRotatable
{
	Transform Transform { get; }
	void RotateTo(Quaternion target);
}