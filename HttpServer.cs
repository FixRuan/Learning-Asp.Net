using System.Net;
using System.Net.Sockets;

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
    }
    catch (Exception error)
    {
      Console.WriteLine($"Erro ao iniciar servidor na porta {this.Port}:\n{error.Message}.");
    }
  }

}