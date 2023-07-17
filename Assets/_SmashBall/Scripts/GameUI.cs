using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image levelSlider;
    public Image currentLevelImage;
    public Image nextLevelImage;

    public Material playerMaterial;


    void Start()
    {
        playerMaterial = FindObjectOfType<PlayerController>().transform.GetChild(0).GetComponent<MeshRenderer>().material;

        levelSlider.transform.GetComponent<Image>().color = playerMaterial.color + Color.gray;
        levelSlider.color = playerMaterial.color;
        currentLevelImage.color = playerMaterial.color;
        nextLevelImage.color = playerMaterial.color;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void levelSliderFill(float fillAmount)
    {
        levelSlider.fillAmount = fillAmount;
    }
}
