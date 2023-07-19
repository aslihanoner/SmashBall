using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image LevelSlider;
    public Image CurrentLevelImage;
    public Image NextLevelImage;
    public Material PlayerMaterial;
    void Start()
    {
        PlayerMaterial = FindObjectOfType<PlayerController>().transform.GetChild(0).GetComponent<MeshRenderer>().material;

        LevelSlider.transform.GetComponent<Image>().color = PlayerMaterial.color + Color.gray;
        LevelSlider.color = PlayerMaterial.color;
        CurrentLevelImage.color = PlayerMaterial.color;
        NextLevelImage.color = PlayerMaterial.color;
    }

    public void LevelSliderFill(float fillAmount)
    {
        LevelSlider.fillAmount = fillAmount;
    }
}
