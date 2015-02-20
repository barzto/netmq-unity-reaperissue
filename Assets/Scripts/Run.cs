using Base.Utils;
using UnityEngine;
using NetMQ;

public class Run : MonoBehaviour
{
	private static void TestNetMQ()
	{
		using (NetMQContext ctx = NetMQContext.Create())
		{
			using (var server = ctx.CreateResponseSocket())
			{
				server.Bind("tcp://127.0.0.1:5556");
				
				using (var client = ctx.CreateRequestSocket())
				{
					client.Connect("tcp://127.0.0.1:5556");
					
					client.Send("Hello");
					
					string m1 = server.ReceiveString();
					
					GameDebug.LogFormat("From Client: {0}", m1);
					
					server.Send("Hi Back");
					
					string m2 = client.ReceiveString();
					
					GameDebug.LogFormat("From Server: {0}", m2);
					
					//Console.ReadLine();
				}
			}
		}
	}
	
	void Start()
	{
		GameDebug.Init();
		TestNetMQ();
	}
}