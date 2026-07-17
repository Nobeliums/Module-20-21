using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CamerasController : MonoBehaviour
{
	[SerializeField] private List<CinemachineVirtualCamera> _cameras;

	private Queue<CinemachineVirtualCamera> _camerasQueue;
	private CinemachineVirtualCamera _currentCamera;

	private void Awake()
	{
		_camerasQueue = new Queue<CinemachineVirtualCamera>(_cameras);
		SetNextCamera();
	}

	private void Update()
	{
		if (InputHandler.IsCameraChangeButtonPressed())
		{
			SetNextCamera();
		}
	}

	private void SetNextCamera()
	{
		CinemachineVirtualCamera nextCamera = _camerasQueue.Dequeue();

		if (_currentCamera != null)
		{
			_camerasQueue.Enqueue(_currentCamera);
			_currentCamera.gameObject.SetActive(false);
		}

		_currentCamera = nextCamera;
		_currentCamera.gameObject.SetActive(true);
	}
}
