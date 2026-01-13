using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System.Collections.Generic;

public class ClientApiService : IClientService
{
    private const string URL =
        "https://qa.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";

    public async Task<List<MergedClientData>> GetClientsAsync()
    {
        using var request = UnityWebRequest.Get(URL);
        await request.SendWebRequest();

        while (!request.isDone)
            await Task.Yield();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
            return new List<MergedClientData>();
        }

        var response =
            JsonUtility.FromJson<ClientApiResponse>(request.downloadHandler.text);

        return MergeData(response);
    }

    private List<MergedClientData> MergeData(ClientApiResponse response)
    {
        var result = new List<MergedClientData>();

        foreach (var client in response.clients)
        {
            var key = client.id.ToString();

            if (!response.data.ContainsKey(key))
                continue;

            var details = response.data[key];

            result.Add(new MergedClientData
            {
                id = client.id,
                label = client.label,
                isManager = client.isManager,
                name = details.name,
                points = details.points,
                address = details.address
            });
        }

        return result;
    }
}
