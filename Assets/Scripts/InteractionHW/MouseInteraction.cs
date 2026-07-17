using System;
using UnityEngine;

public class MouseInteraction : MonoBehaviour
{
	private const int LeftMouseButton = 0;
	private const int RightMouseButton = 1;

	[SerializeField] private LayerMask _itemsLayers;
	[SerializeField] private LayerMask _groundLayers;
	[SerializeField] private LayerMask _graberLayers;

	[SerializeField] private float _grabedItemMoveSpeed;

	[SerializeField] private float _explosionRadius;
	[SerializeField] private float _explosionForce;

	private IPickable _item;
	private InputHandler _input;
	private Exploder _exploder;
	private GrabeHandler _graber;

	private void Start()
	{
		_input = new InputHandler();
		_exploder = new Exploder(_explosionForce, _explosionRadius, _itemsLayers);
		_graber = new GrabeHandler(_itemsLayers, _graberLayers, _grabedItemMoveSpeed, _input);
	}

	private void Update()
	{
		ProcessExploder();
		ProcessGraber();
	}

	private void FixedUpdate()
	{
		_graber.MoveGrabedItemToMousePosition();
	}

	private void ProcessExploder()
	{
		if (InputHandler.IsMouseButtonDown(RightMouseButton))
		{
			RaycastHit mousePosition = InputHandler.GetMousePositionRaycast(_groundLayers);
			_exploder.CreateExplosion(mousePosition.point);
		}
	}

	private void ProcessGraber()
	{
		if (InputHandler.IsMouseButtonDown(LeftMouseButton))
		{
			if (_graber.TryGetPickable(out IPickable item))
			{
				_graber.GrabePickable(item);
			}
		}

		if (InputHandler.IsMouseButtonUp(LeftMouseButton))
		{
			_graber.UnGrabePickable();
		}
	}
}