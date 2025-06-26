using UnityEngine;

public class TileController : MonoBehaviour
{
    public PotionController potion { get; private set; }
    public Vector2Int tileIndex { get; private set; }
    public Vector3 originalMousePos { get; set; } = Vector3.zero;
    public BoardController boardController { get; private set; }
    public IObstacle[] obstacles { get; set; }
    public IObstacle currentObstacle { get; private set; }

    private bool isSelected, isHiding, isDestroying, isDragging;

    private void Awake()
    {
        obstacles = GetComponentsInChildren<IObstacle>(true);
        foreach (var obstacle in obstacles)
            obstacle.RegisterAction(() => currentObstacle = null);
    }

    public void AssignObstacle(int index)
    {
        if (index >= 0)
        {
            currentObstacle = obstacles[index];
            currentObstacle.ActiveObstacle();
        }
    }

    public void DestroyCurrentObstacle()
    {
        currentObstacle.TakeDamage();
    }

    private void OnEnable()
    {
        isSelected = isHiding = isDestroying = isDragging = false;
    }

    private void Start()
    {
        boardController = BoardController.Instance;
    }

    private void OnMouseEnter()
    {
        if (currentObstacle == null)
            new TileMouseEnter().MouseEnter(potion, isHiding);
    }

    private void OnMouseExit()
    {
        if (currentObstacle == null)
            new TileMouseExit().MouseExit(potion, isHiding);
    }

    private void OnMouseDown()
    {
        if (currentObstacle == null)
            new TileMouseDown().MouseDown(potion, boardController, this, isSelected, isHiding,
            (mousePos) => originalMousePos = mousePos);
    }

    private void OnMouseDrag()
    {
        if (currentObstacle == null)
            new TileMouseDrag().MouseDrag(potion, boardController, originalMousePos, isHiding, isDragging,
            (dragging) => isDragging = dragging);
    }

    private void OnMouseUp()
    {
        if (currentObstacle == null)
            new TileMouseUp().MouseUp(potion,
            (value) =>
            {
                isSelected = value;
                isDragging = value;
            });
    }

    public void HidePotion()
    {
        new TileHidePotion().HidePotion(potion, isHiding,
            (value) => isHiding = value,
            () =>
            {
                isHiding = false;
                potion = null;
            });
    }

    public void SetTileIndex(Vector2Int index)
    {
        tileIndex = index;
    }

    public void AssignPotion(PotionController potion, bool changePos = true)
    {
        this.potion = potion;
        if (this.potion != null && changePos)
        {
            potion.transform.localPosition = transform.localPosition;
            potion.ResetScale();
            isDestroying = false;
        }
    }

    public void ClearTilePotion()
    {
        if (this.potion != null)
        {
            HidePotion();
        }
        this.potion = null;
    }

    public void ChangePotion(PotionController potion)
    {
        this.potion = potion;
        if (this.potion != null)
        {
            potion.Move(transform.localPosition);
            potion.ResetScale();
            isDestroying = false;
        }
    }

    public void AssignSpecialType(ESpecialType specialType)
    {
        new TileAssignSpecialPotion().AssignSpecial(specialType, potion, potion.potionType,
            (value) => isHiding = value,
            () => { isHiding = false; potion = null; },
            AssignPotion);
    }

    public void ActiveSpecial(Vector3? startPos = null, Vector3? endPos = null)
    {
        ScoreCalculator.Instance.UpdateScore(potion.BaseScore);
        if (!isDestroying && potion != null)
        {
            isDestroying = true;
            potion.ActiveSpecial(this, startPos, endPos);
        }
    }

#if UNITY_EDITOR
    public void AssignObstacleTool(int index)
    {
        ClearObstacleTool();
        currentObstacle = obstacles[index];
        currentObstacle.ActiveObstacle();
    }

    public void AssignPotionTool(PotionController potion)
    {
        if (this.potion != null)
        {
            this.potion.gameObject.SetActive(false);
            this.potion = null;
        }
        this.potion = potion;
        if (this.potion != null)
            potion.transform.localPosition = transform.localPosition;
    }

    public void ClearObstacleTool()
    {
        if (currentObstacle != null)
        {
            currentObstacle.HideObstacle();
            currentObstacle = null;
        }
    }
    public void ClearTileTool()
    {
        potion?.gameObject.SetActive(false);
        potion = null;

        if (currentObstacle != null)
        {
            currentObstacle.HideObstacle();
            currentObstacle = null;
        }
    }
#endif
}