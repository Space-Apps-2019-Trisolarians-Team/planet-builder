using UnityEngine;

public class Planet : MonoBehaviour
{
    public enum EditionModes { POSTIION, SIZE, TILT }
    public EditionModes editionMode;
    public Sprite image;

    private SpriteRenderer spriteRenderer;
    private float scaleFactor;

    private float size = .5f;
    private float position = .5f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = image;
        editionMode = EditionModes.SIZE;
        scaleFactor = .9f / (Constants.maxPosition - Constants.minPosition);
    }

    private void OnMouseDrag()
    {
        switch (editionMode) {
            case EditionModes.POSTIION:
                {
                    Vector3 v3 = Input.mousePosition;
                    v3 = Camera.main.ScreenToWorldPoint(v3);
                    v3.x = Mathf.Clamp(v3.x, Constants.minPosition, Constants.maxPosition);
                    position = 1 - ((v3.x - Constants.minPosition) / (Constants.maxPosition - Constants.minPosition));
                    Debug.Log(position);
                    break;
                }
            case EditionModes.TILT:
                {
                    Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                    diff.Normalize();
                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                    break;
                }
            case EditionModes.SIZE:
                {
                    Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                    float dist = Mathf.Max(Mathf.Abs(diff.x), Mathf.Abs(diff.y));
                    size = dist / (Constants.maxSize - Constants.minSize);
                    Debug.Log(size);
                    break;
                }
        }
        
    }

    private void Update()
    {
        float s = Mathf.Clamp((Constants.maxSize - Constants.minSize) * size, Constants.minSize, Constants.maxSize) * position + Constants.minSize;
        transform.localScale = new Vector3(s, s, transform.localScale.z);
        transform.position = new Vector3((Constants.maxPosition - Constants.minPosition) * (1 - position) + Constants.minPosition, transform.position.y, transform.position.z);
    }

}
