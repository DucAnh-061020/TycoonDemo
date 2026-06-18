using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public static GameDirector Instance { get; private set; }

    [Header("References")]
    [SerializeField] private Market market;

    [Header("Prefabs")]
    [SerializeField] private Transform treePrefab;
    [SerializeField] private Transform delivererPrefab;
    [SerializeField] private Transform customerPrefab;

    [Header("Settings")]
    [SerializeField] private float customerSpawnInterval = 1f;

    // Core State Tracking
    private int maxCustomers = 1; // Starts at 1 as per your prompt
    private int currentCustomerCount = 0;
    private List<Tree> unlockedTrees = new List<Tree>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void OnEnable()
    {
        UnlockableSlot.OnTreeUnlocked += HandleTreeUnlocked;
        EventsBroker.OnMaxBuyersIncreased += HandleMaxBuyersIncreased;
    }

    private void OnDisable()
    {
        UnlockableSlot.OnTreeUnlocked -= HandleTreeUnlocked;
        EventsBroker.OnMaxBuyersIncreased -= HandleMaxBuyersIncreased;
    }

    private void Start()
    {
        StartCoroutine(CustomerSpawnRoutine());
    }

    private System.Collections.IEnumerator CustomerSpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(customerSpawnInterval);

            if (currentCustomerCount < maxCustomers)
            {
                SpawnCustomer();
            }
        }
    }

    private void SpawnCustomer()
    {
        currentCustomerCount++;
        if (customerPrefab.TryGetComponent(out CustomerAI customerAI))
        {
            GameObject customerObj = PoolManager.Instance.Spawn(customerPrefab.gameObject, market.EntryPoint.position, Quaternion.identity, customerAI.PoolIndex);
            CustomerAI customer = customerObj.GetComponent<CustomerAI>();
            customer.InitializeFromPool(market);
        }
    }

    public void NotifyCustomerLeft()
    {
        currentCustomerCount = Mathf.Max(0, currentCustomerCount - 1);
    }

    private void HandleTreeUnlocked(TreeData data, Transform spawnTransform)
    {
        if (!treePrefab.TryGetComponent(out Tree tree))
        {
            return;
        }
        GameObject treeObj = PoolManager.Instance.Spawn(treePrefab.gameObject, spawnTransform.position, Quaternion.identity, tree.PoolIndex);
        Tree newTree = treeObj.GetComponent<Tree>();
        newTree.Initialize(data);
        unlockedTrees.Add(newTree);
        if (!delivererPrefab.TryGetComponent(out DelivererAI deliverer))
        {
            return;
        }
        GameObject delivererObj = PoolManager.Instance.Spawn(delivererPrefab.gameObject, market.EntryPoint.position, Quaternion.identity, deliverer.PoolIndex);
        DelivererAI newDeliverer = delivererObj.GetComponent<DelivererAI>();
        newDeliverer.InitializeFromPool(market, newTree);
    }

    public void NotifyDelivererRetired(Tree associatedTree)
    {
        if (!delivererPrefab.TryGetComponent(out DelivererAI deliverer))
        {
            return;
        }
        GameObject delivererObj = PoolManager.Instance.Spawn(delivererPrefab.gameObject, market.EntryPoint.position, Quaternion.identity, deliverer.PoolIndex);
        DelivererAI newDeliverer = delivererObj.GetComponent<DelivererAI>();

        newDeliverer.InitializeFromPool(market, associatedTree);
    }

    private void HandleMaxBuyersIncreased(int additionalBuyers)
    {
        maxCustomers += additionalBuyers;
    }
}