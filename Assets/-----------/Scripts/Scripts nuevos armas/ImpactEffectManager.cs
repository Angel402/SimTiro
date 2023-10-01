using UnityEngine;

public class ImpactEffectManager : MonoBehaviour
{
		
    public ImpactInfo[] ImpactElemets = new ImpactInfo[0];
		
    [System.Serializable]
    public class ImpactInfo
    {
        public MaterialType.MaterialTypeEnum MaterialType;
        public GameObject ImpactEffect;
    }
		
    private static ImpactEffectManager _instance;

    public static ImpactEffectManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<ImpactEffectManager>();
            return _instance;
        }
    }
		
    public	GameObject GetImpactEffect(GameObject impactedGameObject)
    {
        var materialType = impactedGameObject.GetComponent<MaterialType>();
        if (materialType==null)
            return null;
        foreach (var impactInfo in ImpactElemets)
        {
            if (impactInfo.MaterialType==materialType.TypeOfMaterial)
                return impactInfo.ImpactEffect;
        }
        return null;
    }
}