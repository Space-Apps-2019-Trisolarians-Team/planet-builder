using UnityEngine;
using System.Collections.Generic;

public class PlanetSprites : MonoBehaviour
{
    public List<Sprite> planets;
    public List<Sprite> rocks;
    public List<Sprite> crates;
    public Sprite clouds;

    public SpriteRenderer planetRenderer;
    public SpriteRenderer rocksRenderer;
    public SpriteRenderer cratesRenderer;
    public SpriteRenderer cloudsRenderer;

    int randomRock;
    int randomCrates;

    private void Start()
    {
        randomRock = Random.Range(0, rocks.Count);
        randomCrates = Random.Range(0, crates.Count);
        planetRenderer.sprite = planets[Random.Range(0, planets.Count)];
    }

    private void Update()
    {
        float pos = transform.position.x;
        float size = transform.localScale.x;
        float posBySize = pos * size;
        
        if (pos > Constants.minTransitionRock)
        {
            rocksRenderer.sprite = rocks[randomRock];
            rocksRenderer.color = new Color(1, 1, 1, pos / 3 + 2);
        }
        else
        {
            rocksRenderer.sprite = null;
        }
        if (posBySize > -5)
        {
            if (posBySize < -2)
            {
                float transparency;
                if (posBySize < -3)
                {
                    transparency = posBySize / 2 + 2.5f;
                }
                else
                {
                    // Debug.Log(string.Format("{0} {1}", posBySize, 3 - posBySize));
                    transparency = -posBySize -2;
                }
                cloudsRenderer.sprite = clouds;
                cloudsRenderer.color = new Color(1, 1, 1, transparency);
            }
            else
            {
                cloudsRenderer.sprite = null;
                cratesRenderer.sprite = crates[randomCrates];
                cratesRenderer.color = new Color(1, 1, 1, posBySize / 2 + 1);
            }
        }
        else
        {
            cratesRenderer.sprite = null;
            cloudsRenderer.sprite = null;
        }
    }
}
