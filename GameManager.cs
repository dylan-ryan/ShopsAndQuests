﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstPlayable_CalebWolthers_22012024
{
    internal class GameManager
    {
        public static Map map;
        public static Player player;
        public static EnemyManager enemyManager;
        public static ItemManager itemManager;
        public static UI ui;
        public static bool gameOver;
        public static Currency currency;
        public static Shop shop;

        public void Play()
        {
            // Init
            player = new Player();
            map = new Map(player);
            currency = new Currency();
            enemyManager = new EnemyManager(player, map, currency);
            gameOver = false;
            map.StartMap();
            ui = new UI(player, map, enemyManager, currency);
            ui.LoadStartingScreen();
            itemManager = new ItemManager(player, map, ui);
            shop = new Shop(currency, player, map, ui, 10, 10);
            Shop shop2 = new Shop(currency, player, map, ui, 10, 10);
            Shop shop3 = new Shop(currency, player, map, ui, 10, 10);
            player.SetStuff(map, enemyManager, ui, itemManager);
            map.DisplayMap();

            enemyManager.PlaceGoblins(5);
            enemyManager.PlaceOrcs(25);
            enemyManager.PlaceMinotaurs(5);
            enemyManager.PlaceDragons(1);

            itemManager.PlaceHealthPotions(25);
            itemManager.PlaceInvincibility(10);
            itemManager.PlaceFreeze(10);

            while (!gameOver)
            {
                if (player.healthSystem.health <= 0)
                {
                    gameOver = true;
                    break;
                }

                GetInput();

                // Check if the shop is open
                if (shop.IsShopOpen())
                {
                    // Handle shop input only
                    shop.HandleInput(input);
                }
                else
                {
                    // Update
                    itemManager.UpdateItems();
                    player.Update(input);
                    shop.Update();
                    enemyManager.UpdateEnemies();

                    // Draw
                    itemManager.DrawItems();
                    enemyManager.DrawEnemies();
                    player.Draw();
                    map.DisplayMap();
                    ui.Draw();
                    shop.DrawShop();

                    // Open the shop by player interaction
                    if (input.Key == ConsoleKey.Q)
                    {
                        Console.Clear();
                        shop.DisplayShop();
                    }
                }
            }

            // Handle game over
            Console.Clear();
            Console.WriteLine("Game Over, try again");
        }


        public void GetInput()
        {
            input = Console.ReadKey(true);
        }


        private ConsoleKeyInfo input;
    }
}
