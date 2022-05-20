using System;
using System.Collections.Generic;
using UnityEngine;

namespace BestPracticeChecker.Editor.BusinessLogic.BestPractices.TextureRatio
{
    [Serializable]
    public sealed class TextureRatioResultContent : IResult
    {
        [SerializeField] private Status status = BusinessLogic.Status.NotCalculated;
        [SerializeField] private List<FaultyTexture> faultyTextures = new List<FaultyTexture>();

        public string Content()
        {
            return status switch
            {
                BusinessLogic.Status.Ok => "Excellent, all your texture's width and height are based on two with compression enabled.",
                BusinessLogic.Status.Warning =>
                    "You have either textures which dimension are not based on two, or textures with compression turned off.\n It is advised considering updating the listed textures below in order to optimise memory usage.",
                _ => "Something went wrong in the texture ratio initialization!"
            };
        }

        public void Status(Status packageStatus)
        {
            status = packageStatus;
        }

        public void AddFaultyTexture(FaultyTexture texture)
        {
            faultyTextures.Add(texture);
        }

        public List<FaultyTexture> FaultyTextures()
        {
            return faultyTextures;
        }
    }
}