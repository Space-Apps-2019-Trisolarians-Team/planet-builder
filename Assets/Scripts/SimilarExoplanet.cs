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
    public string link;

    public void SetInfo(string name, string starName, float starSize, float distanceToStar, float size, bool habitability, string link)
    {
        image.sprite = concepts[Random.Range(0, concepts.Count)];
        planetName1Text.text = name;
        planetName2Text.text = name;
        starNameText.text = starName != "" ? starName : "Not available";
        starSizeText.text = starSize != 0 ? starSize.ToString() + " Suns" : "Not available";
        distanceToStarText.text = distanceToStar != 0 ? (distanceToStar * 695700).ToString() + " Km" : "Not available";
        sizeText.text = size != 0 ? (size * 6371).ToString() + " Km" : "Not available";
        habitabilityText.text = habitability ? "Yes" : "No";
        this.link = link;
    }

    public void GoToMoreInfo()
    {
        Application.ExternalEval(string.Format("window.open(\"{0}\")", link));
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }

}
