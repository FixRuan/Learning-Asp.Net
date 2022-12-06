using System.Net;
using System.Net.Sockets;
using System.Text;

class HttpServer
{

  private TcpListener Controller { get; set; }

  private int Port { get; set; }

  private int RequestsQtd { get; set; }

  public HttpServer(int port = 8080)
  {
    this.Port = port;

    try
    {
      this.Controller = new TcpListener(IPAddress.Parse("127.0.0.1"), this.Port);
      this.Controller.Start();
      Console.WriteLine($"Servidor HTTP está rodando na porta {this.Port}.");
      Console.WriteLine($"Para acessar, acesse: http://localhost:{this.Port}.");

      Task taskHttpServer = Task.Run(() => AwaitRequests());
      taskHttpServer.GetAwaiter().GetResult();
    }
    catch (Exception error)
    {
      Console.WriteLine($"Erro ao iniciar servidor na porta {this.Port}:\n{error.Message}.");
    }
  }


  private async Task AwaitRequests()
  {
    while (true)
    {
      Socket connect = await this.Controller.AcceptSocketAsync();
      this.RequestsQtd++;

      Task task = Task.Run(() => ProcessRequest(connect, this.RequestsQtd));
    }
  }

  private void ProcessRequest(Socket connect, int requestNumber)
  {
    Console.WriteLine($"Processando request #{requestNumber}.");

    if (connect.Connected)
    {
      byte[] bytesRequest = new byte[1024];
      connect.Receive(bytesRequest, bytesRequest.Length, 0);

      string requestText = Encoding.UTF8.GetString(bytesRequest).Replace((char)0, ' ').Trim();

      if (requestText.Length > 0)
      {
        Console.WriteLine($"\n{requestText}\n");
        connect.Close();
      }
    }

    Console.WriteLine($"\n Request {requestNumber} finalizada");
  }
}
