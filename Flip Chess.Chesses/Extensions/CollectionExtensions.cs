using System;
using System.Collections.Generic;
using System.Linq;

namespace Flip_Chess.Chesses.Extensions
{
    public static partial class CollectionExtensions
    {
        readonly static Random Instance = new Random();

        public static void Home(this IChess[] array)
        {
            int index = 0;
            int count = array.Length;

            do
            {
                foreach (ChessCount item in System.Enum.GetValues(typeof(ChessCount)).Cast<ChessCount>())
                {
                    array[index].Type = item.ToRed();
                    index++;
                    if (index >= count) return;

                    array[index].Type = item.ToBlack();
                    index++;
                    if (index >= count) return;
                }
            } while (index < count);
        }

        public static void Random(this IChess[] array)
        {
            int count = array.Length;

            for (int i = 0; i < count; i++)
            {
                int random = CollectionExtensions.Instance.Next(0, count - 1);

                ChessType from = array[i].Type;
                ChessType to = array[random].Type;

                array[i].Type = to;
                array[random].Type = from;
            }
        }

        public static History RandomFlip(this ChessType[,,] array, History defaultValue)
        {
            if (defaultValue != History.Noway) return defaultValue;

            int count = 0;

            for (int y = 0; y < array.Height(); y++)
            {
                for (int x = 0; x < array.Width(); x++)
                {
                    ChessType item = array[0, y, x];

                    if (item is ChessType.Unkonw)
                    {
                        count++;
                    }
                }
            }

            switch (count)
            {
                case 0:
                    return defaultValue;
                case 1:
                    for (int y = 0; y < array.Height(); y++)
                    {
                        for (int x = 0; x < array.Width(); x++)
                        {
                            ChessType item = array[0, y, x];

                            if (item is ChessType.Unkonw)
                            {
                                return new History(y, x);
                            }
                        }
                    }
                    return defaultValue;
                default:
                    {
                        int random = CollectionExtensions.Instance.Next(0, count - 1);

                        for (int y = 0; y < array.Height(); y++)
                        {
                            for (int x = 0; x < array.Width(); x++)
                            {
                                ChessType item = array[0, y, x];

                                if (item is ChessType.Unkonw)
                                {
                                    if (random <= 0)
                                    {
                                        return new History(y, x);
                                    }
                                    random--;
                                }
                            }
                        }
                    }
                    return defaultValue;
            }
        }

        public static void Clear(this IEnumerable<IChess> enumerable)
        {
            foreach (IChess item in enumerable)
            {
                item.Type = default;
            }
        }

        public static void Copy(this IChess[] sourceArray, ChessType[,,] destinationArray)
        {
            for (int y = 0; y < destinationArray.Height(); y++)
            {
                for (int x = 0; x < destinationArray.Width(); x++)
                {
                    int index = destinationArray.IndexOf(y, x);
                    sourceArray[index].Type = destinationArray[0, y, x];
                }
            }
        }

        public static int Width(this ChessType[,,] array) => array.GetLength(2);

        public static int Height(this ChessType[,,] array) => array.GetLength(1);

        public static int ZIndex(this ChessType[,,] array) => array.GetLength(0);

        public static void Clear(this ChessType[,,] array)
        {
            Array.Clear(array, 0, array.Length);
        }

        public static int IndexOf(this ChessType[,,] array, int y, int x)
        {
            //int h = array.Height();
            int w = array.Width();

            return y * w + x;
        }

        public static void Copy(this ChessType[,,] array, int sourceIndex, int destinationIndex)
        {
            for (int y = 0; y < array.Height(); y++)
            {
                for (int x = 0; x < array.Width(); x++)
                {
                    array[sourceIndex, y, x] = array[destinationIndex, y, x];
                }
            }
        }

        public static int GetLevel(this ChessType[,,] array, int index)
        {
            int level = 0;
            for (int y = 0; y < array.Height(); y++)
            {
                for (int x = 0; x < array.Width(); x++)
                {
                    ChessType item = array[index, y, x];
                    level += item.GetLevel();
                }
            }
            return level;
        }
    }
}