using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ClientApiResponse
{
    public List<ClientListItem> clients;
    public SerializableDictionary<string, ClientDetails> data;
    public string label;
}
