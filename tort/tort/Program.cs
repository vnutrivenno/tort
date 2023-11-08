using System;
using System.Collections.Generic;
using System.IO;

public struct MenuItem
{
    public string Description { get; set; }
    public decimal Price { get; set; }
}

public class CakeOrder
{
    private List<MenuItem> mainMenu;
    private List<MenuItem> submenu;
    private List<MenuItem> selectedItems;
    private decimal totalPrice;

    public CakeOrder()
    {
        mainMenu = new List<MenuItem>
        {
            new MenuItem { Description = "Форма торта", Price = 0 },
            new MenuItem { Description = "Размер торта", Price = 0 },
            new MenuItem { Description = "Вкус торта", Price = 0 }
        };

        submenu = new List<MenuItem>
        {
            new MenuItem { Description = "Количество", Price = 0 },
            new MenuItem { Description = "Глазурь", Price = 0 },
            new MenuItem { Description = "Декор", Price = 0 }
        };

        selectedItems = new List<MenuItem>();
        totalPrice = 0;
    }

    public void StartOrder()
    {
        Console.Clear();
        Console.WriteLine("Добро пожаловать в кондитерскую!");
        Console.WriteLine("Чтобы сделать заказ, выберите следующие пункты:");

        while (true)
        {
            Console.WriteLine("Основное меню:");
            int mainMenuItemIndex = DisplayMenu(mainMenu);
            if (mainMenuItemIndex == -1) // Нажат Escape для выхода
                break;

            Console.WriteLine("Дополнительное меню:");
            int submenuItemIndex = DisplayMenu(submenu);
            if (submenuItemIndex == -1) // Нажат Escape для выхода
                break;

            // В этом месте вы можете обработать выбор пользователя и обновить selectedItems и totalPrice
            // Например, добавить выбранный элемент из mainMenu и submenu в selectedItems

            Console.WriteLine("Текущий заказ:");
            DisplaySelectedItems();
        }

        SaveOrderToFile();
    }

    private int DisplayMenu(List<MenuItem> menu)
    {
        for (int i = 0; i < menu.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {menu[i].Description} - {menu[i].Price:C}");
        }

        Console.WriteLine("Нажмите Enter для выбора, Escape для выхода");
        var key = Console.ReadKey(true).Key;

        if (key == ConsoleKey.Enter)
        {
            Console.Write("Выберите пункт меню: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= menu.Count)
            {
                return choice - 1;
            }
        }
        else if (key == ConsoleKey.Escape)
        {
            return -1; // Выйти из меню
        }

        return -2; // Некорректный выбор
    }

    private void DisplaySelectedItems()
    {
        foreach (var item in selectedItems)
        {
            Console.WriteLine($"{item.Description} - {item.Price:C}");
        }
        Console.WriteLine($"Итого: {totalPrice:C}");
    }

    private void SaveOrderToFile()
    {
        string orderInfo = string.Join(Environment.NewLine, selectedItems);
        File.WriteAllText("История_заказов.txt", orderInfo);
    }
}

class Program
{
    static void Main(string[] args)
    {
        CakeOrder cakeOrder = new CakeOrder();
        cakeOrder.StartOrder();
    }
}