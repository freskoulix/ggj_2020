using UnityEngine;

public class RepairEngine : MonoBehaviour
{
  public Texture2D idleTexture;
  public Texture2D repairTexture;
  private float repairUnit = 100f;
  private Wall wall;
  private Color originalColor;
  private MeshRenderer meshRenderer;
  private CursorMode cursorMode = CursorMode.Auto;
  private Vector2 hotSpot = Vector2.zero;
  private Color mouseOverColor = Color.grey;
  private bool isCursorOverWall = false;

  void Start()
  {
    mouseOverColor.a = 0.2f;
    wall = transform.parent.GetChild(0).GetComponent<Wall>();
    meshRenderer = transform.GetComponent<MeshRenderer>();
    originalColor = meshRenderer.material.color;
  }

  void OnDestroy()
  {
    restoreCursor();
  }

  void OnMouseOver()
  {
    meshRenderer.material.color = mouseOverColor;

    if (!isCursorOverWall)
    {
      setCursor(idleTexture);
      isCursorOverWall = true;
    }
  }

  void OnMouseExit()
  {
    meshRenderer.material.color = originalColor;

    restoreCursor();
    isCursorOverWall = false;
  }

  void OnMouseDown()
  {
    if (isCursorOverWall)
    {
      setCursor(repairTexture);
      wall.RepairDamage(repairUnit);
    }
  }

  void OnMouseUp()
  {
    if (isCursorOverWall)
    {
      setCursor(idleTexture);
    }
  }

  private void setCursor(Texture2D texture)
  {
    Cursor.SetCursor(texture, hotSpot, cursorMode);
  }

  private void restoreCursor()
  {
    Cursor.SetCursor(null, Vector2.zero, cursorMode);
  }
}
