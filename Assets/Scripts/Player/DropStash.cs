using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stash))]
public class DropStash : MonoBehaviour
{
    public Collectable collectablePrefab;
    public Transform collectableDropPosition;
    public Stash stash;
    public void DropAllStash()
    {
        for (int i = stash.CollectedCount - 1; i >= 0; i--)
        {
            Stashable removed = stash.RemoveStash();
            Destroy(removed.gameObject);
            Collectable collectable = Instantiate(collectablePrefab, null);
            collectable.transform.position = stash.stashParent.transform.position;
            collectable.transform.localScale = Vector3.zero;
            collectable.transform.DOMove(collectableDropPosition.position, .5f);
            collectable.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack, 2.5f);
        }
    }
}
