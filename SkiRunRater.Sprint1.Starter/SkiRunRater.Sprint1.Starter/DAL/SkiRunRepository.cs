﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SkiRunRater
{
    /// <summary>
    /// method to write all ski run information to the date file
    /// </summary>
    public class SkiRunRepository : IDisposable
    {
        private List<SkiRun> _skiRuns;

        public SkiRunRepository()
        {
            _skiRuns = ReadSkiRunsData(DataSettings.dataFilePath);
        }

        /// <summary>
        /// method to read all ski run information from the data file and return it as a list of SkiRun objects
        /// </summary>
        /// <param name="dataFilePath">path the data file</param>
        /// <returns>list of SkiRun objects</returns>
        public static List<SkiRun> ReadSkiRunsData(string dataFilePath)
        {
            const char delineator = ',';

            // create lists to hold the ski run strings and objects
            List<string> skiRunStringList = new List<string>();
            List<SkiRun> skiRunClassList = new List<SkiRun>();

            // initialize a StreamReader object for reading
            StreamReader sReader = new StreamReader(DataSettings.dataFilePath);

            using (sReader)
            {
                // keep reading lines of text until the end of the file is reached
                while (!sReader.EndOfStream)
                {
                    skiRunStringList.Add(sReader.ReadLine());
                }
            }
            
            foreach (string skiRun in skiRunStringList)
            {
                // use the Split method and the delineator on the array to separate each property into an array of properties
                string[] properties = skiRun.Split(delineator);

                // populate the ski run list with SkiRun objects
                skiRunClassList.Add(new SkiRun() { ID = Convert.ToInt32(properties[0]), Name = properties[1], Vertical = Convert.ToInt32(properties[2]) });
            }

            return skiRunClassList;
        }
 

        /// <summary>
        /// method to write all of the list of ski runs to the text file
        /// </summary>
        public void WriteSkiRunsData()
        {
            string skiRunString;

            // wrap the FileStream object in a StreamWriter object to simplify writing strings
            StreamWriter sWriter = new StreamWriter(DataSettings.dataFilePath, false);

            using (sWriter)
            {
                foreach (SkiRun skiRun in _skiRuns)
                {
                    skiRunString = skiRun.ID + "," + skiRun.Name + "," + skiRun.Vertical;

                    sWriter.WriteLine(skiRunString);
                }
            }
        }

        /// <summary>
        /// method to add a new ski run
        /// </summary>
        /// <param name="skiRun"></param>
        public void InsertSkiRun(SkiRun skiRun)
        {
           // List<SkiRun> skiRuns = new List<SkiRun>();
            string skiRunString;
            int vertical;
            skiRunString = skiRun.ID + "," + skiRun.Name + "," + skiRun.Vertical;

            Console.WriteLine("Please enter the name of your ski run.");
            skiRun.Name = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Please enter the vertical drop of " + skiRun.Name);
            string response = Console.ReadLine();
            if(Int32.TryParse(response, out vertical))
            {
                skiRun.Vertical = vertical;
            }

           

            _skiRuns.Add(new SkiRun() { ID = 1, Name = skiRun.Name, Vertical = skiRun.Vertical });


            WriteSkiRunsData();


        }

        /// <summary>
        /// method to delete a ski run by ski run ID
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteSkiRun( )
        {
            
            int ID;
            string name;
            Console.WriteLine();
            Console.WriteLine("Please choose the ID of the ski run you wish to delete from the list.");
            string response = Console.ReadLine();
            if(Int32.TryParse(response, out ID))
            {
                SkiRun skiRun = GetSkiRunByID(ID);

                for (int index = 0; index < _skiRuns.Count(); index++)
                {
                    if (_skiRuns[index].ID == ID)
                    {
                         name = skiRun.Name;
                        _skiRuns.RemoveAt(index);
                        ConsoleView.DisplayMessage("Ski Run " + skiRun.Name + " has been deleted.");

                    }
                }
            }
             WriteSkiRunsData();
        }

        /// <summary>
        /// method to update an existing ski run
        /// </summary>
        /// <param name="skiRun">ski run object</param>
        public void UpdateSkiRun(SkiRun skiRun)
        {

        }

        /// <summary>
        /// method to return a ski run object given the ID
        /// </summary>
        /// <param name="ID">int ID</param>
        /// <returns>ski run object</returns>
        public SkiRun GetSkiRunByID(int ID)
        {
            SkiRun skiRun = null;

            //
            // run through the ski run list and grab the correct one
            //
            foreach (SkiRun location in _skiRuns)
            {
                if (location.ID == ID)
                {
                    skiRun = location;
                }
            }

            //
            // the specified ID was not found 
            // throw an exception
            //
            if (skiRun == null)
            {
                string feedbackMessage = $"The ID {ID} does not exist.";
                throw new ArgumentException(ID.ToString(), feedbackMessage);
            }

            return skiRun;
        }



         

        /// <summary>
        /// method to return a list of ski run objects
        /// </summary>
        /// <returns>list of ski run objects</returns>
        public List<SkiRun> GetSkiAllRuns()
        {
            return _skiRuns;
        }

        /// <summary>
        /// method to query the data by the vertical of each ski run in feet
        /// </summary>
        /// <param name="minimumVertical">int minimum vertical</param>
        /// <param name="maximumVertical">int maximum vertical</param>
        /// <returns></returns>
        public List<SkiRun> QueryByVertical(int minimumVertical, int maximumVertical)
        {
            List<SkiRun> matchingSkiRuns = new List<SkiRun>();

            return matchingSkiRuns;
        }

        /// <summary>
        /// method to handle the IDisposable interface contract
        /// </summary>
        public void Dispose()
        {
            _skiRuns = null;
        }
    }
}
