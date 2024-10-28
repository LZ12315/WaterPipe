using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Line : MonoBehaviour
{

    private LineRenderer lineRenderer;

    [Header("分割线属性")]
    public float maxWidth = 0.1f;
    private float nowWidth;
    private Vector2 startPos;
    private Vector2 endPos;

    [Header("线条变化")]
    public float duration;
    private float timeElapsed;
    private LineAnimeState lineState;

    private void OnEnable()
    {
        if (this.GetComponent<LineRenderer>())
            lineRenderer = this.GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        LineAnime();
    }

    public void LineInit(Vector2 startPos, Vector2 endPos)
    {
        this.startPos = startPos;
        this.endPos = endPos;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = maxWidth;
        lineRenderer.endWidth = maxWidth;
    }

    public void DrawLine()
    {
        if (lineRenderer == null)
            return;
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    public void ChangeLineAnimeState(LineAnimeState state)
    {
        lineState = state;
    }

    private void LineAnime()
    {
        if (lineState == LineAnimeState.Appear)
            LineFlexAnime(nowWidth, maxWidth);
        if (lineState == LineAnimeState.DisAppear)
            LineFlexAnime(nowWidth, 0);
    }

    private void LineFlexAnime(float width, float targetWidth)
    {
        timeElapsed += Time.fixedDeltaTime;
        float t = timeElapsed / duration;
        t = Mathf.Clamp01(t);
        t = Mathf.SmoothStep(0f, 1f, t);

        nowWidth = Mathf.Lerp(width, targetWidth, t);
        lineRenderer.startWidth = nowWidth;
        lineRenderer.endWidth = nowWidth;

        if (timeElapsed >= duration)
        {
            nowWidth = targetWidth;
            lineRenderer.startWidth = nowWidth;
            lineRenderer.endWidth = nowWidth;

            lineState = LineAnimeState.None;
            timeElapsed = 0f;
        }
    }
}
