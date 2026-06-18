using UnityEngine;

public class Gatherer : MonoBehaviour
{
    private Tree targetTree;
    private IStorageLocation targetMarket;
    private int inventory;

    public void Setup(Tree tree, IStorageLocation market)
    {
        this.targetTree = tree;
        this.targetMarket = market;
        StartCoroutine(GatheringLoop());
    }

    private System.Collections.IEnumerator GatheringLoop()
    {
        while (true)
        {
            // Walk to Tree (Using NavMesh)
            yield return MoveTo(targetTree.transform.position);
            inventory = targetTree.HarvestFruits();

            if (inventory > 0)
            {
                // Walk to Market
                yield return MoveTo(targetMarket.WaitingPoint.position);
                targetMarket.DepositFruits(inventory);
                inventory = 0;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private System.Collections.IEnumerator MoveTo(Vector3 position) { /* Navigation logic */ yield return null; }
}