using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject parentCapsule;
    public Vector3 startPosition;
    public float startXoffset;
    public GameObject mainCamera;

    List<GameObject> capsuleList = new List<GameObject>();
    GameObject currentCapsule;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<5; i++)
        {
            GameObject newObject = Instantiate(parentCapsule);
            newObject.name = "capsule_" + i;
            newObject.transform.position = new Vector3(startPosition.x + i * startXoffset, startPosition.y, startPosition.z);

            capsuleList.Add(newObject);
        }

        FollowCam camScript = mainCamera.GetComponent<FollowCam>();
        camScript.currentTarget = capsuleList[0].transform;
        camScript.LookAtThisOne(capsuleList[0].transform);
        selectCapsule(capsuleList[0]);
    }

    void selectCapsule(GameObject obj)
    {
        CapsuleController capsuleScript;
        foreach (GameObject capsule in capsuleList)
        {
            capsuleScript = capsule.GetComponent<CapsuleController>();
            capsuleScript.isSelected = false;
        }
        currentCapsule = obj;
        FollowCam camScript = mainCamera.GetComponent<FollowCam>();
        camScript.target = currentCapsule.transform;
        camScript.currentTarget = currentCapsule.transform;
        camScript.nextTarget = obj.transform;
        capsuleScript = currentCapsule.GetComponent<CapsuleController>();
        capsuleScript.isSelected = true;
        camScript.transitionProgress = 0f;

       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                GameObject selectedObject = hit.collider.gameObject;
                Debug.Log("Select capsule: " + selectedObject.name);
                selectCapsule(selectedObject);
            }
        }
    }
}
