using System;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.CodeAnalyser
{
    [Serializable]
    public class CodeAnalyserResultContent : IResult
    {
        [SerializeField] private CodeAnalyserStatus status = CodeAnalyserStatus.NotInitialised;

        public string Content()
        {
            return status switch
            {
                CodeAnalyserStatus.SetupOk => "Great, your code analyser is ready to use!",
                CodeAnalyserStatus.SetupMisconfigured =>
                    "Your code analyser is misconfigured. The \"fix\" button will configure it appropriately.",
                CodeAnalyserStatus.NotSupportedIde =>
                    "Your IDE is not officially supported by Unity and the best practice code analyser. Consider changing IDE to a supported one in order to use this code analyser. You find further details in the documentation",
                CodeAnalyserStatus.NoCodeAnalyser =>
                    "You use no code analyser. It is strongly recommended to install and utilise code analyser in your project. You can setup the code analyser through the \"fix\" button.",
                _ => "Something went wrong in the code analyser initialization!"
            };
        }

        public void Status(CodeAnalyserStatus newStatus)
        {
            status = newStatus;
        }
    }
}