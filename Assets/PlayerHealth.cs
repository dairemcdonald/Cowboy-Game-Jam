using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int hp = 2;
    public GameObject gameOverText;
    private GameObject _gameoverText;
    public Canvas Canvas;

    public void carriageDown()
    {
        hp--;
        if (hp <= 0)
        {
            _gameoverText = Instantiate(gameOverText, transform.position, Quaternion.identity);
            _gameoverText.GetComponent<RectTransform>().SetParent(Canvas.GetComponent<RectTransform>(), false);

        }
    }


}
