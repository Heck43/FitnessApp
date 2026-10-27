using System;
using System.Linq;

namespace FitnessApp;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        using var db = new FitnessContext();

        Console.WriteLine("=== 1. Все клиенты фитнес-клуба ===");
        var allClients = db.Clients
            .OrderBy(c => c.ClientId)
            .ToList();

        foreach (var client in allClients)
        {
            Console.WriteLine($"ID: {client.ClientId} | Имя: {client.FirstName} | Фамилия: {client.LastName} | Статус: {client.Status}");
        }

        Console.WriteLine("\n=== 2. Активные клиенты (по фамилии) ===");
        // Если в БД статус хранится на русском ("Активен" / "active")
        var activeClients = db.Clients
            .Where(c => c.Status.ToLower() == "active" || c.Status.ToLower() == "активен" || c.Status.ToLower() == "активный")
            .OrderBy(c => c.LastName)
            .ToList();

        foreach (var client in activeClients)
        {
            Console.WriteLine($"ID: {client.ClientId} | Фамилия: {client.LastName} | Имя: {client.FirstName} | Статус: {client.Status}");
        }

        Console.Write("\n=== 3. Поиск клиента по ID ===\nВведите ID клиента: ");
        var input = Console.ReadLine();

        if (int.TryParse(input, out int searchId))
        {
            var foundClient = db.Clients.FirstOrDefault(c => c.ClientId == searchId);


            if (foundClient != null)
            {
                Console.WriteLine($"Найден клиент: ID: {foundClient.ClientId}, Имя: {foundClient.FirstName}, Фамилия: {foundClient.LastName}, Статус: {foundClient.Status}");
            }
            else
            {
                Console.WriteLine($"Клиент с ID {searchId} не найден.");
            }
        }
        else
        {
            Console.WriteLine("Некорректный формат ID. Необходимо ввести целое число.");
        }
    }
}