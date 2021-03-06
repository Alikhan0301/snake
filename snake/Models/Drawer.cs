﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace snake.Models
{   [Serializable]
    class Drawer
    {   
        /// <summary>
        /// создаются методы для постановки символов, сериализации и десериализации файлов через BinaryFormatter   
        /// </summary>
        public List<Point> body = new List<Point>();
        public ConsoleColor color;
        public char sign;

        public void Draw()
        {
            foreach (Point p in body)
            {
                if (p.x == Game.snake.body[0].x && p.y == Game.snake.body[0].y)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = color;
                }
                Console.SetCursorPosition(p.x, p.y);
                Console.Write(sign);
            }
        }
        public void Erase()
        {
            foreach (Point p in body)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(p.x, p.y);
                Console.Write("-");
            }
        }

        public void Save()
        {
           
            Type t = GetType();
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(string.Format("{0}.dat", t.Name), FileMode.Create, FileAccess.Write);
            formatter.Serialize(fs, this);
            fs.Close();
        }

        public void Resume()
        {
           
            Type t = GetType();
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(string.Format(@"C:\snake\snake\snake\bin\Debug\{0}.dat", t.Name), FileMode.Open, FileAccess.Read);
            if (t == typeof(Wall)) Game.wall = formatter.Deserialize(fs) as Wall;
            if (t == typeof(Snake)) Game.snake = formatter.Deserialize(fs) as Snake;
            if (t == typeof(Food)) Game.food = formatter.Deserialize(fs) as Food;
            fs.Close();
        }
    }
}
