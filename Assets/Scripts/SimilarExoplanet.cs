using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimilarExoplanet : MonoBehaviour
{
    public List<Sprite> concepts;
    public Image image;

    public Text planetName1Text;
    public Text planetName2Text;
    public Text starNameText;
    public Text starSizeText;
    public Text distanceToStarText;
    public Text sizeText;
    public Text habitabilityText;

    public void SetInfo(string name, string starName, float starSize, float distanceToStar, float size, bool habitability)
    {
        image.sprite = concepts[Random.Range(0, concepts.Count)];
        planetName1Text.text = name;
        planetName2Text.text = name;
        starNameText.text = starName != "" ? starName : "Not available";
        starSizeText.text = starSize != 0 ? starSize.ToString() : "Not available";
        distanceToStarText.text = distanceToStar != 0 ? distanceToStar.ToString() : "Not available";
        sizeText.text = size != 0 ? size.ToString() : "Not available";
        habitabilityText.text = habitability ? "Yes" : "No";
    }

    public void GoToMoreInfo()
    {
        // Application.ExternalEval("window.open(\"http://www.unity3d.com\")");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }

}
