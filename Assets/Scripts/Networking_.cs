using UnityEngine;
using System.Collections;


public class Networking_ : MonoBehaviour {
	private string gameName = "_TheUnrealTroll_";
	private bool refreshed = false;
	private HostData[] hd;
	void StartServer(){
		Network.InitializeServer (4, 25000, false);
		MasterServer.ipAddress = "127.0.0.1";
		MasterServer.port = 23466;
		MasterServer.RegisterHost(gameName, gameName);
	}
		

	void JoinServer(){
		Debug.Log (Network.Connect ("127.0.0.1", 25000));	
		Debug.Log("Join....");
	}

	void RefreshHostList(){
		Debug.Log ("Refreshing...");
		refreshed = true;
		MasterServer.RequestHostList (gameName);

	}

	void OnConnectedToServer() {
		Debug.Log("Connected to server");
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
	}

	void OnMasterServerEvent(MasterServerEvent mse){
		if (mse == MasterServerEvent.RegistrationSucceeded) {
			Debug.Log("Server registered");
		}
	}
	void OnGUI(){
		if (GUI.Button (new Rect (10, 10, 200, 50), "Host Server")) {
			StartServer();
		}
		if (GUI.Button (new Rect (10, 70, 200, 50), "Join Server")) {
			JoinServer();
		}
		if (GUI.Button (new Rect (10, 140, 200, 50), "Refresh Server List")) {
			RefreshHostList();
		}
	}
	// Update is called once per frame
	void Update () {
		if (refreshed) {
			//Debug.Log ("Refreshed = true");
			//MasterServer.RequestHostList (gameName);
			if(MasterServer.PollHostList().Length > 0){
				refreshed = false;
				Debug.Log (MasterServer.PollHostList().Length);
				if (MasterServer.PollHostList().Length != 0) {
					hd = MasterServer.PollHostList();
					int i = 0;
					while(i < hd.Length){
						Debug.Log("Game name: " + hd[i].gameName);
						i++;
					}
					MasterServer.ClearHostList();
				}
			}
		}


		//RefreshHostList ();
	}
}
