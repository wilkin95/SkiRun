﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiRunRater
{
    public class Controller
    {
        #region FIELDS

        bool active = true;

        #endregion

        #region PROPERTIES


        #endregion

        #region CONSTRUCTORS

        public Controller()
        {
            ApplicationControl();
        }

        #endregion

        #region METHODS

        private void ApplicationControl()
        {
            SkiRunRepository skiRunRepository = new SkiRunRepository();

            ConsoleView.DisplayWelcomeScreen();

            using (skiRunRepository)
            {
                List<SkiRun> skiRuns = skiRunRepository.GetSkiAllRuns();

                while (active)
                {
                    AppEnum.ManagerAction userActionChoice;
                    int vertical;
                    int skiRunID;
                    SkiRun skiRun;
                    string message;

                    userActionChoice = ConsoleView.GetUserActionChoice();

                    switch (userActionChoice)
                    {
                        case AppEnum.ManagerAction.None:
                            break;
                        case AppEnum.ManagerAction.ListAllSkiRuns:
                            ConsoleView.DisplayAllSkiRuns(skiRuns);
                            ConsoleView.DisplayContinuePrompt();
                            break;
                        case AppEnum.ManagerAction.DisplaySkiRunDetail:
                             ConsoleView.DisplayAllSkiRuns(skiRuns);
                            skiRun = skiRunRepository.GetSkiRunByID(ConsoleView.DisplayGetSkiRunID(skiRuns));
                            ConsoleView.DisplaySkiRunDetails(skiRuns);
                            break;
                        case AppEnum.ManagerAction.DeleteSkiRun:                            
                            skiRunRepository.DeleteSkiRun(ConsoleView.DisplayGetSkiRunID(skiRuns));
                            ConsoleView.DisplayReset();
                            ConsoleView.DisplayMessage("Ski Run has been deleted.");
                            ConsoleView.DisplayContinuePrompt();
                            break;
                        case AppEnum.ManagerAction.AddSkiRun:
                            skiRun = new SkiRun();
                            skiRun.Name = ConsoleView.DisplayGetSkiRunName();
                            skiRun.Vertical = ConsoleView.DisplayGetSkiRunVertical();
                            skiRunRepository.InsertSkiRun(skiRun);
                            ConsoleView.DisplayAllSkiRuns(skiRuns);
                            ConsoleView.DisplayNewSkiRunMessage();
                            break;
                        case AppEnum.ManagerAction.UpdateSkiRun:
                            break;
                        case AppEnum.ManagerAction.QuerySkiRunsByVertical:
                            break;
                        case AppEnum.ManagerAction.Quit:
                            active = false;
                            break;
                        default:
                            break;
                    }
                }
            }

            ConsoleView.DisplayExitPrompt();
        }

        #endregion

    }
}
