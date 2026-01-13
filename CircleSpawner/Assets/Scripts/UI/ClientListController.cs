using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ClientListController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform contentRoot;
    [SerializeField] private ClientListItemView itemPrefab;
    [SerializeField] private TMP_Dropdown filterDropdown;
    [SerializeField] private ClientPopupView popupView;

    private IClientService clientService;
    private List<MergedClientData> allClients = new();

    private async void Start()
    {
        Debug.Log("ClientListController started.");

        clientService = new ClientApiService();
        await LoadClients();

        Debug.Log($"Loaded {allClients.Count} clients.");
        
        filterDropdown.onValueChanged.AddListener(OnFilterChanged);
        RefreshList();
    }

    private async Task LoadClients()
    {
        allClients = await clientService.GetClientsAsync();
    }

    private void RefreshList()
    {
        foreach (Transform child in contentRoot)
            Destroy(child.gameObject);

        foreach (var client in GetFilteredClients())
        {
            var item = Instantiate(itemPrefab, contentRoot);
            item.Bind(client, OnClientClicked);
        }
    }

    private List<MergedClientData> GetFilteredClients()
    {
        return filterDropdown.value switch
        {
            1 => allClients.FindAll(c => c.isManager),
            2 => allClients.FindAll(c => !c.isManager),
            _ => allClients
        };
    }

    private void OnFilterChanged(int _)
    {
        RefreshList();
    }

    private void OnClientClicked(MergedClientData data)
    {
        popupView.Show(data);
    }
}
