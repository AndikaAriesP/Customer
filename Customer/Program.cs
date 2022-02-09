// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
RMQ rmq = new RMQ();
Console.WriteLine("Tekan tombol apapun untuk inisialisasi RMQ parameters.");
Console.ReadKey();
rmq.InitRMQConnection(); // inisialisasi parameter (secara default) untuk koneksi ke server RMQ
Console.WriteLine("Tekan tombol apapun untuk membuka koneksi ke RMQ.");
Console.ReadKey();
rmq.CreateRMQConnection(); // memulai koneksi dengan RMQ
Console.Write("Masukkan nama queue channel untuk menerima pesan melalui RMQ.\n>> ");
string queue_name = Console.ReadLine();
Console.WriteLine("Menunggu pesan masuk...");
rmq.WaitingMessage(queue_name);