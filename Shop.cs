using System;

namespace FirstPlayable_CalebWolthers_22012024
{
    internal class Shop
    {
        private Currency currency;
        private Player player;
        private Map map;
        private UI ui;

        private string[] items = { "1.Health Potion", "2.Enemy Freeze", "3.Invincibility" };
        private int[] prices = { 1, 2, 3 };
        private string selectedItem = "";
        private bool isOpen = false;

        public Shop(Currency currency, Player player, Map map, UI ui)
        {
            this.currency = currency;
            this.player = player;
            this.map = map;
            this.ui = ui;
        }

        public void DisplayShop()
        {
            isOpen = true;
            int shopStartPosX = 0;
            int shopStartPosY = 0;

            Console.SetCursorPosition(shopStartPosX, shopStartPosY);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------------");
            Console.WriteLine("      SHOP       ");
            Console.WriteLine("------------------");
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < items.Length; i++)
            {
                Console.SetCursorPosition(shopStartPosX, shopStartPosY + 2 + i);
                Console.WriteLine(items[i] + " - " + prices[i] + " coins");
            }

            Console.SetCursorPosition(shopStartPosX, shopStartPosY + 5);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(shopStartPosX, shopStartPosY + 6);
            Console.WriteLine("Currency: " + currency.currency);
            Console.SetCursorPosition(shopStartPosX, shopStartPosY + 7);
            Console.WriteLine("Select an item (1-3) or press ESC to exit.");
        }

        public void HandleInput(ConsoleKeyInfo input)
        {
            if (isOpen)
            {
                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        BuyHealth();
                        break;
                    case ConsoleKey.D2:
                        BuyFreeze();
                        break;
                    case ConsoleKey.D3:
                        BuyInvincibility();
                        break;
                    case ConsoleKey.Escape:
                        isOpen = false;
                        Console.Clear();
                        break;
                    default:
                        Console.SetCursorPosition(0, 8);
                        Console.WriteLine("Invalid input                                               ");
                        break;
                }
            }
        }
        private void BuyHealth()
        {
            Console.SetCursorPosition(0, 8);
            if (currency.currency >= prices[0])
            {
                currency.LoseCurrency(prices[0]);
                Console.WriteLine("You bought a Health Potion!");

                ItemHealth healthPotion = new ItemHealth(map, player, ui);
                healthPotion.DoYourJob();
            }
            else
            {
                Console.WriteLine("Not enough currency to buy Health Potion               ");
            }
        }

        private void BuyFreeze()
        {
            Console.SetCursorPosition(0, 8);
            if (currency.currency >= prices[1])
            {
                currency.LoseCurrency(prices[1]);
                Console.WriteLine("You bought Freeze!");

                ItemFreeze freeze = new ItemFreeze(map, player, ui);
                freeze.DoYourJob();
            }
            else
            {
                Console.WriteLine("Not enough currency to buy Enemy Freeze              ");
            }
        }
        private void BuyInvincibility()
        {
            Console.SetCursorPosition(0, 8);
            if (currency.currency >= prices[2])
            {
                currency.LoseCurrency(prices[2]);
                Console.WriteLine("You bought Invincibility!");

                ItemInvincible invincibility = new ItemInvincible(map, player, ui);
                invincibility.DoYourJob();
            }
            else
            {
                Console.WriteLine("Not enough currency to buy Invincibility               ");
            }
        }


        public bool IsShopOpen()
        {
            return isOpen;
        }
    }
}
