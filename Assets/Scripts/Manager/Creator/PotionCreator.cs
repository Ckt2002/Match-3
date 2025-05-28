using DG.Tweening;
using UnityEngine;

public class PotionCreator : MonoBehaviour, ICreator
{
    public Transform potionContainer;
    public GameObject[] potionPrefabs;

    public float animationShowTime;
    public Ease animationShowEase;

    public void Create(int width, int height, float spacing, Vector3 spawnPosOffset, BoardController boardController)
    {
        BoardGrid board = boardController.boardGrid;
        board.InitPotionGrid(width, height);
        Sequence sequence = DOTween.Sequence();

        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                Vector3 tilePos = new Vector3(w * spacing + spawnPosOffset.x, h * spacing + spawnPosOffset.y, 0f);
                int index = Random.Range(0, potionPrefabs.Length);
                GameObject potionToSpawn = potionPrefabs[index];
                GameObject potion = Instantiate(potionToSpawn, potionContainer);
                Vector3 scale = potion.transform.localScale;
                potion.transform.position = tilePos;
                potion.transform.localScale = Vector3.zero;
                sequence.Join(
                    potion.transform.DOScale(scale, animationShowTime)
                    .SetEase(animationShowEase)
                );

                board.SetPotionGrid(potion, w, h);
            }
        }
    }
}
