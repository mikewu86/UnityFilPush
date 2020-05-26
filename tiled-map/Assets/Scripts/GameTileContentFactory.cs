using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class GameTileContentFactory : ScriptableObject {

	

	[SerializeField]
	GameTileContent emptyPrefab = default;

	[SerializeField]
	GameTileContent wallPrefab = default;

	[SerializeField]
	GameTileContent wall_1Prefab = default;

	[SerializeField]
	GameTileContent content_1 = default;

	[SerializeField]
	GameTileContent content_2 = default;

	[SerializeField]
	GameTileContent content_3 = default;
	[SerializeField]
	GameTileContent content_4 = default;
	[SerializeField]
	GameTileContent content_5 = default;

	Scene contentScene;

	public GameTileContent Get (GameTileContentType type) {
		switch (type) {
			case GameTileContentType.Empty: return Get(emptyPrefab);
			case GameTileContentType.Wall: return Get(wallPrefab);
			case GameTileContentType.Wall_1: return Get(wall_1Prefab);
			case GameTileContentType.Content_1: return Get(content_1);
			case GameTileContentType.Content_2: return Get(content_2);
			case GameTileContentType.Content_3: return Get(content_3);
			case GameTileContentType.Content_4: return Get(content_4);
			case GameTileContentType.Content_5: return Get(content_5);
		
		}
		Debug.Assert(false, "Unsupported type: " + type);
		return null;
	}

	public void Reclaim (GameTileContent content) {
		Debug.Assert(content.OriginFactory == this, "Wrong factory reclaimed!");
		Destroy(content.gameObject);
	}

	GameTileContent Get (GameTileContent prefab) {
		GameTileContent instance = Instantiate(prefab);
		instance.OriginFactory = this;
		MoveToFactoryScene(instance.gameObject);
		return instance;
	}

	void MoveToFactoryScene (GameObject o) {
		if (!contentScene.isLoaded) {
			if (Application.isEditor) {
				contentScene = SceneManager.GetSceneByName(name);
				if (!contentScene.isLoaded) {
					contentScene = SceneManager.CreateScene(name);
				}
			}
			else {
				contentScene = SceneManager.CreateScene(name);
			}
		}
		SceneManager.MoveGameObjectToScene(o, contentScene);
	}
}