using UnityEngine;

public class InputHandler
{
	private const string SailRotationAxisName = "Vertical";
	private const string ShipRotationAxisName = "Horizontal";

	public float GetSailRotationInput()
	{
		return Input.GetAxis(SailRotationAxisName);
	}

	public float GetShipRotationInput()
	{
		return Input.GetAxis(ShipRotationAxisName);
	}

	public bool IsMouseButtonDown(int mouseButton)
	{
		return Input.GetMouseButtonDown(mouseButton);
	}

	public bool IsMouseButtonPressed(int mouseButton)
	{
		return Input.GetMouseButton(mouseButton);
	}

	public bool IsMouseButtonUp(int mouseButton)
	{
		return Input.GetMouseButtonUp(mouseButton);
	}

	public RaycastHit GetMousePositionRaycast(LayerMask layer)
	{
		Ray mousePositionRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		Physics.Raycast(mousePositionRay, out RaycastHit hit, Mathf.Infinity, layer);
		return hit;
	}
}