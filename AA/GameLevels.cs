using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AA
{
    public class GameLevels
    {
        public List<Rotator> Levels { get; set; }
        private const int NumberOfLevels = 10;
        private int CurrentLevel;

        public GameLevels()
        {
            Levels = new List<Rotator>();
            CurrentLevel = LoadCurrentLevel();
            InitLevels();
        }

        public static int LoadCurrentLevel()
        {
            if (!File.Exists("level.bin"))
                return 0;

            using (BinaryReader br = new BinaryReader(File.Open("level.bin", FileMode.Open)))
            {
                return br.ReadByte();
            }
        }

        public static void SaveLevel(int level)
        {
            if (!File.Exists("level.bin"))
                File.Delete("level.bin");

            using (BinaryWriter bw = new BinaryWriter(File.Open("level.bin", FileMode.Create)))
            {
                bw.Write(level);
            }
        }

        private void InitLevels()
        {
            for (int i = 0; i < NumberOfLevels; i++)
            {
                Rotator level = new Rotator((i + 15.0) / NumberOfLevels);
                level.NumberOfBallsToShoot = (int)(((double)(i * 11) / NumberOfLevels) + 5);
                level.NumberOfBallsRotator = (int)(((double)(i * 16) / NumberOfLevels) + 2);

                if (i % 3 == 0)
                {
                    //CheckCollision doesn't work for CounterClockWise rotator
                    //level.RotateClockWise = false;
                }
                else
                {
                    level.RotateClockWise = true;
                }

                if (i % 5 == 0)
                {
                    //level.Speed = 2;
                }

                if (i == 8)
                {
                    level.RotateClockWise = false;
                }

                // should be in Rotator class
                for (int j = 0; j < level.NumberOfBallsRotator; j++)
                {
                    level.Balls.Add(new Ball(j * (360 / level.NumberOfBallsRotator)));
                }

                Levels.Add(level);
            }
        }

        public Rotator GetLevel()
        {
            if (CurrentLevel > NumberOfLevels)
            {
                CurrentLevel = 0;
                SaveLevel(CurrentLevel);
            }
            return Levels[CurrentLevel];
        }

        public Rotator NextLevel()
        {
            if (CurrentLevel < NumberOfLevels)
            {
                int i = CurrentLevel;
                CurrentLevel++;
                SaveLevel(CurrentLevel);
                return Levels[i];
            }
            else
            {
                CurrentLevel = 0;
                SaveLevel(CurrentLevel);
                return Levels[0];
            }

            //return null;
        }
    }
}
