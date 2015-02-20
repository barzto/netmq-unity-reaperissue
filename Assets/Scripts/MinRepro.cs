using UnityEngine;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;

public class MinRepro : MonoBehaviour
{
    private Socket _readSocket;
    private Socket _writeSocket;
    private byte[] _dummyRead = new byte[]{0};
    private byte[] _dummyWrite = new byte[]{0};
    
    private void MakeSocketsPair()
    {
        using (Socket listner = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Unspecified))
        {
            listner.NoDelay = true;
            listner.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            
            // using ephemeral port            
            listner.Bind(new IPEndPoint(IPAddress.Loopback, 0));
            listner.Listen(1);
            
            _writeSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Unspecified);
            _writeSocket.NoDelay = true;
            
            _writeSocket.Connect(listner.LocalEndPoint);
            _readSocket = listner.Accept();
        }            
        
        
    }
    
    // Use this for initialization
    void Start()
    {
    	Base.Utils.GameDebug.Init();
    	MakeSocketsPair();
    
	    int sent = _writeSocket.Send(_dummyWrite);
        Base.Utils.GameDebug.Assert(sent == 1);
        
		List<Socket> readList = new List<Socket>();
		List<Socket> writeList = new List<Socket>();
		List<Socket> errorList = new List<Socket>();
		while (true)
        {
        	readList.Clear();
			writeList.Clear();
			errorList.Clear();
			readList.Add(_readSocket);
			errorList.Add(_readSocket);
			
			Socket.Select(readList, writeList, errorList, 0);
			
			if (readList.Count > 0)
			{
				Base.Utils.GameDebug.Log("Read ready");
                
				break;
			}
			Base.Utils.GameDebug.Log("Tick");
        }
		
		int recv = _readSocket.Receive(_dummyRead);
		Base.Utils.GameDebug.Assert(recv == 1);
    }
	
    // Update is called once per frame
    void Update()
    {
	
    }
}

