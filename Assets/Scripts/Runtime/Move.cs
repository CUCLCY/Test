using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public enum EaseType
    {
        None,
        EaseIn,
        EaseOut,
        EaseInOut,
    }
    
    public Vector3 begin;
    public Vector3 end;
    public float time;
    public bool pingpong;
    public EaseType easeType;
    private Coroutine m_Coroutine;
    public void move()
    {
        if (m_Coroutine!=null)
        {
            StopCoroutine(m_Coroutine);
        }
        m_Coroutine = StartCoroutine(doMove(gameObject, begin, end, time, pingpong, easeType));
    }

    private IEnumerator doMove(GameObject gameObject, Vector3 begin, Vector3 end, float time, bool pingpong, EaseType easeType)
    {
        var trans = gameObject.transform;
        trans.position = begin;
        var t = 0f;
        var sign = 1;
        while (true)
        {
            var v = doEase(t / time, easeType);
            trans.position = Vector3.Lerp(begin, end, v);
            yield return null;
            t += sign * Time.deltaTime;
            if (t >= time && !pingpong)
            {
                yield break;
            }

            if (t>= time && pingpong)
            {
                sign = -1;
            }

            if (t<=0 && pingpong)
            {
                sign = 1;
            }
        }
    }

    private float doEase(float t, EaseType easeType)
    {
        switch (easeType)
        {
            case EaseType.None:
                return t;
            case EaseType.EaseIn:
                return Mathf.Pow(t, 2);
            case EaseType.EaseOut:
                return 1 - Mathf.Pow(1 - t, 2);
            case EaseType.EaseInOut:
                return (t < 0.5f) ? 2 * Mathf.Pow(t, 2) : 1 - Mathf.Pow(-2 * t + 2, 2) / 2;
        }

        return t;
    }
}
