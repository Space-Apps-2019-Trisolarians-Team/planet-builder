using UnityEngine;
using UnityEngine.UI;

public class StarConfiguration : MonoBehaviour
{
    public Slider ageSlider;
    public Slider massSlider;
    public Slider temperatureSlider;
    public Slider radiusSlider;
    public Text nameText;
    public Text typeText;

    public void SetStarValues(string name, float age, float mass, float temperature, float luminocity, float radius, string type)
    {
        nameText.text = name;
        typeText.text = type;
        ageSlider.value = age;
        massSlider.value = mass;
        temperatureSlider.value = temperature;
        radiusSlider.value = radius;
    }

    public void SetMinMaxValues(StarStatsResponse res)
    {
        ageSlider.minValue = res.st_age.min;
        ageSlider.maxValue = res.st_age.max;
        massSlider.minValue = res.st_mass.min;
        massSlider.maxValue = res.st_mass.max;
        temperatureSlider.minValue = res.st_teff.min;
        temperatureSlider.maxValue = res.st_teff.max;
        radiusSlider.minValue = res.st_rad.min;
        radiusSlider.maxValue = res.st_rad.max;
    }
}
