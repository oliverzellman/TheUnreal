using UnityEngine;
using System.Collections;


public class Networking_ : MonoBehaviour {
	private string gameName = "_TheUnrealTroll_";
	private bool refreshed = false;
	private HostData[] hd;
	public GameObject playerPrefab;
	public Transform spawnObject;

	MainCamera mainCamera = new MainCamera();
	GameObject player;

	private string _IP = "83.254.134.189";

	void StartServer(){
		Network.InitializeServer (4, 25000, false);
		MasterServer.ipAddress = _IP;
		MasterServer.port = 23466;
		MasterServer.RegisterHost(gameName, gameName);
	}
		

	void JoinServer(){
		Debug.Log (Network.Connect (_IP, 25000));	
		Debug.Log("Join....");
	}

	void RefreshHostList(){
		Debug.Log ("Refreshing...");
		refreshed = true;
		MasterServer.RequestHostList (gameName);

	}

	void OnConnectedToServer() {
		Debug.Log("Connected to server");
		spawnPlayer ();
	}

	void OnFailedToConnect(NetworkConnectionError error) {
		Debug.Log("Could not connect to server: " + error);
	}

	// Use this for initialization
	void Start () {
		//hd.Add (new HostData());
	}

	void OnServerInitialized() {
		Debug.Log("Server initialized and ready");
		spawnPlayer ();
	}

	void spawnPlayer() {

		 Network.Instantiate (playerPrefab, spawnObject.position, Quaternion.identity, 0);


	}

	void OnMasterServerEvent(MasterServerEvent mse){
		if (mse == MasterServerEvent.RegistrationSucceeded) {
			Debug.Log("Server registered");
		}
	}
	void OnGUI(){

		//if (!Network.isClient && !Network.isServer) {
						if (GUI.Button (new Rect (10, 10, 200, 50), "Host Server")) {
								StartServer ();
						}
						if (GUI.Button (new Rect (10, 70, 200, 50), "Join Server")) {
								JoinServer ();
						}
						if (GUI.Button (new Rect (10, 140, 200, 50), "Refresh Server List")) {
								RefreshHostList ();

						}


		//		}
	}

	void getServerList(){
				if (refreshed) {
						//Debug.Log ("Refreshed = true");
						//MasterServer.RequestHostList (gameName);
						if (MasterServer.PollHostList ().Length > 0) {
								refreshed = false;
								Debug.Log (MasterServer.PollHostList ().Length);
								if (MasterServer.PollHostList ().Length != 0) {
										hd = MasterServer.PollHostList ();
										int i = 0;
										//Layout
										while (i < hd.Length) {
											
						
						
													Debug.Log("Game name: " + hd[i].gameName);
												i++;
												
										}
									
										MasterServer.ClearHostList ();
								}
						}
				}
		}

	// Update is called once per frame
	void Update () {
	
		getServerList ();
		//RefreshHostList ();
	}
}
