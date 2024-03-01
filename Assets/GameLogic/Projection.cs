using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public class Projection : MonoBehaviour
{
    [SerializeField] private Transform _obstaclesParent;
    [SerializeField] private int _maxPhysicsFrameIterations = 100;
    [SerializeField] private LineRenderer _line;
    public Bullet ghostObj;
    private ObjectPool<Bullet> bulletPool;
    private Scene _simulationScene;
    private PhysicsScene _physicsScene;
    private readonly Dictionary<Transform, Transform> _spawnedObjects = new Dictionary<Transform, Transform>();
    private void Start()
    {
        /*
        bulletPool = new ObjectPool<Bullet>(() =>
        {
    
            return Instantiate(ghostObj);
            
        }, bullet =>
        {
            bullet.gameObject.SetActive(true);
        }, bullet =>
        {
            bullet.gameObject.SetActive(false);
        }, bullet =>
        {
            Destroy(bullet.gameObject);
        }, true, 15, 20);
        */

        CreatePhysicsScene();

        //for (int i = 0; i < 15; i++)
        //{
            //var ghostObj2 = bulletPool.Get();
            //SceneManager.MoveGameObjectToScene(ghostObj2.gameObject, _simulationScene);
            //bulletPool.Release(ghostObj2);
        //}
    }
    private void CreatePhysicsScene()
    {
        _simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulationScene.GetPhysicsScene();

        foreach (Transform obj in _obstaclesParent)
        {
            var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            ghostObj.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);
            if (!ghostObj.isStatic) _spawnedObjects.Add(obj, ghostObj.transform);
        }
    }
    private void Update()
    {
        foreach (var item in _spawnedObjects)
        {
            item.Value.position = item.Key.position;
            item.Value.rotation = item.Key.rotation;
        }
    }

    public void SimulateTrajectory(Bullet bulletPrefab, Vector3 pos, Vector3 velocity)
    {
        //var ghostObj1 = bulletPool.Get();
        //ghostObj1.transform.position = pos;
        //ghostObj1.transform.rotation = Quaternion.identity;
        var ghostObj = Instantiate(bulletPrefab, pos, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simulationScene);

        ghostObj.Init(velocity, true);

        _line.positionCount = _maxPhysicsFrameIterations;

        for (var i = 0; i < _maxPhysicsFrameIterations; i++)
        {
            _physicsScene.Simulate(Time.fixedDeltaTime);
            _line.SetPosition(i, ghostObj.transform.position);
        }
        //ghostObj.transform.position = pos;
        //ghostObj1.transform.position = pos;
        //Invoke("Release", .2f);

        //bulletPool.Release(ghostObj1);
        Destroy(ghostObj.gameObject);
    }

    //public void Release()
    //{
    //    bulletPool.Release(ghostObj);
    //}

    public void Initialize(Bullet bulletPrefab, Vector3 pos, Vector3 velocity)
    {
        ghostObj = Instantiate(bulletPrefab, pos, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simulationScene);
        ghostObj.Init(velocity, true);

        _line.positionCount = _maxPhysicsFrameIterations;

        for (var i = 0; i < _maxPhysicsFrameIterations; i++)
        {
            _physicsScene.Simulate(Time.fixedDeltaTime);
            _line.SetPosition(i, ghostObj.transform.position);
        }

        ghostObj.transform.position = pos;
    }


}
