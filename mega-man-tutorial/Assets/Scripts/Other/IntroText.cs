using System;
using UnityEngine;

[Serializable]
public class IntroText
{
    [SerializeField] private string text;
    [SerializeField] private float time;

    public IntroText() { }

    public IntroText(string text, float time)
    {
        Text = text;
        Time = time;
    }

    public string Text { get => text; set => text = value; }
    public float Time { get => time; set => time = value; }
}
