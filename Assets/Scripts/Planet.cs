using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
    public enum EditionModes { NONE, POSTIION, SIZE, TILT, MASS }
    public EditionModes editionMode = EditionModes.NONE;
    public StarConfiguration starConfig;
    public Text text;
    public Texture2D cursorTexture;
    public GameManager manager;

    private void OnMouseDrag()
    {
        switch (editionMode)
        {
            case EditionModes.POSTIION:
                {
                    Vector3 v3 = Input.mousePosition;
                    v3 = Camera.main.ScreenToWorldPoint(v3);
                    v3.x = Mathf.Clamp(v3.x, Constants.minPosition, Constants.maxPosition);
                    transform.position = new Vector3(v3.x, transform.position.y, transform.position.z);

                    float a = (Constants.maxPerspectiveSize - Constants.minPerspectiveSize) / (Constants.minPosition - Constants.maxPosition) * (v3.x - Constants.maxPosition) + Constants.minPerspectiveSize;
                    transform.parent.localScale = new Vector3(a, a, transform.parent.localScale.z);

                    // Valor posta de el planeta, este valor multiplicado por 695.700 km (radio del sol)
                    text.text = string.Format("{0:0} km", ((Constants.dataMaxDIstance - Constants.dataMinDistance) / (Constants.minPosition - Constants.maxPosition) * (transform.position.x - Constants.maxPosition) + Constants.dataMinDistance) * 695700);

                    break;
                }
            case EditionModes.TILT:
                {
                    Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                    diff.Normalize();
                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

                    // Inclinacion del eje de rotacion
                    text.text = string.Format("{0:0} degrees", transform.rotation.eulerAngles.z);
                    break;
                }
            case EditionModes.SIZE:
                {
                    Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                    float dist = Mathf.Max(Mathf.Abs(diff.x), Mathf.Abs(diff.y));
                    float a = dist / (Constants.maxSize - Constants.minSize);
                    a = Mathf.Clamp(a, Constants.minSize, Constants.maxSize);
                    transform.localScale = new Vector3(a, a, transform.localScale.z);

                    // Valor posta de el planeta, este valor multiplicado por 6.371km (radio de la tierra)
                    text.text = string.Format("{0:0} km", ((Constants.dataMaxSize - Constants.dataMinSize) / (Constants.maxSize - Constants.minSize) * (transform.localScale.x - Constants.minSize) + Constants.dataMinSize) * 6371);
                    break;
                }
        }

    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void GoToSimilar()
    {
        float radius = (Constants.dataMaxSize - Constants.dataMinSize) / (Constants.maxSize - Constants.minSize) * (transform.localScale.x - Constants.minSize) + Constants.dataMinSize;
        float distance = (Constants.dataMaxDIstance - Constants.dataMinDistance) / (Constants.minPosition - Constants.maxPosition) * (transform.position.x - Constants.maxPosition) + Constants.dataMinDistance;
        manager.GetSimilarPlanet(radius, distance);
    }

}
