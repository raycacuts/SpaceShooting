using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ProcessModel : AssetPostprocessor
{
   void OnPostprocessModel(GameObject input)
    {   
        if (input.name != "Enemy2b")
            return;


        ModelImporter importer = assetImporter as ModelImporter;

        Debug.Log( importer.assetPath);
        GameObject tar = (GameObject)AssetDatabase.LoadAssetAtPath(importer.assetPath, typeof(Object));
        if(tar == null)
        {
            Debug.Log("Can't find the Object");
        }
        else
        {
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(tar, "Assets/Prefabs/Enemy2c.prefab");
            Debug.Log("Import Succesful2");
            prefab.tag = "Enemy";


            foreach (Transform obj in prefab.GetComponentsInChildren<Transform>())
            {
                if (obj.name == "col")
                {
                    MeshRenderer r = obj.GetComponent<MeshRenderer>();
                    r.enabled = false;

                    if (obj.gameObject.GetComponent<MeshCollider>() == null)
                        obj.gameObject.AddComponent<MeshCollider>();

                    obj.tag = "Enemy";
                }
            }
            Debug.Log("Import Succesful3");
            Rigidbody rigid = prefab.AddComponent<Rigidbody>();
            rigid.useGravity = false;
            rigid.isKinematic = true;

            prefab.AddComponent<AudioSource>();

            GameObject rocket = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/EnemyRocket.prefab");

            GameObject fx = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/FX/Explosion.prefab");

            SuperEnemy enemy = prefab.AddComponent<SuperEnemy>();

            enemy.m_life = 50;
            enemy.m_point = 50;
            enemy.m_rocket = rocket.transform;
            enemy.m_explosionFX = fx.transform;
        }
        
    }
}
