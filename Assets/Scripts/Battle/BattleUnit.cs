using System.Collections;
using System.Collections.Generic;
// using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] PokemonBase _base;
    [SerializeField] int level;
    [SerializeField] bool isPlayerUnit;

    public Pokemon Pokemon{get; set;}

    Image image;
    Vector3 originalPos;
    Color originalColor;
    private void Awake(){
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition; // relative position in the canvas(not the world)
        originalColor = image.color;
    }
    public void Setup(){
        Pokemon = new Pokemon(_base, level);
        if (isPlayerUnit)
            image.sprite = Pokemon.Base.BackSprite;        
        else
            image.sprite = Pokemon.Base.FrontSprite;

        image.color = originalColor;
        PlayerEnterAnimation();
    }

    public void PlayerEnterAnimation(){
        if (isPlayerUnit)
            image.transform.localPosition = new Vector3(-500f, originalPos.y);
        else
            image.transform.localPosition = new Vector3(500f,originalPos.y);

        image.transform.DOLocalMoveX(originalPos.x,1f);
    }

    public void PlayAttackAnimation(){
        var sequence = DOTween.Sequence();
        if (isPlayerUnit)
            sequence.Append(image.transform.DOLocalMoveX(originalPos.x + 50f, 0.25f));
        else
            sequence.Append(image.transform.DOLocalMoveX(originalPos.x - 50f, 0.25f));
        sequence.Append(image.transform.DOLocalMoveX(originalPos.x, 0.25f));
    }

    public void PlayerHitAnimation(){
        var sequence = DOTween.Sequence();
        sequence.Append(image.DOColor(Color.gray, 0.1f));
        sequence.Append(image.DOColor(originalColor, 0.1f));
    }

    public void PlayerFaintAnimation(){
        var sequence = DOTween.Sequence();
        sequence.Append(image.transform.DOLocalMoveY(originalPos.y-150f, 0.5f));
        sequence.Join(image.DOFade(0f,0.5f));
    }
}
