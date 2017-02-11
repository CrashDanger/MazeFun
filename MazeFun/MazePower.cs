﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeFun
{
    struct Spot
    {
        public int width, height;

        public Spot(int x, int y)
        {
            width = x;
            height = y;
        }
    }

    struct Move
    {
        public Spot from, to;
        public Move(Spot from, Spot to)
        {
            this.from = from;
            this.to = to;
        }
    }
    struct mazeTile
    {
        public bool up, down, left, right, maze;

        public mazeTile(int empty)
        {
            up = down = left = right = maze = false;
        }
    }
    class Maze
    {
        private byte[,] bytesMap;
        public Maze(int width, int height)
        {
            bytesMap = new byte[width,height];
        }
    }

    class MazePower
    {
        private Random RandSeed = new Random();
        private mazeTile[,] emptyMaze(int width, int height)
        {
            mazeTile[,] result = new mazeTile[width, height];
            for (int q = 0; q < width; q++)
            {
                for (int i = 0; i < height; i++)
                {
                    result[width,height] = new mazeTile(0);
                }
            }
            return result;
        }
        mazeTile[,] makeMazeTiles(int width, int height)
        {
            mazeTile[,] result = emptyMaze(width, height);
            result[0, 0].maze = result[0,0].down = result[0, 0].right = true;
            result[1, 0].maze = result[1, 0].left = true;
            result[0, 1].maze = result[0, 1].up = true;
            List<Spot> activeSpots = new List<Spot>();
            activeSpots.Add(new Spot(1,0));
            activeSpots.Add(new Spot(0, 1));
            int emptytiles = (width*height) - 3;
            do
            {
                List<Move> PossibleAddons = new List<Move>();
                foreach (var spot in activeSpots) // creating list of possible moves
                {
                    if (spot.width + 1 < width && !result[spot.width + 1, spot.height].maze)
                    {
                        PossibleAddons.Add(new Move(spot, new Spot(spot.width + 1, spot.height)));
                    }
                    if (spot.width - 1 > -1 && !result[spot.width - 1, spot.height].maze)
                    {
                        PossibleAddons.Add(new Move(spot, new Spot(spot.width - 1, spot.height)));
                    }
                    if (spot.height + 1 < height && !result[spot.width, spot.height + 1].maze)
                    {
                        PossibleAddons.Add(new Move(spot, new Spot(spot.width, spot.height + 1)));
                    }
                    if (spot.height - 1 > -1 && !result[spot.width, spot.height - 1].maze)
                    {
                        PossibleAddons.Add(new Move(spot, new Spot(spot.width, spot.height - 1)));
                    }
                }
                Move ChosenMove = PossibleAddons[RandSeed.Next(PossibleAddons.Count)];
                switch ((ChosenMove.from.width - ChosenMove.to.width) + (2 * (ChosenMove.from.height - ChosenMove.to.height)))
                {
                    case 1:
                        result[ChosenMove.from.width, ChosenMove.from.height].right = true;
                        result[ChosenMove.to.width, ChosenMove.to.height].left = true;
                        break;
                    case -1:
                        result[ChosenMove.from.width, ChosenMove.from.height].left = true;
                        result[ChosenMove.to.width, ChosenMove.to.height].right = true;
                        break;
                    case 2:
                        result[ChosenMove.from.width, ChosenMove.from.height].up = true;
                        result[ChosenMove.to.width, ChosenMove.to.height].down = true;
                        break;
                    case -2:
                        result[ChosenMove.from.width, ChosenMove.from.height].down = true;
                        result[ChosenMove.to.width, ChosenMove.to.height].up = true;
                        break;
                    default:
                        break;
                }
                result[ChosenMove.to.width, ChosenMove.to.height].maze = true;
                emptytiles--;
                activeSpots.Remove(ChosenMove.from);
                activeSpots.Add(ChosenMove.to);
            } while (emptytiles > 0);

            return result;
        } 
    }
}
