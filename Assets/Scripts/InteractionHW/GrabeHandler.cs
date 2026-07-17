using UnityEngine;

public class GrabeHandler
{
	private LayerMask _itemsLayers;
	private LayerMask _graberLayers;

	private float _grabedItemMoveSpeed;
	private IPickable _grabedItem;

	public GrabeHandler(LayerMask itemsLayers, LayerMask graberLayers, float grabedItemMoveSpeed, InputHandler input)
	{
		_itemsLayers = itemsLayers;
		_graberLayers = graberLayers;
		_grabedItemMoveSpeed = grabedItemMoveSpeed;
	}

	public bool TryGetPickable(out IPickable item)
	{
		item = null;

		RaycastHit hit = InputHandler.GetMousePositionRaycast(_itemsLayers);

		if (hit.collider != null)
			hit.collider.TryGetComponent(out item);

		return item != null;
	}

	public void MoveGrabedItemToMousePosition()
	{
		if (_grabedItem == null)
			return;

		RaycastHit hit = InputHandler.GetMousePositionRaycast(_graberLayers);
		
		Vector3 direction = Vector3.MoveTowards(_grabedItem.GameObject.transform.position, hit.point,
			_grabedItemMoveSpeed * Time.fixedDeltaTime);
		_grabedItem.GameObject.GetComponent<Rigidbody>().MovePosition(direction);
		}

	public void GrabePickable(IPickable item)
	{
		item.OnPicked();
		_grabedItem = item;
		_grabedItem.GameObject.GetComponent<Rigidbody>().useGravity = false;
	}

	public void UnGrabePickable()
	{
		if (_grabedItem == null)
			return;

		Rigidbody rb = _grabedItem.GameObject.GetComponent<Rigidbody>();
		rb.useGravity = true;
		rb.velocity = Vector3.zero;
		_grabedItem.OnUnPicked();
		_grabedItem = null;
	}
}