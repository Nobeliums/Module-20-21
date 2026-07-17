using UnityEngine;

public class InputHandler
{
	private const string SailRotationAxisName = "Vertical";
	private const string ShipRotationAxisName = "Horizontal";
	private const KeyCode CameraChangeButton = KeyCode.C;

	public static float GetSailRotationInput()
	{
		return Input.GetAxis(SailRotationAxisName);
	}

	public static float GetShipRotationInput()
	{
		return Input.GetAxis(ShipRotationAxisName);
	}

	public static bool IsMouseButtonDown(int mouseButton)
	{
		return Input.GetMouseButtonDown(mouseButton);
	}

	public static bool IsMouseButtonPressed(int mouseButton)
	{
		return Input.GetMouseButton(mouseButton);
	}

	public static bool IsMouseButtonUp(int mouseButton)
	{
		return Input.GetMouseButtonUp(mouseButton);
	}

	public static RaycastHit GetMousePositionRaycast(LayerMask layer)
	{
		Ray mousePositionRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		Physics.Raycast(mousePositionRay, out RaycastHit hit, Mathf.Infinity, layer);
		return hit;
	}

	public static bool IsCameraChangeButtonPressed()
	{
		return Input.GetKeyDown(CameraChangeButton);
	}
}