using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;

	
	public float width=10f;
	public float height=5f;
	public float speed=15f;
	public float spawnDelay=0.5f;
	
	private float xmax;
	private float xmin;
	
	private bool rightMove=true;
	
	// Use this for initialization
	void Start () {

		float distance= transform.position.z-Camera.main.transform.position.z;
		Vector3 leftmost= Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightmost= Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		xmin=leftmost.x;
		xmax=rightmost.x;
		SpawnUntilFull();
	}
	
	void SpawnEnemies(){
		foreach(Transform child in transform){
			GameObject enemy= Instantiate(enemyPrefab,child.transform.position,Quaternion.identity) as GameObject;
			enemy.transform.parent=child;
		}
	}
	
	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition();
		if(freePosition){
		GameObject enemy= Instantiate(enemyPrefab,freePosition.position,Quaternion.identity) as GameObject;
		enemy.transform.parent=freePosition;
		
		}
		if(NextFreePosition()){
			Invoke("SpawnUntilFull",spawnDelay);
			
		}
		
	}
	
	 public void OnDrawGizmos(){
	 	Gizmos.DrawWireCube(transform.position,new Vector3(width,height));
	 }
	
	// Update is called once per frame
	void Update () {
		if(rightMove){
			transform.position +=Vector3.right*speed*Time.deltaTime;
		}else{
			transform.position +=Vector3.left*speed*Time.deltaTime;
		}
		float rightEdgeOfFormation = transform.position.x+(0.5f*width);
		float leftEdgeOfFormation = transform.position.x-(0.5f*width);
		if(leftEdgeOfFormation<xmin){
			rightMove= true;
		}else if(rightEdgeOfFormation > xmax){
			rightMove=false;
		}
		
		if(AllMembersDead()){
		//Debug.Log("KILL EVERYTHING");
			SpawnUntilFull();
		}
		
	}
	
	Transform NextFreePosition(){
		foreach(Transform childPositionGameObject in transform){
			if(childPositionGameObject.childCount==0){						
				return childPositionGameObject;
			}
		}
		return null;
	}
	
	
	bool AllMembersDead(){
		foreach(Transform childPositionGameObject in transform){
			if(childPositionGameObject.childCount>0){
			return false;
			}
		}
		return true;
	}
}











