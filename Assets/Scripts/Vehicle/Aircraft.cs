using DG.Tweening;
using UnityEngine;

public class Aircraft :Vehicle
{
    [SerializeField] private float yHeight;
    [SerializeField] DropStash _dropStash;
    [SerializeField] private ObjectRotater propeller;
    
    private Vector3 startPos;
    protected override void EnterVehicle(GameObject playerObj)
    {
        player = playerObj;
        startPos = transform.position;
        Quaternion startRot = transform.rotation;
        playerObj.SetActive(false);
        cameraController.TargetTransform = movement.transform;
        Vector3 target = transform.forward * 3;
        target.y = yHeight;
        propeller.enabled = true;
        transform.DOLookAt(target, .2f);
        transform.DOMove(target, 1f)
            .OnComplete(() =>
            {
                transform.DORotateQuaternion(startRot, .2f).OnComplete(() =>
                {
                    leaveButton.onClick.RemoveAllListeners();
                    leaveButton.onClick.AddListener(Leave);
                    leaveButton.gameObject.SetActive(true);
                    movement.enabled = true;
                    collector.enabled = true;
                });
            });
    }

    protected override void Leave()
    {
        Quaternion startRot = transform.rotation;
        
        transform.DOLookAt(startPos, .3f);
        transform.DOMove(startPos, 1.5f)
            .OnComplete(()=>
            {
                transform.DORotateQuaternion(startRot, .3f).OnComplete(() =>
                {
                    propeller.enabled = false;
                    base.Leave();
                    _dropStash.DropAllStash();
                });
            });
    }
}