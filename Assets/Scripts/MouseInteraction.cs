using System;
using UnityEngine;

public class MouseInteraction : MonoBehaviour
{
	private const int MouseLeftButton = 0;

	[SerializeField] private LayerMask _pickableLayers;
	[SerializeField] private LayerMask _groundLayers;
	[SerializeField] private LayerMask _groundGraberLayers;

	[SerializeField] private float _grabedItemMoveSpeed;

	[SerializeField] private float _explosionRadius;
	[SerializeField] private float _explosionForce;

	private IPickable _item;

	private bool _isLeftButtonDown;
	private bool _isLeftButtonClicked;
	private bool _isLeftButtonUp;

	private void Update()
	{
		GetMouseInput();
		
		if (Input.GetMouseButtonDown(1))
			CreateExplosion();
	}

	private void FixedUpdate()
	{
		MouseGrabeProcess();
	}

	private void GetMouseInput()
	{
		if (Input.GetMouseButtonDown(MouseLeftButton) && _isLeftButtonDown == false)
		{
			_isLeftButtonDown = true;
			_isLeftButtonUp = false;
		}

		if (Input.GetMouseButtonUp(MouseLeftButton) && _isLeftButtonUp == false)
		{
			_isLeftButtonUp = true;
			_isLeftButtonDown = false;
		}
		
		_isLeftButtonClicked = Input.GetMouseButton(MouseLeftButton);
	}

	private void CreateExplosion()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundLayers));
		{
			RaycastHit[] hits = Physics.SphereCastAll(hit.point, _explosionRadius, Vector3.up,
				_explosionRadius, _pickableLayers);

			foreach (var hited in hits)
			{
				var rb = hited.collider.GetComponent<Rigidbody>();
				if (rb == null)
					continue;
				
				rb.AddExplosionForce(_explosionForce, hit.point, _explosionRadius);
			}
		}
		
		
	}

	private void MouseGrabeProcess()
	{
		if (_isLeftButtonDown)
		{
			if (TryGetPickable(out IPickable grabedItem))
				GrabePickable(grabedItem);

			_isLeftButtonDown = false;
		}

		if (_isLeftButtonClicked)
		{
			if (_item == null)
				return;
			
			MoveGrabedItemToMousePosition();
		}

		if (_isLeftButtonUp)
		{
			UnGrabePickable();
			_isLeftButtonUp = false;
		}
	}

	private bool TryGetPickable(out IPickable item)
	{
		item = null;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out RaycastHit hit,Mathf.Infinity, _pickableLayers))
		{
			item = hit.collider.GetComponent<IPickable>();
		}

		return item != null;
	}

	private void MoveGrabedItemToMousePosition()
	{
		if (_item == null)
			return;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundGraberLayers))
		{
			Vector3 direction = Vector3.MoveTowards(_item.GameObject.transform.position, hit.point,
				_grabedItemMoveSpeed * Time.fixedDeltaTime);
			_item.GameObject.GetComponent<Rigidbody>().MovePosition(direction);
		}
	}

	public void GrabePickable(IPickable item)
	{
		item.OnPicked();
		_item = item;
		_item.GameObject.GetComponent<Rigidbody>().useGravity = false;
	}

	public void UnGrabePickable()
	{
		if (_item == null)
			return;

		Rigidbody rb = _item.GameObject.GetComponent<Rigidbody>();
		rb.useGravity = true;
		rb.velocity = Vector3.zero;
		_item.OnUnPicked();
		_item = null;
	}
}