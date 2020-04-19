using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsDisplay : MonoBehaviour
{
    [SerializeField]
    private Text _averageLabel, _highestLabel, _lowestLabel;

    [SerializeField]
    private FPSColor[] _fpsColors;

    private FpsCounter _fpsCounter;

    private string[] _stringsFrom00To99 = {
        "00", "01", "02", "03", "04", "05", "06", "07", "08", "09",
        "10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
        "20", "21", "22", "23", "24", "25", "26", "27", "28", "29",
        "30", "31", "32", "33", "34", "35", "36", "37", "38", "39",
        "40", "41", "42", "43", "44", "45", "46", "47", "48", "49",
        "50", "51", "52", "53", "54", "55", "56", "57", "58", "59",
        "60", "61", "62", "63", "64", "65", "66", "67", "68", "69",
        "70", "71", "72", "73", "74", "75", "76", "77", "78", "79",
        "80", "81", "82", "83", "84", "85", "86", "87", "88", "89",
        "90", "91", "92", "93", "94", "95", "96", "97", "98", "99"
    };

    private void Awake()
    {
        _fpsCounter = GetComponent<FpsCounter>();
    }

    private void Update()
    {
        Display(_averageLabel, _fpsCounter.AverageFps);
        Display(_highestLabel, _fpsCounter.HighestFps);
        Display(_lowestLabel, _fpsCounter.LowerFps);
    }

    private void Display(Text label, int fps)
    {
        label.text = _stringsFrom00To99[Mathf.Clamp(fps, 0, 99)];
        for (int i = 0; i < _fpsColors.Length; i++)
        {
            if (fps >= _fpsColors[i].MinFPS)
            {
                label.color = _fpsColors[i].Color;
                break;
            }
        }
    }

    [Serializable]
    private struct FPSColor
    {
        public Color Color;
        public int MinFPS;
    }
}
