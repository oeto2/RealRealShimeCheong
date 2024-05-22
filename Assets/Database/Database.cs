using UnityEngine;

public class Database : MonoBehaviour
{
    private static ItemDB _item;
    private static ClueDB _clue;
    private static DialogueDB _dialogue;

    public static ItemDB Item
    {
        get
        {
            if (_item == null)
                _item = new ItemDB();

            return _item;
        }
    }

    public static ClueDB Clue
    {
        get
        {
            if (_clue == null)
                _clue = new ClueDB();

            return _clue;
        }
    }

    public static DialogueDB Dialogue
    {
        get
        {
            if (_dialogue == null)
                _dialogue = new DialogueDB();

            return _dialogue;
        }
    }
}

