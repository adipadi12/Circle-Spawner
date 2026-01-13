using System.Collections.Generic;
using UnityEngine;

public interface IClientService
{
    System.Threading.Tasks.Task<List<MergedClientData>> GetClientsAsync();
}
