using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    [SerializeField] GameObject weaponParent;

    public void GameOver()
    {
        Debug.Log("Game Over");
        gameOverPanel.SetActive(true);
        weaponParent.SetActive(false);
    }
}
