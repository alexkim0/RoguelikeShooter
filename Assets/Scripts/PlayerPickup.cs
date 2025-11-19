using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	void OnTriggerEnter(Collider other)
	{
        if (other.CompareTag("Item")) {
            GeneralItem item = other.gameObject.GetComponent<GeneralItem>();
            item.giveItem();
			Destroy(other.gameObject);
		}
	}    
}
