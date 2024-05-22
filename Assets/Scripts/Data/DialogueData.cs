using Constant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueData
{
    [SerializeField] private int _id;
    [SerializeField] private SpeakerType _speakerType;
    [SerializeField] private string _name;
    [SerializeField] private string _comment;
    [SerializeField] private bool _isUsing;
    [SerializeField] private int _indexNum;

    public int Id => _id;
    public SpeakerType SpeakerType => _speakerType;
    public string Name => _name;
    public string Comment => _comment;
    public bool IsUsing => _isUsing;
    public int IndexNum => _indexNum;
}
