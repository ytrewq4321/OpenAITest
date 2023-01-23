using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CompetitionResponse
{
    public string id;

    public string @object;
    public int created;
    public string model;

    public List<Choise> choices;

    public Usage usage;
}

[System.Serializable]
public class Choise
{
    public string text;
    public int index;
    public object logprobs;
    public string finish_reason;
}

[System.Serializable]
public class Usage
{
    public int prompt_tokens;
    public int completion_tokens;
    public int total_tokens;
}
