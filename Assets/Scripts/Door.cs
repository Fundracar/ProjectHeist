using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractiveObject
{
    [SerializeField] private bool isMoving;
    [SerializeField] private Transform pivot;
    [SerializeField] private Collider _collider;

    private int _left = -90;
    private int _right = 90;
    
    
    private Transform actualRotation;

    private void Start()
    {
        actualRotation = pivot;
    }

    protected override void ActiveObject()
    {
        if (!isMoving)
        {
            isActive = !isActive;
            
            if (isActive)
                StartCoroutine(Rotate(_left));
            else if (!isActive)
                StartCoroutine(Rotate(_right));
        }

       /* if (needTool)
            needTool = false;*/
    }

    protected override IEnumerator GuardActiveObject()
    {
        if (!isActive && !isMoving)
        {
            yield return Rotate(_left);
            
            yield return new WaitForSeconds(0.5f);

            yield return Rotate(_right);
        }
        
        yield return null;
    }

    // Rotate the object around the pivot
    private IEnumerator Rotate(int sens)
    {
        isMoving = true;
        _collider.enabled = false;

        Quaternion newRotation = Quaternion.Euler(actualRotation.rotation.eulerAngles - new Vector3(0,sens,0));
        float t = 0;
      
        while (t <= 1)
        {
            t += Time.deltaTime;
            pivot.rotation = Quaternion.Lerp(actualRotation.rotation, newRotation, t);
            yield return null;
        }
      
        _collider.enabled = true;
        isMoving = false;
    }
}
