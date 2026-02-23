using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotRotator : MonoBehaviour
{
    [Header("Input Point")]
    public List<Transform> slots = new List<Transform>();

    [Header("Input Object")]
    public List<Transform> items = new List<Transform>();

    public float moveTime = 0.25f;
    bool isMoving;

    void Start()
    {
        ApplyPositionInstant();
    }

    // ปุ่มซ้าย
    public void RotateLeft()
    {
        if (isMoving) return;

        Transform first = items[0];
        items.RemoveAt(0);
        items.Add(first);

        StartCoroutine(MoveToSlots());
    }

    // ปุ่มขวา
    public void RotateRight()
    {
        if (isMoving) return;

        Transform last = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        items.Insert(0, last);

        StartCoroutine(MoveToSlots());
    }

    void ApplyPositionInstant()
    {
        for (int i = 0; i < items.Count; i++)
            items[i].position = slots[i].position;
    }

    IEnumerator MoveToSlots()
    {
        isMoving = true;

        Vector3[] startPos = new Vector3[items.Count];
        Vector3[] targetPos = new Vector3[items.Count];

        for (int i = 0; i < items.Count; i++)
        {
            startPos[i] = items[i].position;
            targetPos[i] = slots[i].position;
        }

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / moveTime;

            for (int i = 0; i < items.Count; i++)
            {
                items[i].position = Vector3.Lerp(
                    startPos[i],
                    targetPos[i],
                    Mathf.SmoothStep(0, 1, t)
                );
            }

            yield return null;
        }

        // กันคลาด
        for (int i = 0; i < items.Count; i++)
            items[i].position = targetPos[i];

        isMoving = false;
    }
}